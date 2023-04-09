using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            /*
            if (null == inventoryPanel)
            {
                inventoryPanel = GameObject.Find("InventoryPanel");
            }
            */
            GameObject inventoryPanel = inventory.GetPanel();
            inventoryPanel.GetComponent<InventoryPanel>().Show();
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
