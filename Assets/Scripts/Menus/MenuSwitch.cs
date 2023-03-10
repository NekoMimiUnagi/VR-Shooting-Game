using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{
    public GameObject targetMenu;

    public void SwitchMenu()
    {
        // TODO: remove all other players
        GameObject parentMenu = gameObject.transform.parent.gameObject;
        string originMenuName = parentMenu.GetComponent<MenuController>().Hide();
        if ("ColorPanel" == targetMenu.name)
        {
            targetMenu.GetComponent<ColorPanel>().Show(originMenuName);
        }
        else
        {
            targetMenu.GetComponent<MenuController>().Show(originMenuName);
        }
    }
}
