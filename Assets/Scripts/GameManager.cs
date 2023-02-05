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

    public bool CanRotate { get; set; }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
#if UNITY_EDITOR
        Application.targetFrameRate = 144;
#endif
        Instance = this;
        DontDestroyOnLoad(gameObject);
        obstacles = new List<Obstacle>();
    }

    public void Rotate()
    {
        player.Rotate();
        cog.Rotate();
        decorationCog.Rotate();
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
        if (ctx.performed && CanRotate) Rotate();
    }

}
