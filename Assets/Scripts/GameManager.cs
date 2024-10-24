using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [SerializeField] private GameObject gameOverScreen;

    public float bulletDamage = 10;
    private PlayerMovement player;


    private void Awake()
    {
        Time.timeScale = 0;
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameStart()
    {
        Time.timeScale = 1;
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        NewGame();
    }

    public void NewGame()
    {

        enabled = true;

        player.gameObject.SetActive(true);
        gameOverScreen.gameObject.SetActive(false);

    }

    public void GameOver()
    {
        enabled = false;

        player.gameObject.SetActive(false);
        Time.timeScale = 0;
        gameOverScreen.SetActive(true); 

    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("New");
        Time.timeScale = 1;

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}