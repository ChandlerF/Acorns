using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameSettings : NetworkBehaviour
{
    public static GameSettings Instance;


    public NetworkVariable<int> MacroClientID = new NetworkVariable<int>(9, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> MicroClientID = new NetworkVariable<int>(9, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public bool ClientHasJoined = false;


    private int ClientIDToTeam(string objectsTeam)
    {
        return objectsTeam == "Macro" ? MacroClientID.Value : MicroClientID.Value;
    }





    public bool IsOnSameTeam(int clientID, string objectsTeam)
    {
        return clientID == ClientIDToTeam(objectsTeam);
    }











    private void Awake()
    {
        SetInstance();
    }
    public override void OnNetworkSpawn()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (GameSettings.Instance == null)
            Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
