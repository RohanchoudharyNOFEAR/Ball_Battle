using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//VFX Manager is global Singleton that you can call to play vfx by passing the vfx gameobject
//i am using singleton because its better for these types of global managers that follow single responsibilty priciple
// i use observer pattern in Audio Manager just to showcase the obser pettern but i prefer singletion for these types of managers
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
