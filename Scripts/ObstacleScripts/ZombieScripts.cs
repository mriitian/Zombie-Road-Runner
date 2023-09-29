using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScripts : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BloodFX;
    public float Speed = 1f;
    public Rigidbody mybody;
    private bool IsAlive;
    void Start()
    {
        mybody = GetComponent<Rigidbody>();
        IsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }
        if (IsAlive)
        {
            mybody.velocity = new Vector3(0f, 0f, -Speed);
        }
    }
    void Die()
    {
        IsAlive = false;
        mybody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("Idle");
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Player" || target.gameObject.tag == "Bullet")
        {
            Instantiate(BloodFX, transform.position, Quaternion.identity);
            Invoke("DeactivateGameObject", 3f);
            GameplayController.instance.increaseScore();
            Die();
        }
    }
}
