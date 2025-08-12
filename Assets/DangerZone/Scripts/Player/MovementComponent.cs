using System;
using UnityEngine;
using DG.Tweening;

public class MovementComponent : MonoBehaviour
{
    // --- Inspector Fields ---
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float energyCostPerMove = 2f;
    
    [SerializeField] private Ease moveEase = Ease.Linear; // Linear, InQuad, OutBounce, etc.
    
    [Header("Component References")]
    [SerializeField] private EnergyComponent energyComponent;
    
    // --- Private Fields ---
    private Tween currentMoveTween;
    
    // --- Public Methods ---
    public void MoveTo(Vector3 targetPosition)
    {
        if(energyComponent!= null && !energyComponent.HasEnoughEnergy(energyCostPerMove))
        {
            Debug.LogWarning("Not enough energy to move.");
            return;
        }

        if (energyComponent != null)
        {
            energyComponent.SpendEnergy(energyCostPerMove);
        }
        
        currentMoveTween?.Kill(); // Stop any ongoing movement before starting a new one
        
        float distance = Vector3.Distance(transform.position, targetPosition);
        float duration = distance / speed;
        
        currentMoveTween = transform.DOMove(targetPosition, duration).SetEase(moveEase).OnComplete(() =>
        {
            // add logic to execute after movement completes
            currentMoveTween = null;
            Debug.Log("Movement completed to position: " + targetPosition);
        });
    }

    private void OnDestroy()
    {
        currentMoveTween?.Kill();
    }
}
