using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameSettings : NetworkBehaviour
{
    public static GameSettings Instance;


    public NetworkVariable<int> MacroClientID = new NetworkVariable<int>();

    public NetworkVariable<int> MicroClientID = new NetworkVariable<int>();


    private int ClientIDToTeam(string objectsTeam)
    {
        return objectsTeam == "Macro" ? MacroClientID.Value : MicroClientID.Value;
    }





    public bool IsOnSameTeam(int clientID, string objectsTeam)
    {
        return clientID == ClientIDToTeam(objectsTeam);
    }












    public override void OnNetworkSpawn()
    {if (GameSettings.Instance == null)
            Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
