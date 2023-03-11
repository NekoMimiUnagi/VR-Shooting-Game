using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{
    public GameObject targetMenu;

    public void SwitchMenu()
    {
        // TODO: remove all other players
        string originMenuName = gameObject.transform.parent.GetComponent<MenuController>().Hide();
        if ("ColorPanel" == targetMenu.name)
        {
            targetMenu.GetComponent<ColorPanel>().Show(originMenuName);
        }
        else if("VolumePanel" == targetMenu.name)
        {
            targetMenu.GetComponent<VolumePanel>().Show(originMenuName);
        }
        else
        {
            targetMenu.GetComponent<MenuController>().Show(originMenuName);
        }
    }
}
