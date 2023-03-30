using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameSettings : NetworkBehaviour
{
    public static GameSettings Instance;


    //Macro = 1
    public NetworkVariable<int> MacroClientID = new NetworkVariable<int>();

    //Micro = 2
    public NetworkVariable<int> MicroClientID = new NetworkVariable<int>();


    private int ClientIDToTeam(string clientID)
    {
        return clientID =="Macro" ? 1 : 2;
    }





    //Macro = 1  || Micro = 2           There is no Micro without Macro
    public bool IsOnSameTeam(string clientID, int objectsTeam)
    {
        return objectsTeam == ClientIDToTeam(clientID);
    }



















    public override void OnNetworkSpawn()
    {if (GameSettings.Instance == null)
            Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
