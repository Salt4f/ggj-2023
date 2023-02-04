using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircleObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    public Vector2 spawnPoint;
    public int obstaclesPerTurn;
    public float stepRotation = 10f;

    public uint[] chancesToNotSpawn = { 0, 10, 20, 30, 100 };

    [SerializeField]
    private uint obstaclesInsertedInARow;
    private float currentRotation;

    private void Awake()
    {
        obstaclesInsertedInARow = 0;
        currentRotation = 0;
        var gm = GameManager.Instance;
        gm.cog = this;
    }

    public void Rotate()
    {
        var rot = 360f / obstaclesPerTurn;
        currentRotation += stepRotation;
        if (currentRotation > rot)
        {
            var delta = currentRotation - rot;
            transform.rotation *= Quaternion.Euler(0, stepRotation - delta, 0);

            // GENERATE OBSTACLE
            var chance = Random.Range(0, 99);
            if (chance >= chancesToNotSpawn[obstaclesInsertedInARow++])
            {
                int i = Random.Range(0, obstaclesPrefabs.Length);
                var newObstacle = obstaclesPrefabs[i];

                var obj = Instantiate(newObstacle, new Vector3(spawnPoint.x, 0, spawnPoint.y), Quaternion.identity, transform);
                var obs = obj.GetComponent<Obstacle>();
                obs.SetMaxRotations(obstaclesPerTurn + 1);
            }
            else
            {
                obstaclesInsertedInARow = 0;
            }

            // APPLY RESTANT ROTATION
            transform.rotation *= Quaternion.Euler(0, delta, 0);
            currentRotation = delta;

            GameManager.Instance.NotifyObstacles();
        } else
        {
            transform.rotation *= Quaternion.Euler(0, stepRotation, 0);
        }
        
    }

}
