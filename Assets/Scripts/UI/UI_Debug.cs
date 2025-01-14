using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Debug : SingletonMonobehavior<UI_Debug>
{
    public CharacterBase TargetCharacter;

    public TMP_Text TargetCharacterText;
    public TMP_Text TargetCharacterInputMoveText;
    public TMP_Text TargetCharacterVelocityText;
    public TMP_Text TargetCharacterCurrentStateText;
    public TMP_Text TargetCharacterWaterState;
    public TMP_Text TargetCharacterHealthData;
    public TMP_Text TargetCharacterOxygenData;
    public TMP_Text TargetCharacterDashStatus;
    public TMP_Text TargetCharacterDash;
    public TMP_Text TargetCharacterAttack;

    public TMP_Text InputManagerAttack;
    public TMP_Text InputManagerDash;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateCharacter();
    }

    void UpdateCharacter()
    {
        TargetCharacterText.text = "Character Object Name: " + TargetCharacter?.name;
        TargetCharacterInputMoveText.text = "Input: " + TargetCharacter?.Input.ToString();
        TargetCharacterVelocityText.text = "Velocity: " + TargetCharacter?.Velocity.ToString();
        TargetCharacterCurrentStateText.text = "CurrentState: " + TargetCharacter?.StateMachine.GetCurrentStateName();
        TargetCharacterWaterState.text = "IsOnWater: " + TargetCharacter?.IsOnWater.ToString() + " Influence Water: " + TargetCharacter?.IsInfluenceByFlowWater.ToString();
        TargetCharacterHealthData.text = "MaxHealth: " + TargetCharacter?.MaxHealth.ToString() + " CurrentHealth: " + TargetCharacter?.Health.ToString();

        if(TargetCharacter.TryGetComponent<IBreath>(out IBreath iBreath))
        {
            TargetCharacterOxygenData.text = "MaxOxygen: " + iBreath.MaxOxygen.ToString() + " CurrentOxygen: " + iBreath.Oxygen.ToString();
        }

        TargetCharacterDashStatus.text = "MaxDashCount: " + TargetCharacter.MaxDashCount + " CurrentDashCount: " + TargetCharacter.DashCount;
        TargetCharacterDash.text = "DashDurationRemain: " + TargetCharacter.DashCooldownRemain + " CanDash: " + TargetCharacter.CanDash.ToString();
        TargetCharacterAttack.text = "AttackDurationRemain: " + TargetCharacter.AttackCooldownRemain + " CanDash: " + TargetCharacter.CanAttack.ToString();
    }

    void UpdateInputManager()
    {
        InputManagerAttack.text = "InputManager - Attack: " + InputManager.AttackKeyDown.ToString();
        InputManagerDash.text = "InputManager - Dash: " + InputManager.DashKeyDown.ToString();
    }
}
