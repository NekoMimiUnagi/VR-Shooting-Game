using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject lobbyMenu;
    public GameObject settingsPanel;
    public GameObject colorPanel;
    public GameObject volumePanel;
    public GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // press Menu to activate/deactivate settings menu
        // handle what happends after close all menus and when to open lobby menu
        if (Input.GetButtonDown("js1") && !mainMenu.activeSelf && !inventoryPanel.activeSelf)
        {
            if (lobbyMenu.activeSelf)
            {
                lobbyMenu.GetComponent<MenuController>().Hide();
            }
            else if (settingsPanel.activeSelf)
            {
                string originMenuName = settingsPanel.GetComponent<MenuController>().Hide();
                if ("MainMenu" == originMenuName)
                {
                    mainMenu.GetComponent<MenuController>().Show(mainMenu.name);
                }
            }
            else if (colorPanel.activeSelf)
            {
                string originMenuName = colorPanel.GetComponent<ColorPanel>().Hide();
                settingsPanel.GetComponent<MenuController>().Show(originMenuName);
            }
            else if (volumePanel.activeSelf)
            {
                string originMenuName = volumePanel.GetComponent<VolumePanel>().Hide();
                settingsPanel.GetComponent<MenuController>().Show(originMenuName);
            }
            else
            {
                lobbyMenu.GetComponent<MenuController>().Show(lobbyMenu.name);
            }
        }
    }
}
