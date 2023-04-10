using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeNotice : MonoBehaviour
{
    public static ShootingRangeNotice instance { get; private set; }

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
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Ready").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Load camera when switching scenes
        if (GetComponent<Canvas>().worldCamera is null)
        {
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
    }
}
