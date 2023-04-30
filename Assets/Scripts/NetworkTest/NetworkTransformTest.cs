using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransformTest : NetworkBehaviour
{
    void Update()
    {
        ///*
        if (IsServer)
        {
            float trendH = Input.GetAxisRaw("Horizontal");
            float trendV = Input.GetAxisRaw("Vertical");

            if (trendV > 0)
            {
                transform.position += new Vector3(0, 0, 0.001f);
            }
            else if (trendV < 0)
            {
                transform.position -= new Vector3(0, 0, 0.001f);
            }

            if (trendH > 0)
            {
                transform.position += new Vector3(0.01f, 0, 0);
            }
            else if (trendH < 0)
            {
                transform.position -= new Vector3(0.01f, 0, 0);
            }
        }
        //*/
        /*
        if (IsServer)
        {
            float theta = Time.frameCount / 10.0f;
            transform.position = new Vector3((float)Math.Cos(theta), 0.0f, (float)Math.Sin(theta));
        }
        */
    }
}