using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleActivator : MonoBehaviour
{
    public static ReticleActivator instance { get; private set; }

    private GameObject mainCamera;

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.transform.Find("VRGroup/Reticle").gameObject.SetActive(true);
    }

    public void Hide()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.transform.Find("VRGroup/Reticle").gameObject.SetActive(false);
    }
}
