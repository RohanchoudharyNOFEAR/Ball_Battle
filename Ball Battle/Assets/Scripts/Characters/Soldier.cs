using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soldier : MonoBehaviour
{
    protected ISoldierState currentState;

    public ISoldierState GetCurrentState {  get { return currentState; } }
    public float spawnDelay = 0.5f;
    public float reactivateTime = 2.5f;
    public Renderer rend;
    public Color originalColor;
    public Color inactiveColor = Color.gray;

    public GameObject reactivateEffect;
    public GameObject activationEffect;
    public Transform arrowTransform;
    public Animator anim;

    public virtual void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        originalColor = rend.material.color;
       
        anim = GetComponent<Animator>();
        TransitionToState(new InactiveState(spawnDelay));
    }

    public virtual void Update()
    {
        currentState?.Update(this);

        if (arrowTransform)
            arrowTransform.forward = transform.forward;
    }

    public void TransitionToState(ISoldierState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    public void SetColor(bool active)
    {
        rend.material.color = active ? originalColor : inactiveColor;
    }

    public void PlayEffect(GameObject effectPrefab)
    {
        if (effectPrefab)
            Instantiate(effectPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
    }

    public abstract ISoldierState GetInitialActiveState();
}
