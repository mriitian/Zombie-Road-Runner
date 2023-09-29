using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ExplosionPrefab;
    public int Damage = 10;
    public void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Player")
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().ApplyDamage(Damage);
            gameObject.SetActive(false);
        }
        if(target.gameObject.tag == "Bullet")
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
          
            gameObject.SetActive(false);
        }


    }
}
