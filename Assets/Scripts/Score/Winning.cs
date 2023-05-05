using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winning;
    private float displayTime = 2.0f;
    private float passedTime = 0f;

    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Load camera for non-lobby scenes
        if (GetComponent<Canvas>().worldCamera is null)
        {
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        passedTime += Time.deltaTime;
        if (passedTime >= displayTime)
        {
            passedTime = 0f;
            gameObject.SetActive(false);
        }
    }
}
