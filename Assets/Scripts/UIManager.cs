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


    private void Awake()
    {
        GameManager.Instance.ui = this;
        SetSteps(0);
    }

    public void SetSteps(uint steps)
    {
        stepsCounter.text = "Steps: " + steps.ToString();
    }

}