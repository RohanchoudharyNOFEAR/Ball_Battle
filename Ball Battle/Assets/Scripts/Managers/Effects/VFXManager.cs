using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

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
    }

    // Call this to play a VFX at a specific position with optional rotation and auto-destroy
    public void PlayEffect(GameObject effectPrefab, Vector3 position, Quaternion? rotation = null, float destroyAfter = 2f)
    {
        if (effectPrefab == null) return;

        Quaternion rot = rotation ?? Quaternion.identity;
        GameObject effect = Instantiate(effectPrefab, position, rot);
        Destroy(effect, destroyAfter);
    }

    // Optional overload if no rotation is needed
    public void PlayEffect(GameObject effectPrefab, Vector3 position, float destroyAfter = 2f)
    {
        PlayEffect(effectPrefab, position, Quaternion.identity, destroyAfter);
    }
}
