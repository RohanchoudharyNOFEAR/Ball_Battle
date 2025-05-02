using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public float energy = 0f;
    public float maxEnergy = 6f;
    public float regenRate = 0.5f;

    void Update()
    {
        energy = Mathf.Min(maxEnergy, energy + regenRate * Time.deltaTime);
    }

    public bool TryUseEnergy(float cost)
    {
        if (energy >= cost)
        {
            energy -= cost;
            return true;
        }
        return false;
    }
}
