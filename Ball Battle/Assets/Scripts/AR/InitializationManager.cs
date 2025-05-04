using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static SoldierSpawner;

public class InitializationManager : MonoBehaviour
{

    public static InitializationManager instance;
    public bool initialized = false;
    public GameObject Field;
    private ARRaycastManager raycastManager;

    public TMP_Text uitext1;
    public TMP_Text uitext2;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        if (raycastManager == null)
        {
            raycastManager = GameManager.Instance.raycastManager;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Field == null && initialized == false)
        {
            HandleTap();
        }
    }

    void HandleTap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (uitext1 != null)
        {
            uitext1.text = ray.ToString();
        }
        // For AR, use raycasting to find the point in the real world
        if (raycastManager != null)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                if (uitext2 != null)
                {
                    uitext2.text = hitPose.ToString();
                }
                SpawnField(Field, hitPose.position);



            }
        }
    }

    void SpawnField(GameObject prefab, Vector3 spawnPosition)
    {
        Vector3 adjustedPosition = new Vector3(spawnPosition.x, 1.5f, spawnPosition.z); // adjust height as needed
        Instantiate(prefab, adjustedPosition, Quaternion.identity);
        initialized = true;
    }
}
