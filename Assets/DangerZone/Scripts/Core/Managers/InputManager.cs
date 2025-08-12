using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // -- Singleton instance --
    public static InputManager Instance { get; private set; }

    // -- Input Actions --
    public static event Action<String> OnMoveNodePressed;
    public static event Action OnSkillButtonPressed;

    private PlayerControls playerControls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
        
        playerControls.Gameplay.Move.performed += HandleMoveInput;
        playerControls.Gameplay.Skill.performed += HandleSkillInput;
    }
    
    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
        
        playerControls.Gameplay.Move.performed -= HandleMoveInput;
        playerControls.Gameplay.Skill.performed -= HandleSkillInput;
    }

    private void HandleMoveInput(InputAction.CallbackContext context)
    {
        String bindingName = context.control.name;
        
        OnMoveNodePressed?.Invoke(bindingName);
        
        Debug.Log("Move Node Pressed: " + bindingName);
    }

    private void HandleSkillInput(InputAction.CallbackContext context)
    {
        OnSkillButtonPressed?.Invoke();
        
        Debug.Log("Skill Button Pressed");
    }
}
