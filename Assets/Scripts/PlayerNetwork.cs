using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private Rigidbody2D _rb;

    private float _horizontal;
    private float _vertical;

    [SerializeField]  private float _speed = 8.5f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            this.enabled = false;
            return;
        }

        Camera.main.AddComponent<CameraFollow>().Player = transform;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * _speed, _vertical * _speed);
    }
}




/*
            GetComponent<PlayerStats>().Gold.Value ++;
            Debug.Log(OwnerClientId + " ; " + GetComponent<PlayerStats>().Gold.Value);
*/