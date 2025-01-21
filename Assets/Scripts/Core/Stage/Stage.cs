using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Transform _spawnPoint;

    private void Awake()
    {
        _spawnPoint = transform.GetChild(0);
    }

    public Vector3 GetSpawnPoint()
    {
        return _spawnPoint.position;
    }
}
