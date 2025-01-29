using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RandomPatrol", story: "[Self] Navigate Random Position", category: "Action", id: "e05df250b6e2ce08356228511a8762ed")]
public partial class RandomPatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    private NavMeshAgent _agent;
    private Vector3 _patrolPos;
    private float _currentPatrolTime = 0;
    private float _maxPatrolTime = 5;

    protected override Status OnStart()
    {
        int jitterMin = 0;
        int jitterMax = 360;
        float targetRad = UnityEngine.Random.Range(2.5f, 6f);
        int targetJitter = UnityEngine.Random.Range(jitterMin, jitterMax);

        //위치 = agent위치 + jitter 사이즈의 원 둘레 위치
        _patrolPos = Self.Value.transform.position + Utils.GetPositionFromAngle(targetRad, targetJitter);
        _agent = Self.Value.GetComponent<NavMeshAgent>();
        _agent.SetDestination(_patrolPos);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if ((_patrolPos - Self.Value.transform.position).magnitude < 0.1f || Time.time - _currentPatrolTime > _maxPatrolTime)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }

    protected override void OnEnd()
    {
    }
}

