using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsUnit : MonoBehaviour
{
    [Header("Info")]
    private Boids _boids;
    List<BoidsUnit> _neighbors = new List<BoidsUnit>();

    Vector3 _targetVector;
    Vector3 _egoVector;
    float _speed;

    float _additionalSpeed;
    bool _isEnemy;

    MeshRenderer _myMeshRenderer;
    TrailRenderer _trailRenderer;
    [SerializeField] private Color _color;

    [Header("Neighbor")]
    [SerializeField] private float _obstacleDist;
    [SerializeField] private float _fovAngle;
    [SerializeField] private float _maxNeighbourCount = 50;
    [SerializeField] private float _neighborDis = 10;

    [Header("ETC")]
    [SerializeField] private LayerMask _boidLayer;  
    [SerializeField] private LayerMask _obstacleLayer;

    Coroutine _findNeighbourCoroutine;
    Coroutine _calculateEgoVectorCoroutine;

    public void Init(Boids boids, float speed, int myIndex)
    {
        _boids = boids;
        _speed = speed;


    }
}
