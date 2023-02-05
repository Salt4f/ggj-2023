using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;
    public AudioSource clickSource;

    private float timer;
    private readonly float clickDuration = 0.5f;

    private int sceneToLoad;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        sceneToLoad = -1;
    }

    private void Update()
    {
        if (timer > 0 && sceneToLoad != -1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public void OnStart()
    {
        timer = clickDuration;
        sceneToLoad = 1;
        clickSource.Play();
    }

    public void OnCreditsOpen()
    {
        credits.SetActive(true);
        clickSource.Play();
    }

    public void OnCreditsClose()
    {
        credits.SetActive(false);
        clickSource.Play();
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
