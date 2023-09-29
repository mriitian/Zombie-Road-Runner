using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public TextMeshProUGUI Highscore;

    private void Start()
    {
        Highscore.text = "Highscore: " + GameplayController.instance.highScore;
    }
    public void PLayGame()
    {
        
        anim.Play("MainCamAnim");
    }

    public void Settings()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void Level1 ()
    {
        anim.Play("MainCamAnim");
        StartCoroutine(Waitforanim());
        SceneManager.LoadScene("GamePlay");
    }
    public void Level2 ()
    {
        anim.Play("MainCamAnim");
        StartCoroutine(Waitforanim());
        SceneManager.LoadScene("NewLevel");
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator Waitforanim()
    {
        yield return new WaitForSeconds(2f);
    }


}
