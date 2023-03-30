using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SelectionUI : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        transform.GetChild(0).gameObject.SetActive(true); 
    }


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
