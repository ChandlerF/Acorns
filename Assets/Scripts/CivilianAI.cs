using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class CivilianAI : NetworkBehaviour
{
    NavMeshAgent _agent;
    private bool _sameTeam = false;


    public override async void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _agent = transform.AddComponent<NavMeshAgent>();
            _agent.speed = 3.5f;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            
        }

        //SetTarget(transform.position);
        if (!IsServer)
            WarpAgentServerRpc();
        else
            WarpAgentClientRpc(transform.position);
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Vector3 spawnPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            SetTarget(spawnPos);

        }
    }



    private void SetTarget(Vector3 pos)
    {
        _sameTeam = GameSettings.Instance.IsOnSameTeam(transform.tag, (int)OwnerClientId);

        if (_sameTeam)
        {
            if (!IsServer)
                SetTargetServerRpc(pos);
            else
                SetTargetClient(pos);
        }
    }





    
    [ServerRpc(RequireOwnership = false)]
    private void SetTargetServerRpc(Vector3 pos)
    {
        SetTargetClient(pos);
    }

    private void SetTargetClient(Vector3 pos)
    {
        _agent.SetDestination(pos);
    }








    [ServerRpc(RequireOwnership = false)]
    private void WarpAgentServerRpc()
    {
        WarpAgentClientRpc(transform.position);
    }

    [ClientRpc]
    private void WarpAgentClientRpc(Vector3 pos)
    {
        _agent.Warp(pos);
    }
}
