using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int maxRotations;
    private bool setToDestroy;

    private void Awake()
    {
        GameManager.Instance.obstacles.Add(this);
    }

    private void LateUpdate()
    {
        if (setToDestroy) Destroy();
    }

    public void SetMaxRotations(int rotations)
    {
        maxRotations = rotations;
    }

    public void Rotate()
    {
        if (--maxRotations <= 0)
        {
            setToDestroy = true;
        }
    }

    private void Destroy()
    {
        GameManager.Instance.obstacles.Remove(this);
        Destroy(gameObject);
    }
}
