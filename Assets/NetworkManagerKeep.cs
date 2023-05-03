using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerKeep : MonoBehaviour
{
    public static NetworkManagerKeep instance { get; private set; }

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
}
