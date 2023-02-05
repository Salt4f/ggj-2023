using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject hud;
    public TMP_Text stepsCounter;

    public GameObject deathPanel;
    public TMP_Text stepsDeathCounter;
    public AudioSource clickSource;

    private float timer;
    private readonly float clickDuration = 0.5f;

    private int sceneToLoad;

    private void Awake()
    {
        GameManager.Instance.ui = this;
        sceneToLoad = -1;
        SetSteps(0);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public void SetSteps(uint steps)
    {
        stepsCounter.text = "Steps: " + steps.ToString();
    }

    public void OpenDeadPanel()
    {
        deathPanel.SetActive(true);
        stepsDeathCounter.text = stepsCounter.text;
        hud.SetActive(false);
    }

    public void OnMainMenu()
    {
        if (sceneToLoad != -1) return;
        sceneToLoad = 0;
        timer = clickDuration;
        clickSource.Play();
    }

    public void OnRestart()
    {
        if (sceneToLoad != -1) return;
        sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        timer = clickDuration;
        clickSource.Play();
    }

}