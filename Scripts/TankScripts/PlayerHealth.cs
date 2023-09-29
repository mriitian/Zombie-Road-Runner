using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    private Slider healthSlider;

    private GameObject UI_Holder;
    void Start()
    {
        healthSlider = GameObject.Find("Health").GetComponent<Slider>();
        healthSlider.value = health;
        UI_Holder = GameObject.Find("UI Holder");
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        healthSlider.value = health;

        if(health == 0)
        {
            UI_Holder.SetActive(false);
            GameplayController.instance.Gameover();
        }
    }
}
