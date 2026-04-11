using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    [Header("Health Settings")]
    [SerializeField] public int health;              //current health
    [SerializeField] public int maxHealth = 3;       //max health

    [SerializeField] public Image[] hearts;
    [SerializeField] public Sprite fullHeart;
    [SerializeField] public Sprite emptyHeart;
    public bool alive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);

        health = maxHealth;
        alive = true;
    }

    //private void Update()
    //{
    //    Debug.Log("Testing only");
    //    Health();
    //}

    //player health function, controls player heart sprites
    public void Health()
    {
        if (health > maxHealth)
        {
            health = maxHealth;         //set player health = max amount of health player can have
        }

        for (int i = 0; i < hearts.Length; i++)        //count how many player hearth sprites they need
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            health = 0;
            alive = false;
            //can call game over menu
            gameOverPanel.SetActive(true);
        }
    }

    public void ReviveHealth()
    {
        health = maxHealth;
        alive = true;
        gameOverPanel.SetActive(false);
    }
}
