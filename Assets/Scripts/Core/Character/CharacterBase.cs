using Unity.Behavior;
using UnityEngine;

public class CharacterBase : MonoBehaviour, ICharacter, IHealth
{
    [Header("ICharacter")]
    [SerializeField] protected string _name = "NPCCharacterBase";

    [Header("Health")]
    [SerializeField] protected float _health = 100.0f;
    [SerializeField] protected float _maxHealth = 100.0f;
    [SerializeField] protected bool _imuttable = false;
    [SerializeField] protected bool _temporaryImuttable = false;

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
    public virtual void Die(ICharacter perp)
    {
        
    }

    public virtual void GetDamage(ICharacter character, float amount)
    {
        
    }

    public void RecoverFullHealth()
    {
        
    }
    #endregion
}
