using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Soldier : MonoBehaviour
{
    protected ISoldierState currentState;


    [SerializeField] private float spawnDelay = 0.5f;
    // [SerializeField] private float reactivateTime = 2.5f;

    public ISoldierState GetCurrentState { get { return currentState; } }
    public GameObject reactivateEffect;
    public GameObject activationEffect;
    public Animator anim;
    public CapsuleCollider collider;

    [SerializeField] protected Renderer rend;
    [SerializeField] protected Color inactiveColor = Color.gray;
    protected Color originalColor;  
    protected GameManager gameManager;

    public virtual void Start()
    {
        //   rend = GetComponentInChildren<Renderer>();
        originalColor = rend.material.color;


        collider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        TransitionToState(new InactiveState(spawnDelay));

        gameManager = GameManager.Instance;
    }

    public virtual void Update()
    {
        currentState?.Update(this);


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
