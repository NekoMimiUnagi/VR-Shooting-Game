using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] subTutorial;
    private int index;
    private GameObject mainMenu;

    private SpeedController speedCtrl;
    private ReticleActivator reticleActivator;
    private string originMenuName = "";

    private void OnEnable()
    {
        // Ensure that the first sub-canvas is active when the canvas is enabled
        index = 0;
        for (int i = 0; i < subTutorial.Length; i++)
        {
            subTutorial[i].gameObject.SetActive(i == index);
        }
    }

    private void OnDisable()
    {
        // Ensure that all sub-canvases are inactive when the canvas is disabled
        for (int i = 0; i < subTutorial.Length; i++)
        {
            subTutorial[i].gameObject.SetActive(false);
        }
    }

    void Start()
    {
        // use two controller to control speed anc reticle
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        reticleActivator = GameObject.Find("ReticleActivator").GetComponent<ReticleActivator>();

        mainMenu = GameObject.Find("MainMenu");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Canvas>().worldCamera is null)
        {
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        if(Input.GetButtonDown("js5"))
        {
            if (index < subTutorial.Length - 1)
            {
                subTutorial[index].gameObject.SetActive(false);
                index++;
                subTutorial[index].gameObject.SetActive(true);
            }
            else
            {
                index = 0;
                Hide();
                mainMenu.GetComponent<MenuController>().Show();
            }
        }
    }

    public void Show(string name = "")
    {
        originMenuName = name;
        reticleActivator.Hide();
        speedCtrl.Lock();
        gameObject.SetActive(true);
    }

    public string Hide()
    {
        reticleActivator.Show();
        speedCtrl.Unlock();
        gameObject.SetActive(false);

        return originMenuName;
    }
}
