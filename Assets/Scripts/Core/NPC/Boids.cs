using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    [SerializeField] private BoidsUnit _unitPrefap;
    [SerializeField] private float _spawnRange = 30;
    public int BoidCount;

    private void Start()
    {
        for (int i = 0; i < BoidCount; i++)
        {
            Vector3 rand = Random.insideUnitCircle;
            rand *= _spawnRange;

        }
    }
}
