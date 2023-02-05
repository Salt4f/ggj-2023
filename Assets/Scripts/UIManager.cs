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


    private void Awake()
    {
        GameManager.Instance.ui = this;
        SetSteps(0);
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
        SceneManager.LoadScene(0);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}