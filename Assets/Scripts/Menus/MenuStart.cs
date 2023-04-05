using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour
{
    public void StartGame()
    {
        gameObject.transform.parent.gameObject.GetComponent<MenuController>().Hide();
    }
}
