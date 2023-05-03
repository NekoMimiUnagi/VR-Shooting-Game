using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<Color> color = new NetworkVariable<Color>(Color.white);

    //private Dictionary<ulong, ReadyStatus> ready = new Dictionary<ulong, ReadyStatus>(); // for server
    [SerializeField] private string ready;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            // find google xr prefab for VR settings
            GameObject xr = GameObject.Find("XRCardboardRig");
            xr.transform.SetParent(transform);
            xr.transform.localPosition = Vector3.zero;

            // find camera for movement
            GameObject cam = GameObject.FindWithTag("MainCamera");
            GetComponent<CharacterMovement>().cameraObj = cam;

            // set initial position in the lobby
            transform.position = new Vector3(Random.Range(0, 2), 1, Random.Range(-2, 2));

            // Start game if the player spawns
            GetComponent<Player>().StartGame();

            ready = "";
        }

        color.OnValueChanged += (Color previousValue, Color newValue) =>
        {
            GetComponent<Renderer>().material.color = color.Value;
        };
    }

    [ServerRpc]
    public void SetColorServerRpc(Color new_color)
    {
        color.Value = new_color;
    }

    public void SetReady(int status)
    {
        SetReadyServerRpc(status);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetReadyServerRpc(int status, ServerRpcParams rpcParams = default)
    {
        UpdateClientReady(rpcParams.Receive.SenderClientId, status);
    }

    private void UpdateClientReady(ulong clientID, int status)
    {
        int total_clients = NetworkManager.ConnectedClientsIds.Count;
        while (ready.Length < total_clients)
        {
            ready += "0";
        }
        int id = (int)clientID;
        ready = ready.Remove(id, 1).Insert(id, status.ToString());
        if (IsServer)
        {
            BroadcastReadyClientRpc(ready);
        }
    }

    public void ClearAllReady()
    {
        ClearAllReadyServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void ClearAllReadyServerRpc()
    {
        ready = "";
        if (IsServer)
        {
            BroadcastReadyClientRpc("");
        }
    }

    [ClientRpc]
    private void BroadcastReadyClientRpc(string all_status)
    {
        ReceiveReady(all_status);
    }

    private void ReceiveReady(string all_status)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            go.GetComponent<PlayerNetwork>().UpdateReady(all_status);
        }
    }

    public void UpdateReady(string status)
    {
        ready = status;
    }

    public bool AllReady()
    {
        int flag = -1;
        Debug.Log("All Ready? " + ready);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            int clientID = (int)go.GetComponent<PlayerNetwork>().OwnerClientId;
            int number = ready[clientID] - '0';
            if (-1 == flag)
            {
                flag = number;
                if (0 == flag)
                {
                    return false;
                }
            }
            else if (flag != number)
            {
                return false;
            }
        }
        return true;
    }

    [ClientRpc]
    public void ResetSceneStatusClientRpc()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            go.GetComponent<Teleportation>().ResetSceneStatus();
        }
    }

    /*
    void Start()
    {
        PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        GameObject player = GameObject.FindWithTag("Player");
        if ("MainMenu" == gameObject.name && !playerData.Exists(player.name))
        {
            originMenuName = gameObject.name;
            reticleActivator.Hide();
            playerData.StartGame(player.name);
            speedCtrl.Lock(player.name);
        }
    }
    */
}
