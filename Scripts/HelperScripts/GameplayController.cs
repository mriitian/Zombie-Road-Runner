using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameplayController instance;
    public GameObject[] ObstaclePrefabs;
    public GameObject[] ZombiePrefabs;
    public Transform[] Lanes;
    public float Max_Obsdelay = 40f, Min_ObsDelay = 10f;
    private float halfGroundSize;
    private BaseController playercontroller;
    public Button ShootButton;
    private TextMeshProUGUI Scoretext;
    public AudioSource AudioPlayer;
    private int Zombiekillcount;
    [SerializeField]
    private GameObject gameover_Panel; 
    [SerializeField]
    private GameObject pause_Panel;
    [SerializeField]
    private TextMeshProUGUI Final_Score;
    public int highScore;
    void Awake()
    {
        MakeInstance();
    }
    void Start()
    {
        halfGroundSize = 100f;
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();
        StartCoroutine("GenerateObstacles");
        Scoretext = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int newScore = Zombiekillcount; // Example new high score

        if (newScore > highScore)
        {
            // Update the high score if the new score is higher
            highScore = newScore;

            // Save the new high score to PlayerPrefs
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save(); // This line is optional, as PlayerPrefs usually saves automatically, but it's good practice to call Save() to be sure.
        }
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }else if(instance!= null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateObstacles()
    {
        float timer = Random.Range(Min_ObsDelay, Max_Obsdelay) / playercontroller.Speed.z;
        yield return new WaitForSeconds(timer);
        CreateObs(playercontroller.gameObject.transform.position.z + halfGroundSize);

        StartCoroutine("GenerateObstacles");
    }

    void CreateObs(float zPos)
    {
        int r = Random.Range(0, 10);

        if(0 <= r && r < 7)
        {
            int ObstacleLane = Random.Range(0, Lanes.Length);

            AddObstacle(new Vector3(Lanes[ObstacleLane].transform.position.x, 0.35f, zPos), Random.Range(0, ObstaclePrefabs.Length));
            int zombieLane = 0;

            if (ObstacleLane == 0)
            {
                zombieLane = Random.Range (0, 2) == 1 ? 1 : 2;
            }
            else if(ObstacleLane == 1)
            { 
                zombieLane = Random.Range (0, 2) == 1? 0 : 2;
            } else if(ObstacleLane == 2)
            { 
                zombieLane = Random.Range (0, 2) == 1? 0 : 1;
            }
            
             AddZombies(new Vector3(Lanes[ObstacleLane].transform.position.x, 0.15f, zPos));
        }
    }

    void AddObstacle(Vector3 position, int type)
    {
        GameObject Obstacle = Instantiate(ObstaclePrefabs[type], position, Quaternion.identity);
        bool mirror = Random.Range(0, 2) == 1;

        switch(type)
        {
            case 0:
                Obstacle.transform.rotation = Quaternion.Euler(0f, mirror?-20 : 20, 0f);
                break;

            case 1:
                Obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            
            case 2:
                Obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -1 : 1, 0f);
                break;

            case 3:
                Obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -170 : 170, 0f);
                break;
        }

        Obstacle.transform.position = position;
    }

    void AddZombies(Vector3 pos)
    {
        int Count = Random.Range (0, 3) + 1;
        for(int i = 0;i< Count; i++)
        {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f),0f, Random.Range(1f, 10f) ) * i;
            Instantiate(ZombiePrefabs[Random.Range(0, ZombiePrefabs.Length)], pos + shift * i, Quaternion.identity);
        }
    }

    public void increaseScore()
    {
        Zombiekillcount++;
        Scoretext.text = "killed:" + Zombiekillcount.ToString();
    }

    public void PauseGame()
    {
        GameObject.Find("ShootButton").SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Pause();
        pause_Panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("Game is REsume");
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().UnPause();

        ShootButton.gameObject.SetActive(true);
        pause_Panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void Gameover()
    {
        Time.timeScale = 0f;
        gameover_Panel.SetActive(true);
        ShootButton.gameObject.SetActive(false);
        Final_Score.text = "Killed:" + Zombiekillcount.ToString();
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Pause();
      //  GameObject.FindGameObjectWithTag("AudioPlayer").SetActive(true);
       // GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioSource>().Play();
        AudioPlayer.gameObject.SetActive(true);

    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamePlay");
    }
    void OnDestroy()
    {
        // Always save the high score when the game object is destroyed (e.g., when the game quits)
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
