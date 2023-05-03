using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Table : MonoBehaviour
{
    private Inventory inventory;
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("js5") && flag)
        {
            GameObject inventoryPanel = inventory.GetPanel();
            if("MainLobby" != SceneManager.GetActiveScene().name)
            {
                inventoryPanel.GetComponent<InventoryPanel>().Show();
            }
            else
            {
                inventoryPanel.GetComponent<InventoryPanelMainLobby>().Show();
            }
        }
    }

    public void PointerEnter()
    {
        flag = true;
    }

    public void PointerExit()
    {
        flag = false;
    }
}
