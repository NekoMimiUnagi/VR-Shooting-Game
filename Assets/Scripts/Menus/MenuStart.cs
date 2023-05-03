using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MenuStart : MonoBehaviour
{
    public void StartGame()
    {
        NetworkManager.Singleton.StartHost();
        gameObject.transform.parent.GetComponent<MenuController>().Hide();
        Player playerData = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerData.StartGame();
    }
}
