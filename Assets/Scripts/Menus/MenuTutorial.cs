using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTutorial : MonoBehaviour
{
    private GameObject tutorial;

    // Start is called before the first frame update
    public void tutorialStart()
    {
        tutorial.SetActive(true);
    }

    void Start()
    {
        tutorial = GameObject.Find("Tutorial");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
