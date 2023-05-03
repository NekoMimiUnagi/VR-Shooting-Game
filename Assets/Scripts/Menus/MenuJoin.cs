using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MenuJoin : MonoBehaviour
{
    public void JoinGame()
    {
        NetworkManager.Singleton.StartClient();
        gameObject.transform.parent.GetComponent<MenuController>().Hide();
    }
}
