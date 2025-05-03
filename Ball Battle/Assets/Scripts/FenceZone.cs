using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceZone : MonoBehaviour
{
    public bool isEnemyGoal; // true = enemy gate = player wins if reached
    public GameObject ParticleEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attacker") && isEnemyGoal)
        {
          
            VFXManager.Instance.PlayEffect(ParticleEffect, transform.position, 3);
            Destroy(other.gameObject);

            
        }
    }
}
