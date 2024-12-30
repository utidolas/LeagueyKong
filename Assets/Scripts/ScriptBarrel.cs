using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBarrel : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collision.gameObject.CompareTag("RightGround"))
        {
            rb.AddForce(collision.transform.right * speed, ForceMode2D.Impulse);
        }else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collision.gameObject.CompareTag("LeftGround"))
        {
            rb.AddForce(collision.transform.right * -speed, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Destroy"))
        {
            Transform parentTransform = transform.parent;
            Destroy(parentTransform.gameObject);
        }
    }

}
