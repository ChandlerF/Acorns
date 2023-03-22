using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class CivilianAI : NetworkBehaviour
{
    private NavMeshAgent _agent;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Vector3 spawnPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);


            if (!IsServer)
                SetTargetServerRpc(spawnPos);
            else
                SetTargetClient(spawnPos);
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
}
