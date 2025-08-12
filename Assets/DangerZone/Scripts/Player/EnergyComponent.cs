using UnityEngine;
using System;

public class EnergyComponent : MonoBehaviour
{
    public float maxEnergy = 100f;
    private float currentEnergy;
    
    public event Action<float, float> OnEnergyChanged;
    private void Start()
    {
        currentEnergy = maxEnergy; 
        OnEnergyChanged?.Invoke(currentEnergy, maxEnergy);
    }

    public void SpendEnergy(float amount)
    {
        currentEnergy -= amount;
        if (currentEnergy < 0) currentEnergy = 0;
        OnEnergyChanged?.Invoke(currentEnergy, maxEnergy);
        Debug.Log("Energy spent: " + amount + ", Current Energy: " + currentEnergy);
    }
    
    public bool HasEnoughEnergy(float amount)
    {
        return currentEnergy >= amount;
    }
}
