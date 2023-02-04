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
    public float rotationSpeed = 1f;

    public uint[] chancesToNotSpawn = { 0, 10, 20, 30, 100 };

    private uint obstaclesInsertedInARow;
    private Quaternion targetRotation;
    private bool canRotate;

    private void Awake()
    {
        canRotate = true;
        obstaclesInsertedInARow = 0;
        var gm = GameManager.Instance;
        gm.cog = this;
    }

    private void FixedUpdate()
    {
        if (!canRotate)
        {
            // Try to replace with Quaternion.Lerp
            var delta = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            if (delta.eulerAngles.y == targetRotation.eulerAngles.y)
            {
                canRotate = true;
                GenerateObstacle();
            }
            transform.rotation = delta;
        }
    }

    public void Rotate()
    {
        if (!canRotate) return;
        canRotate = false;
        targetRotation = transform.rotation * Quaternion.Euler(0, stepRotation, 0);
    }

    private void GenerateObstacle()
    {
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
        GameManager.Instance.NotifyObstacles();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnPoint.magnitude);
    }
#endif

}
