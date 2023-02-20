using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    public RectTransform pauseImage;
    public RectTransform pauseMenu;
    private GameObject player;
    public RectTransform soundMenu;
    public RectTransform screenMenu;

    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("x"),
            PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
        player.transform.eulerAngles = new Vector3(0f, PlayerPrefs.GetFloat("Cam_y"), 0f);
    }

  
    void Update()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }
    }

    public void Pause()
    {
        if(pauseImage.gameObject.activeInHierarchy == false)
        {
            if(pauseMenu.gameObject.activeInHierarchy==false)
            {
                pauseMenu.gameObject.SetActive(true);
            }
            pauseImage.gameObject.SetActive(true);
            Time.timeScale = 0f;
            player.SetActive(false);
        }
        else
        {
            pauseImage.gameObject.SetActive(false);
            Time.timeScale = 1f;
            player.SetActive(true);
        }
    }

    public void Sounds(bool open)
    {
        if (open)
        {
            soundMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }
        if (!open)
        {
            soundMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }
    public void ScreenSetting(bool open)
    {
        if (open)
        {
            soundMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(false);
            screenMenu.gameObject.SetActive(true);
        }
        if (!open)
        {
            soundMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
            screenMenu.gameObject.SetActive(false);
        }
    }
    public void SaveSetting(bool isQuit)
    {   //Set하면서 위에서 받은(Get)값을 가져옴
        PlayerPrefs.SetFloat("x", player.transform.position.x);
        PlayerPrefs.SetFloat("y", player.transform.position.y);
        PlayerPrefs.SetFloat("z", player.transform.position.z);
        PlayerPrefs.SetFloat("Cam_y", player.transform.eulerAngles.y);
        if (isQuit)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("StartScene_E");
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
