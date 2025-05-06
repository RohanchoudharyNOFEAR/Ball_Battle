using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SoldierSpawner : MonoBehaviour
{
    [Header("Spawn Specific Variables")]
    [SerializeField] private GameObject attackerPrefab;
    [SerializeField] private GameObject defenderPrefab;
    [SerializeField] private LayerMask landFieldLayer;
    [SerializeField] private float rayLength = 100f;
    [SerializeField] private float spawnYPos = 1.5f;
   
    [Header("Components")]
    [SerializeField] private EnergySystem energySystem;


    private Inputaction controls;
    private bool canSpawn = true;
    private bool tapped = false;
    
    public enum TurnType { Attacker, Defender }
    public TurnType currentTurn;
    public static Action<GameObject> SoldierSpawnEvent;

    private void Awake()
    {
        controls = new Inputaction();
    }
    private void Start()
    {

    }

    void Update()
    {
        if (InitializationManager.instance.GetInitilized && tapped && canSpawn)
        {
            HandleTap();
        }
    }

    private void OnEnable()
    {

        controls.Enable();
        controls.Player.Touch.performed += ctx => tapped = true;
        controls.Player.Touch.canceled += ctx => tapped = false;
    }

    private void OnDisable()
    {
        controls.Player.Touch.performed -= ctx => tapped = true;
        controls.Player.Touch.canceled -= ctx => tapped = false;
        controls.Disable();
    }

    void HandleTap()
    {
        if (GameManager.Instance.currentSoldierCount < GameManager.Instance.MaxSoldierSpawn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());


            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, landFieldLayer))
            {
                string tag = hit.collider.tag;

                if (currentTurn == TurnType.Attacker && tag == "PlayerField")
                {
                    if (energySystem.TryUseEnergy(2))
                    {
                        SpawnSoldier(attackerPrefab, hit.point);
                    }
                }
                else if (currentTurn == TurnType.Defender && tag == "PlayerField")
                {
                    if (energySystem.TryUseEnergy(3))
                    {
                        SpawnSoldier(defenderPrefab, hit.point);
                    }
                }
                else if (currentTurn == TurnType.Attacker && tag == "EnemyField")
                {
                    if (energySystem.TryUseEnemyEnergy(3))
                    {
                        SpawnSoldier(defenderPrefab, hit.point);
                    }
                }
                else if (currentTurn == TurnType.Defender && tag == "EnemyField")
                {
                    if (energySystem.TryUseEnemyEnergy(2))
                    {
                        SpawnSoldier(attackerPrefab, hit.point);
                    }
                }
            }
        }
    }

    void SpawnSoldier(GameObject prefab, Vector3 spawnPosition)
    {
       
            Vector3 adjustedPosition = new Vector3(spawnPosition.x, spawnYPos, spawnPosition.z); // adjust height as needed
            GameObject soldier = Instantiate(prefab, adjustedPosition, Quaternion.identity);
            GameManager.Instance.currentSoldierCount++;
            if (SoldierSpawnEvent != null)
            {
                SoldierSpawnEvent.Invoke(soldier);
            }
            StartCoroutine(resetCanSpawn());
        
    }

    public void SetTurn(TurnType newTurn)
    {
        currentTurn = newTurn;
    }

    IEnumerator resetCanSpawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(1.5f);
        canSpawn = true;
    }
}
