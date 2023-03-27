using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RemoveComponentClient : NetworkBehaviour
{
    [SerializeField] private List<Component> _discard = new List<Component>();


    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            foreach (var component in _discard)
            {
                Destroy(component);
            }
        }
    }
}
