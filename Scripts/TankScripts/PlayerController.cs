using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    // Start is called before the first frame update
    private Rigidbody mybody;
    public Transform Bullet_Point;
    public GameObject Bullet_Body;
    public ParticleSystem ShootFX;
    private Animator shootSlideranim;
    public Button rightbutton;
    public Button leftbutton;
    [HideInInspector]
    public bool CanShoot;
    new void Awake()
    {
        Speed = new Vector3(0f, 0f, zSpeed);
    }
    new void Start()
    {
        mybody = GetComponent<Rigidbody>();
        shootSlideranim = GameObject.Find("Fire Bar").GetComponent<Animator>();
        GameObject.Find("ShootButton").GetComponent<Button>().onClick.AddListener(ShootingControl);
        CanShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
        ChangePlayerRotation();
    }

    void FixedUpdate()
    {
        MoveTank();
    }

    void MoveTank()
    {
        mybody.MovePosition(mybody.position + Speed * Time.deltaTime);
    }

    void ControlMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Moveslow();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            MoveStraight();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            MoveStraight();
        }
         if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            MoveStraight();
        }
        

    }
    void ChangePlayerRotation()
    {
        if(Speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, MaxAngle, 0f), Time.deltaTime * RotationSpeed);
        }
        else if(Speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -MaxAngle, 0f), Time.deltaTime * RotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * RotationSpeed);
        }
    }

    public void ShootingControl()
    {
        if(Time.timeScale != 0)
        {
            if (CanShoot)
            {
                GameObject bullet = Instantiate(Bullet_Body, Bullet_Point.transform.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().Move(2000f);
                ShootFX.Play();

               
                CanShoot = false;
                shootSlideranim.Play("FadeIn");
            }
        }
    }

    public void LeftMove()
    {
        Speed = new Vector3(-xSpeed / 2, 0f, Speed.z);
        StartCoroutine(wait());
        ChangePlayerRotation();
        //
    }
    public void RightMove()
    {
        Speed = new Vector3(xSpeed / 2, 0f, Speed.z);
        StartCoroutine(wait());
        ChangePlayerRotation();
        //StraightMove();
    }
    public void StraightMove()
    {
        Speed = new Vector3(0f, 0f, Speed.z);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        StraightMove();
    }
}
