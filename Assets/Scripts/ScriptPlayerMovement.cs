using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptPlayerMovement : MonoBehaviour
{
    // Calling outer scripts
    public ScriptGameManager gameManager;

    // Components within the object
    private Rigidbody2D rb;
    private Animator anim;

    // Serialized Vars
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float invertTime;


    // Vars
    private float initialInvertTime;
    public Vector2 dir;
    private Collider2D collid;
    private Collider2D[] results;
    private bool grounded;
    private bool climbing;
    private bool invertX = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<ScriptGameManager>();
        rb = GetComponent<Rigidbody2D>();
        collid = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        results = new Collider2D[5];
    }

    private void Start()
    {
        initialInvertTime = invertTime;
        gameManager.AddScore(0);
    }

    private void Update()
    {
        CheckCollision();

        // Climbing
        if (climbing)
        {
            dir.y = Input.GetAxisRaw("Vertical") * moveSpeed; 
        }

        // Jumping
        if (grounded && Input.GetButtonDown("Jump"))
        {
            dir = Vector2.up * jumpForce;
        }
        else
        {
            dir += Physics2D.gravity * Time.deltaTime;
        }

        dir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        // prevent to gravity push down
        if (grounded)
        {
            dir.y = Mathf.Max(dir.y, -1f); 
        }

        // Player rotation and animation
        if(dir.x > 0f)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }else if(dir.x < 0f)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        } 
        anim.SetFloat("isWalking", Mathf.Abs(dir.x));

        // Inverting X' axis every x time
        invertTime -= Time.deltaTime;
        if(invertTime <= 0)
        {
            invertX = !invertX;
            invertTime = initialInvertTime;
        }

    }

    // Moving player
    private void FixedUpdate()
    {
        if (invertX)
        {
            dir.x *= -1f;
        }
        rb.MovePosition(rb.position + dir * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Destroy"))
        {
            enabled = false;
            FindObjectOfType<ScriptGameManager>().LevelFailed();
        }else if (collision.gameObject.CompareTag("Finish"))
        {
            enabled = false;
            FindObjectOfType<ScriptGameManager>().LevelComplete();
        }
    }

    // CHECK TRIGGER COLLISION
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("ScoreCounter"))
        {
            //FindObjectOfType<ScriptGameManager>().score += 2.5f;
            gameManager.AddScore(2.5f);
        }

    }

    private void CheckCollision()
    {
        grounded = false;
        climbing = false;

        // Lower collisions to make it 'feel better'
        Vector2 size = collid.bounds.size;
        size.y += .3f;
        size.x /= 2f;

        int qntOfCollisions = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for (int i = 0; i < qntOfCollisions; i++)
        {
            GameObject hit = results[i].gameObject;

            if(hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - .5f); // Make sure not to check with upper collision and lowering check box to its feet
                Physics2D.IgnoreCollision(collid, results[i], !grounded); // Ignore collision when jumping
            } else if(hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                climbing = true;
            }

        }
    }
}
