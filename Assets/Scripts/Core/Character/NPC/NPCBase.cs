using System.Collections;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class NPCBase : CharacterBase, IHallucination
{
    [Header("Reference")]
    private NavMeshAgent _agent;
    private BehaviorGraphAgent _behaviourAgent;

    [Header("NPC Data")]
    public float PatrolSpeed;
    public float ChaseSpeed;
    public float AttackSpeed;

    public float AttackDistance;
    public float ChaseDistance;
    public float GiveupDistance;

    public float Damage;

    //Func
    #region Monobehaviour
    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _behaviourAgent = GetComponent<BehaviorGraphAgent>();

        InitialzeNPC();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        EventHandler.PlayerInHallucination -= OnHallucinationChanged;
        EventHandler.PlayerInHallucination += OnHallucinationChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        EventHandler.PlayerInHallucination -= OnHallucinationChanged;
    }
    #endregion

    #region IHallucination
    public void OnHallucinationChanged(bool active)
    {
        if(active) OnHallucinationBegin();
        else OnHallucinationEnd();
    }

    public virtual void OnHallucinationBegin()
    {
        
    }

    public virtual void OnHallucinationEnd()
    {
        
    }

    #endregion


    public void InitialzeNPC()
    {
        if (_behaviourAgent == null) return;

        _name = _name + gameObject.GetInstanceID().ToString();
        _behaviourAgent.SetVariableValue("AttackDist", AttackDistance);
        _behaviourAgent.SetVariableValue("ChaseDist", ChaseDistance);
        _behaviourAgent.SetVariableValue("GiveupDist", GiveupDistance);
        _behaviourAgent.SetVariableValue("Target", CharacterManager.GetCharacter("Player").GameObject);
    }
}
