using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button _serverButton;
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;
    [SerializeField] private GameObject _selection;

    private void Awake()
    {
        _serverButton.onClick.AddListener(() => 
        {
            NetworkManager.Singleton.StartServer();
            RemoveButtons();

        });

        _hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            SpawnSelection();
            RemoveButtons();
        });

        _clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            SpawnSelection();
            RemoveButtons();
        });
    }


    private void SpawnSelection()
    {
        GameSettings.Instance.ClientHasJoined = true;
    }

    private void RemoveButtons()
    {
        Destroy(_serverButton);
        Destroy(_hostButton);
        Destroy(_clientButton);
        Destroy(gameObject);
    }
}
