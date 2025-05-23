using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance;

    private bool isRush = false;
    private float PlayerEnergy = 0f;
    private float EnemyEnergy = 0f;
    [Header("Energy Attributes Variables")]
    [SerializeField] private float maxEnergy = 6f;
    [SerializeField] private float regenRate = 0.5f;

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
