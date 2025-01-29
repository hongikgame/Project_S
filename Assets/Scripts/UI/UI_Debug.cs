using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Debug : SingletonMonobehavior<UI_Debug>
{
    [SerializeField] private string _prefix = string.Empty;
    [SerializeField] private ConsoleCommand[] _commands = new ConsoleCommand[0];

    [Header("UI")]
    [SerializeField] private GameObject _debugCanvas;
    [SerializeField] private GameObject _consoleCanvas;
    [SerializeField] private TMP_InputField _consoleInputField;

    private static Console _console;

    private Console Console
    {
        get
        {
            if (_console == null) _console = new Console(_prefix, _commands);
            return _console;
        }
    }

    public PlayerCharacterBase TargetCharacter;

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

    #region Monobehaviour
    protected override void Awake()
    {
        base.Awake();
        _consoleInputField.onEndEdit.AddListener(ProcessCommand);
    }

    private void OnEnable()
    {
        InputManager.RegisterCommand(ConsoleToggle);
    }

    private void OnDisable()
    {
        InputManager.DeregisterCommand(ConsoleToggle);
    }
    #endregion

    public void DebugToggle()
    {
        if(_debugCanvas.activeSelf)
        {
            _debugCanvas.SetActive(false);
            StopAllCoroutines();
        }
        else
        {
            _debugCanvas.SetActive(true);
            StartCoroutine(DebugCoroutine());
        }
    }

    private void UpdateCharacter()
    {
        TargetCharacterText.text = "Character Object Name: " + TargetCharacter?.name;
        TargetCharacterInputMoveText.text = "Input: " + TargetCharacter?.Input.ToString();
        TargetCharacterVelocityText.text = "Velocity: " + TargetCharacter?.Velocity.ToString();
        TargetCharacterCurrentStateText.text = "CurrentState: " + TargetCharacter?.StateMachine.GetCurrentStateName();
        TargetCharacterWaterState.text = "IsOnWater: " + TargetCharacter?.CurrentDetectorData.IsOnWater.ToString() + " Influence Water: " + TargetCharacter?.IsInfluenceByFlowWater.ToString();
        TargetCharacterHealthData.text = "MaxHealth: " + TargetCharacter?.MaxHealth.ToString() + " CurrentHealth: " + TargetCharacter?.Health.ToString();

        if(TargetCharacter.TryGetComponent<IBreath>(out IBreath iBreath))
        {
            TargetCharacterOxygenData.text = "MaxOxygen: " + iBreath.MaxOxygen.ToString() + " CurrentOxygen: " + iBreath.Oxygen.ToString();
        }

        TargetCharacterDashStatus.text = "MaxDashCount: " + TargetCharacter.MaxDashCount + " CurrentDashCount: " + TargetCharacter.DashCount;
        TargetCharacterDash.text = "DashDurationRemain: " + TargetCharacter.DashCooldownRemain + " CanDash: " + TargetCharacter.CanDash.ToString();
        TargetCharacterAttack.text = "AttackDurationRemain: " + TargetCharacter.AttackCooldownRemain + " CanDash: " + TargetCharacter.CanAttack.ToString();
    }

    public void ConsoleToggle(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;

        if (_consoleCanvas.activeSelf)
        {
            InputManager.SetPlayerInputActivate(true);
            _consoleCanvas.SetActive(false);
        }
        else
        {
            InputManager.SetPlayerInputActivate(false);
            _consoleCanvas.SetActive(true);
            _consoleInputField.ActivateInputField();
        }
    }

    public void ProcessCommand(string input)
    {
        Console.ProcessCommand(input);
        _consoleInputField.text = string.Empty;
    }

    private IEnumerator DebugCoroutine()
    {
        UpdateCharacter();
        yield return null;
    }
}
