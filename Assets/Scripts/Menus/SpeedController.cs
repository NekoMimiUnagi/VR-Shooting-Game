using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController instance { get; private set; }

    private CharacterMovement charaMove;
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

    // Start is called before the first frame update
    void Start()
    {
        charaMove = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        speed = charaMove.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed(int new_speed)
    {
        if (speedLock)
        {
            speed = new_speed;
        }
        else
        {
            charaMove.speed = new_speed;
        }
    }

    public void Lock()
    {
        charaMove = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        // If lock for multiple times, only the first time affect the speed
        if (!speedLock)
        {
            speedLock = true;
            speed = charaMove.speed;
            charaMove.speed = 0;
        }
    }

    public void Unlock()
    {
        charaMove = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        // If unlock for multiple times, only the first time affect the speed
        if (speedLock)
        {
            speedLock = false;
            charaMove.speed = speed;
        }
    }
}
