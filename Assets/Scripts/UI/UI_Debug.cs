using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Debug : MonoBehaviour
{
    public CharacterBase TargetCharacter;

    public TMP_Text TargetCharacterText;
    public TMP_Text TargetCharacterVelocityText;
    public TMP_Text TargetCharacterCurrentStateText;
    public TMP_Text TargetCharacterWaterState;
    public TMP_Text TargetCharacterHealthData;
    public TMP_Text TargetCharacterOxygenData;

    public TMP_Text InputManagerAttack;
    public TMP_Text InputManagerDash;
    public TMP_Text InputManagerInput;

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
        TargetCharacterVelocityText.text = "Velocity: " + TargetCharacter?.Velocity.ToString();
        TargetCharacterCurrentStateText.text = "CurrentState: " + TargetCharacter?.StateMachine.GetCurrentStateName();
        TargetCharacterWaterState.text = "IsOnWater: " + TargetCharacter?.IsOnWater.ToString() + " Influence Water: " + TargetCharacter?.IsInfluenceByFlowWater.ToString();
        TargetCharacterHealthData.text = "MaxHealth: " + TargetCharacter?.MaxHealth.ToString() + " CurrentHealth: " + TargetCharacter?.Health.ToString();

        if(TargetCharacter.TryGetComponent<IBreath>(out IBreath iBreath))
        {
            TargetCharacterOxygenData.text = "MaxOxygen: " + iBreath.MaxOxygen.ToString() + " CurrentOxygen: " + iBreath.Oxygen.ToString();
        }
    }
}
