using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 3f;
    void Start()
    {
        Invoke("DeactivateGameObject", timer);
    }
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
