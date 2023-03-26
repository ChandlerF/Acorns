using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField] private NetworkVariable<int> _gold = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
   
    public int Gold
    {
        get
        {
            return _gold.Value;
        }
        set
        {
            UIManager.instance.UpdateText(value);
            _gold.Value = value;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) this.enabled = false;
        UIManager.instance.UpdateText(_gold.Value);
    }
}
