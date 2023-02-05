using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public IList<Obstacle> obstacles;
    public CircleObstacleGenerator decorationCog;
    public CircleObstacleGenerator cog;
    public Player player;
    public UIManager ui;

    private bool rotateInput;
    private bool isDead;

    public bool CanRotate { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
#if UNITY_EDITOR
        Application.targetFrameRate = 144;
#endif
        Instance = this;
        obstacles = new List<Obstacle>();
        isDead = false;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isDead) return;
        if (rotateInput && CanRotate)
        {
            Rotate();
        }
    }

    public void Rotate()
    {
        player.Rotate();
        cog.Rotate();
        decorationCog.Rotate();
    }

    public void Live()
    {
        isDead = false;
    }

    public void Die()
    {
        isDead = true;
        ui.OpenDeadPanel();
    }

    public void NotifyObstacles()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.Rotate();
        }
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) rotateInput = true;
        else if (ctx.canceled) rotateInput = false;
    }

}
