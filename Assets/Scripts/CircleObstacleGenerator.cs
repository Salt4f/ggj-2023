using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public bool isDecoration;

    private uint obstaclesInsertedInARow;
    private Quaternion targetRotation;

    private void Awake()
    {
        if (!isDecoration) GameManager.Instance.CanRotate = true;
        obstaclesInsertedInARow = 0;
        var gm = GameManager.Instance;
        if (isDecoration) gm.decorationCog = this;
        else gm.cog = this;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.CanRotate)
        {
            // Try to replace with Quaternion.Lerp
            var delta = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            if (delta.eulerAngles.y == targetRotation.eulerAngles.y)
            {
                if (!isDecoration) GameManager.Instance.CanRotate = true;
                GenerateObstacle();
            }
            transform.rotation = delta;
        }
    }

    public void Rotate()
    {
        if (!isDecoration) GameManager.Instance.CanRotate = false;
        targetRotation = transform.rotation * Quaternion.Euler(0, stepRotation, 0);
    }

    private void GenerateObstacle()
    {
        var chance = Random.Range(0, 99);
        if (chance >= chancesToNotSpawn[obstaclesInsertedInARow++])
        {
            int i = Random.Range(0, obstaclesPrefabs.Length);
            var newObstacle = obstaclesPrefabs[i];

            var obj = Instantiate(newObstacle, new Vector3(spawnPoint.x, transform.position.y, spawnPoint.y), Quaternion.identity, transform);
            var obs = obj.GetComponent<Obstacle>();
            obs.SetMaxRotations(obstaclesPerTurn + 1);
        }
        else
        {
            obstaclesInsertedInARow = 0;
        }
        if (!isDecoration) GameManager.Instance.NotifyObstacles();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnPoint.magnitude);
    }
#endif

}
