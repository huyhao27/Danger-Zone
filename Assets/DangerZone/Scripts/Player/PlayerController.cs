using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class NodeMapping
    {
        public string keyName;
        public Transform nodeTransform;
    }

    [Header("Node configuration")] [SerializeField]
    private List<NodeMapping> nodeMappings;

    [Header("Component refrences")] [SerializeField]
    private MovementComponent movementComponent;

    // [SerializeField] private SkillExecutor skillExecutor; // Sẽ dùng sau này

    private void OnEnable()
    {
        // Listen event from input manager
        InputManager.OnMoveNodePressed += HandleMoveNodeInput;
        InputManager.OnSkillButtonPressed += HandleSkillButtonInput;
    }

    private void OnDisable()
    {
        InputManager.OnMoveNodePressed -= HandleMoveNodeInput;
        InputManager.OnSkillButtonPressed -= HandleSkillButtonInput;
    }

    // --- Event Handlers ---
    private void HandleMoveNodeInput(string receivedKeyName)
    {
        NodeMapping targetMapping =
            nodeMappings.FirstOrDefault(mapping => mapping.keyName.ToLower() == receivedKeyName.ToLower());
        if (targetMapping != null && targetMapping.nodeTransform != null)
        {
            //Move the player to the target node
            if(movementComponent!= null)
            {
                movementComponent.MoveTo(targetMapping.nodeTransform.position);
            }
            else
            {
                Debug.LogWarning("MovementComponent is not assigned in PlayerController.");
            }
        }
        else
        {
            Debug.LogWarning("Node mapping not found for key: " + receivedKeyName);
        }
    }
    
    private void HandleSkillButtonInput()
    {
        // Execute skill logic here
        // if (skillExecutor != null)
        // {
        //     skillExecutor.ExecuteSkill();
        // }
        // else
        // {
        //     Debug.LogWarning("SkillExecutor is not assigned in PlayerController.");
        // }
        
        Debug.Log("Skill button pressed, but skill execution is not implemented yet.");
    }
}