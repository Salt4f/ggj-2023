using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircleObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    public Vector2 spawnPoint;
    public int obstaclesPerTurn;
    public float stepRotation = 10f;

    private float currentRotation;

    private void Awake()
    {
        currentRotation = 0;
        var gm = GameManager.Instance;
        gm.cog = this;
        for (int i = 0; i < obstaclesPerTurn; ++i)
        {
            gm.Rotate();
        }
    }

    public void Rotate()
    {
        var rot = 360f / obstaclesPerTurn;
        currentRotation += stepRotation;
        if (currentRotation > rot)
        {
            transform.rotation *= Quaternion.Euler(0, stepRotation - (currentRotation - rot), 0);
            //GENERATE OBSTACLE AND APPLY RESTANT ROTATION
        }

        int i = Random.Range(0, obstaclesPrefabs.Length);
        var newObstacle = obstaclesPrefabs[i];

        var obj = Instantiate(newObstacle, new Vector3(spawnPoint.x, 0, spawnPoint.y), Quaternion.identity, transform);
        var obs = obj.GetComponent<Obstacle>();
        obs.SetMaxRotations(obstaclesPerTurn + 1);

        
    }

}
