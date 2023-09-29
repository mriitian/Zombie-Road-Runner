using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 Speed;
    public float xSpeed = 8f, zSpeed = 15f;
    public float accelerated = 20f, deccelerated = 15f;
    protected float RotationSpeed = 10f;
    protected float MaxAngle = 10f;
    public float low_Sound_Pitch, normal_Sound_Pitch, high_Sound_Pitch;
    public AudioClip EngineOn_sound, EngineOff_sound;
    private bool Isslow;
    private AudioSource Soundmanager;
    public void Awake()
    {
        Speed = new Vector3(0f, 0f, zSpeed);
        Soundmanager = GetComponent<AudioSource>();

        if (Soundmanager == null)
        {
            // If the AudioSource component is not found, add it to the same GameObject
            Soundmanager = gameObject.AddComponent<AudioSource>();
        }
    }
    public void Start()
    {

    }

    // Update is called once per frame
    public void MoveLeft()
    {
        Speed = new Vector3(-xSpeed / 2, 0f, Speed.z);
    }
    public void MoveRight()
    {
        Speed = new Vector3(xSpeed / 2, 0f, Speed.z);
    }
    public void MoveStraight()
    {
        Speed = new Vector3(0f, 0f, Speed.z);
    }
    public void MoveNormal()
    {
        
        Speed = new Vector3(Speed.x, 0f, Speed.z);
    }
    public void Moveslow()
    {
        
        Speed = new Vector3(Speed.x, 0f, deccelerated);
    }

    public void MoveFast()
    {
        Speed = new Vector3(Speed.x, 0f, accelerated);
    }

}
