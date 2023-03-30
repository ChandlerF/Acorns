using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SelectionUI : NetworkBehaviour
{
    private bool _hasEnabledChild = false;
    /*public override void OnNetworkSpawn()
    {
        transform.GetChild(0).gameObject.SetActive(true); 
    }*/


    private void Update()
    {
        if (GameSettings.Instance.ClientHasJoined && !_hasEnabledChild)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            _hasEnabledChild = true;
        }
    }

    //Macro 1  ||  Micro 2
    public void SelectionButton(int x)
    {
       if(x == 1)
        {
            GameSettings.Instance.MacroClientID.Value = (int)OwnerClientId;
        }
        else
        {
            GameSettings.Instance.MicroClientID.Value = (int)OwnerClientId;
        }

       Destroy(gameObject);
    }
}
