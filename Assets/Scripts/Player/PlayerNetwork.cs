using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<Color> color = new NetworkVariable<Color>(Color.white);
    private NetworkVariable<bool> ready = new NetworkVariable<bool>(false);

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
            gameObject.GetComponent<Renderer>().material.color = color.Value;
        };
    }

    [ServerRpc]
    public void SetColorServerRpc(Color new_color)
    {
        color.Value = new_color;
    }

    [ServerRpc]
    public void SetReadyServerRpc(bool new_status)
    {
        ready.Value = new_status;
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
