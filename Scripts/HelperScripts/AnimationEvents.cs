using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController playerController;
    private Animator anim;
    public MainMenuController mainMenuController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void ResetShooting()
    {
        playerController.CanShoot = true;
        anim.Play("Idle");
    }

    void CamstartGame()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
