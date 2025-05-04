using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SoldierSpawner : MonoBehaviour
{
    public GameObject attackerPrefab;
    public GameObject defenderPrefab;
    public LayerMask landFieldLayer;
    public float rayLength = 100f;
    public float spawnYPos = 1.5f;
    public enum TurnType { Attacker, Defender }
    public TurnType currentTurn;

    public EnergySystem energySystem;

    public Inputaction controls;

   [SerializeField] private bool tapped = false;
    private bool canSpawn = true;

    private void Awake()
    {
        controls = new Inputaction();
    }
    private void Start()
    {
      
    }

    void Update()
    {
        if ( InitializationManager.instance.initialized && tapped && canSpawn)
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

   

    void SpawnSoldier(GameObject prefab, Vector3 spawnPosition)
    {
        Vector3 adjustedPosition = new Vector3(spawnPosition.x, spawnYPos, spawnPosition.z); // adjust height as needed
        Instantiate(prefab, adjustedPosition, Quaternion.identity);
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
