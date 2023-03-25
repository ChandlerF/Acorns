using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingPlacer : NetworkBehaviour
{


    [SerializeField] private GameObject _selectedBuilding;

    private GhostBuilding _ghostScript;
    private GameObject _ghostBuilding;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            this.enabled = false;
            return;
        }
    }




    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<PlayerStats>().Gold.Value >= _selectedBuilding.GetComponent<Buildings>().Price)
        {
            SpawnGhostBuilding();
        }
        else if (Input.GetMouseButtonUp(0) && _ghostBuilding != null)
        {
            Destroy(_ghostBuilding);
            _ghostBuilding = null;


            if (_ghostScript.CanPlace)   PlaceBuilding();
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<PlayerStats>().Gold.Value++;
        }
    }


    



    private void SpawnGhostBuilding()
    {
        _ghostBuilding = new GameObject();
        _ghostScript = _ghostBuilding.AddComponent<GhostBuilding>();
        _ghostScript.Initialize(_selectedBuilding);
    }


    private void PlaceBuilding()
    {
        GetComponent<PlayerStats>().Gold.Value -=  _selectedBuilding.GetComponent<Buildings>().Price;



        Vector3 spawnPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);


        if (!IsServer)
            SpawnBuildingServerRpc(spawnPos);
        else
            SpawnBuildingClient(spawnPos);
    }



    public void SpawnBuildingClient(Vector3 pos)
    {
        GameObject spawnedBuilding = Instantiate(_selectedBuilding, pos, Quaternion.identity);
        spawnedBuilding.GetComponent<NetworkObject>().Spawn(true);
    }


    [ServerRpc]
    public void SpawnBuildingServerRpc(Vector3 pos)
    {
        SpawnBuildingClient(pos);
    }
}
