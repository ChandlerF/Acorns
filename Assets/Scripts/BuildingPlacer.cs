using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BuildingPlacer : NetworkBehaviour
{


    [SerializeField] private GameObject _building1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);


            if (!IsServer)
                SpawnBuildingServerRpc(spawnPos);
            else
                SpawnBuildingClient(spawnPos);
        }
    }





    public void SpawnBuildingClient(Vector3 pos)
    {
        GameObject spawnedBuilding = Instantiate(_building1, pos, Quaternion.identity);
        spawnedBuilding.GetComponent<NetworkObject>().Spawn(true);
    }


    [ServerRpc]
    public void SpawnBuildingServerRpc(Vector3 pos)
    {
        SpawnBuildingClient(pos);
    }
}
