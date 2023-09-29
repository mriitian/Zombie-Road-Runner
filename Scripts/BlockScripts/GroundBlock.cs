using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Otherblock;
    public float HalfLength = 100f;
    private Transform player;
    private float endoffset = 10f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveGround();
    }

    void MoveGround()
    {
        if(transform.position.z + HalfLength < player.position.z -endoffset)
        {
            transform.position = new Vector3(Otherblock.position.x, Otherblock.position.y, Otherblock.position.z + HalfLength * 2);
        }
    }
}//class
