using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private Rigidbody2D _rb;

    private float _horizontal;
    private float _vertical;

    [SerializeField]  private float _speed = 8.5f;
    [SerializeField] private GameObject _building1;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) this.enabled = false;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);


            if (!IsServer)
                SpawnBuildingServerRpc(spawnPos);
            else
                SpawnBuildingClient(spawnPos);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * _speed, _vertical * _speed);
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




/*
            GetComponent<PlayerStats>().Gold.Value ++;
            Debug.Log(OwnerClientId + " ; " + GetComponent<PlayerStats>().Gold.Value);
*/