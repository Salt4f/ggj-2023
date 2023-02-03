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
    public CircleObstacleGenerator cog;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        obstacles = new List<Obstacle>();
    }

    public void Rotate()
    {
        cog.Rotate();
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
        if (ctx.performed) Rotate();
    }

}
