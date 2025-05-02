using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance;
    public bool isRush = false;
    public float energy = 0f;
    public float maxEnergy = 6f;
    public float regenRate = 0.5f;

    void Awake() => Instance = this;

    void Update()
    {
        float rate = isRush ? regenRate * 2f : regenRate;
        energy = Mathf.Min(maxEnergy, energy + rate * Time.deltaTime);
    }

    public void SetRushTime(bool active)
    {
        isRush = active;
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
