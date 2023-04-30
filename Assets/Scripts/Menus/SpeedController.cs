using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController instance { get; private set; }

    private CharacterMovement charaMove = null;
    private float speed = 0;
    private bool speedLock = false;

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

    // Update is called once per frame
    void Update()
    {
        if (null == charaMove)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (go.GetComponent<PlayerNetwork>().IsOwner)
                {
                    charaMove = go.GetComponent<CharacterMovement>();
                    break;
                }
            }
        }
    }

    public void SetSpeed(float new_speed)
    {
        speed = new_speed;
        if (!speedLock && null != charaMove)
        {
            charaMove.speed = new_speed;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void Lock()
    {
        // If lock for multiple times, only the first time affect the speed
        if (!speedLock)
        {
            speedLock = true;
            if (null != charaMove)
            {
                speed = charaMove.speed;
                charaMove.speed = 0;
            }
        }
    }

    public void Unlock()
    {
        // If unlock for multiple times, only the first time affect the speed
        if (speedLock)
        {
            speedLock = false;
            if (null != charaMove)
            {
                charaMove.speed = speed;
            }
        }
    }
}
