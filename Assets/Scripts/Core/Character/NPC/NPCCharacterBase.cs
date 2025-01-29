using System.Collections;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class NPCCharacterBase : MonoBehaviour, ICharacter, IHealth
{
    [Header("ICharacter")]
    [SerializeField] protected string _name = "NPCCharacterBase";

    [Header("Health")]
    [SerializeField] protected float _health = 100.0f;
    [SerializeField] protected float _maxHealth = 100.0f;
    [SerializeField] protected bool _imuttable = false;
    [SerializeField] protected bool _temporaryImuttable = false;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private BehaviorGraphAgent _behaviourAgent;

    //Getter or Setter
    #region ICharacter
    public string Name { get { return _name; } }
    #endregion

    #region IHealth
    public float Health { get => _health; }
    public float MaxHealth { get => _maxHealth; }
    public bool IsImuttable { get => _imuttable; }
    public bool IsTemporaryImuttable { get => _temporaryImuttable; set => _temporaryImuttable = value; }
    #endregion


    //Func
    #region Monobehaviour
    private void Awake()
    {
        _behaviourAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void Start()
    {
        _behaviourAgent.SetVariableValue("Target", CharacterManager.GetCharacter("Player").GameObject);
    }

    protected virtual void OnEnable()
    {
        RegisterNPC();
    }

    protected virtual void OnDisable()
    {
        DeregisterNPC();
    }

    #endregion

    #region ICharacter
    public void RegisterNPC()
    {
        CharacterManager.RegisterCharacter(gameObject, this);
    }

    public void DeregisterNPC()
    {
        CharacterManager.DeregisterCharacter(gameObject, this);
    }
    #endregion

    #region IHealth
    public void Die(ICharacter perp)
    {
        throw new System.NotImplementedException();
    }

    public void GetDamage(ICharacter character, float amount)
    {
        throw new System.NotImplementedException();
    }

    public void RecoverFullHealth()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
