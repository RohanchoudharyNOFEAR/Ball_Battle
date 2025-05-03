using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance;
    public bool isRush = false;
    public float PlayerEnergy = 0f;
    public float EnemyEnergy = 0f;
    public float maxEnergy = 6f;
    public float regenRate = 0.5f;

    public float GetPlayerNormalizedEnergy() => PlayerEnergy / maxEnergy;
    public float GetEnemyNormalizedEnergy() => EnemyEnergy / maxEnergy;
    void Awake() => Instance = this;

    void Update()
    {
        float rate = isRush ? regenRate * 2f : regenRate;
        PlayerEnergy = Mathf.Min(maxEnergy, PlayerEnergy + rate * Time.deltaTime);
        EnemyEnergy = Mathf.Min(maxEnergy, EnemyEnergy + rate * Time.deltaTime);
    }

    public void SetRushTime(bool active)
    {
        isRush = active;
    }

    public bool TryUseEnergy(float cost)
    {
        if (PlayerEnergy >= cost)
        {
            PlayerEnergy -= cost;
            return true;
        }
        return false;
    }
    public bool TryUseEnemyEnergy(float cost)
    {
        if (EnemyEnergy >= cost)
        {
            EnemyEnergy -= cost;
            return true;
        }
        return false;
    }
}
