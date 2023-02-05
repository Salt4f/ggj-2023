using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;

    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCreditsOpen()
    {
        credits.SetActive(true);
    }

    public void OnCreditsClose()
    {
        credits.SetActive(false);
    }
}
