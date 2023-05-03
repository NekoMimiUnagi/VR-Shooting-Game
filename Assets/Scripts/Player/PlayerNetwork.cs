using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<Color> color = new NetworkVariable<Color>(Color.white);
    
    private Dictionary<ulong, ReadyStatus> ready = new Dictionary<ulong, ReadyStatus>(); // for server

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

    [ServerRpc]
    public void SetReadyServerRpc(ReadyStatus rs, ServerRpcParams rpcParams = default)
    {
        SetReady(rpcParams.Receive.SenderClientId, rs);
        /*
        Debug.Log(OwnerClientId + "?" + rpcParams.Receive.SenderClientId + ": " + rs.sceneID + " --- " + rs.flag);
        ready[rpcParams.Receive.SenderClientId] = rs;
        foreach (KeyValuePair<ulong, ReadyStatus> entry in ready)
        {
            Debug.Log(OwnerClientId + "?" + entry.Key + ": " + entry.Value.sceneID + " -- " + entry.Value.flag);
        }
        */
    }

    private void SetReady(ulong clientID, ReadyStatus rs)
    {
        Debug.Log(OwnerClientId + " -- " + clientID + " -- " + rs.sceneID + " -- " + rs.flag);
        ready[clientID] = rs;
        foreach (KeyValuePair<ulong, ReadyStatus> entry in ready)
        {
            Debug.Log(OwnerClientId + "?" + entry.Key + ": " + entry.Value.sceneID + " -- " + entry.Value.flag);
        }
    }

    public bool AllReady()
    {
        /*
        foreach (KeyValuePair<ulong, ReadyStatus> entry in ready)
        {
            Debug.Log(entry.Key + ": " + entry.Value.sceneID + " -- " + entry.Value.flag);
        }
        */

        int flag = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            ulong clientID = go.GetComponent<PlayerNetwork>().OwnerClientId;
            if (!ready.ContainsKey(clientID))
            {
                return false;
            }
            else
            {
                if (ready[clientID].flag)
                {
                    flag += (1 << ready[clientID].sceneID);
                }
            }
        }
        return (flag == 1) || (flag == 2) || (flag == 4); // 001 || 010 || 100
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
