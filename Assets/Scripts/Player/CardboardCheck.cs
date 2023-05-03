using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardCheck : MonoBehaviour
{
    private GameObject player = null;

    // Update is called once per frame
    void Update()
    {
        if (null != transform.parent && "Player" == transform.parent.tag)
        {
            return ;
        }
        // find the right player and store it
        if (null == player)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (go.GetComponent<PlayerNetwork>().IsOwner)
                {
                    player = go;
                    if (gameObject.name == player.transform.GetChild(0).name)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
