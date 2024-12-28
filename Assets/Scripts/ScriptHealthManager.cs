using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptHealthManager : MonoBehaviour
{
    // Calling outer scripts
    public ScriptGameManager gameManager;

    // Public vars
    public int health;
    public int maxHealth;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] hearts;

    private void Awake()
    {
        gameManager = FindObjectOfType<ScriptGameManager>();
    }

    private void Update()
    {

        health = gameManager.lives;
        // Checking hearts in array
        for (int i = 0; i < hearts.Length; i++)
        {

            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Display all hearts in array
            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
