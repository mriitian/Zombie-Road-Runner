using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody mybody;
    public void Move(float speed)
    {
        mybody.AddForce(transform.forward.normalized *  speed);
        Invoke("Deactivate", 1.5f);
    }
    // Update is called once per frame
    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
    }
}
