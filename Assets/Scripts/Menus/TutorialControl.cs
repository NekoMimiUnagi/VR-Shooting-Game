using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] subTutorial;
    private int index;
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
        gameObject.SetActive(false);
        
        // for (int i = 1; i < subTutorial.Length; i++)
        // {
        //     subTutorial[i].gameObject.SetActive(false);
        // }
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log(index);
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
                
                gameObject.SetActive(false);
                index = 0;

            }
        }
    }
}
