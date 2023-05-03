using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }

    private GameObject inventoryPanel;
    private List<GameObject> inventory = new List<GameObject>();
    private string previousSceneName = "";

    void Awake()
    {
        if (null != instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        // load all buttons in the panel
        foreach (Transform child in gameObject.transform)
        {
            if (null == child)
                continue;
            inventory.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (previousSceneName != SceneManager.GetActiveScene().name)
        {
            GameObject[] gameObjects;
            gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject go in gameObjects)
            {
                if ("InventoryPanel" == go.name)
                {
                    inventoryPanel = go;
                    break;
                }
            }
            previousSceneName = SceneManager.GetActiveScene().name;
        }
    }

    public GameObject Get(int index)
    {
        if (index < inventory.Count)
        {
            return inventory[index];
        }
        return null;
    }

    public int Count()
    {
        return inventory.Count;
    }

    public GameObject GetPanel()
    {
        return inventoryPanel;
    }

    /* for debug only
    public void ShowAll()
    {
        foreach (GameObject go in inventory)
        {
            Debug.Log(go.name);
        }
    }
    */
}
