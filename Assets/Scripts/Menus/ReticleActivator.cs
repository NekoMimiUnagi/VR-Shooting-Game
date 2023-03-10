using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleActivator : MonoBehaviour
{
    public GameObject reticle;

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
        reticle.SetActive(true);
    }

    public void Hide()
    {
        reticle.SetActive(false);
    }
}
