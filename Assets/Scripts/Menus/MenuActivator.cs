using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuActivator : MonoBehaviour
{
    /*
    public GameObject mainMenu;
    public GameObject lobbyMenu;
    public GameObject settingsPanel;
    public GameObject colorPanel;
    public GameObject volumePanel;
    public GameObject inventoryPanel;
    */
    private GameObject mainMenu;
    private GameObject lobbyMenu;
    private GameObject settingsPanel;
    private GameObject colorPanel;
    private GameObject volumePanel;
    private GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects;
        gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in gameObjects)
        {
            if ("Menu" != go.tag)
            {
                continue;
            }
            switch (go.name)
            {
                case "MainMenu":
                    mainMenu = go;
                    break;
                case "LobbyMenu":
                    lobbyMenu = go;
                    break;
                case "SettingsPanel":
                    settingsPanel = go;
                    break;
                case "ColorPanel":
                    colorPanel = go;
                    break;
                case "VolumePanel":
                    volumePanel = go;
                    break;
                case "InventoryPanel":
                    inventoryPanel = go;
                    break;
                default:
                    Debug.Log(go.name + go.tag);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // press Menu to activate/deactivate settings menu
        // handle what happends after close all menus and when to open lobby menu
        if (Input.GetButtonDown("js2") && !mainMenu.activeSelf && !inventoryPanel.activeSelf)
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
