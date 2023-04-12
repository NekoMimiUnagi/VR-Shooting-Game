using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController instance { get; private set; }

    private PlayerData playerData = null;
    private CharacterMovement charaMove;

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
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeed(string playerName, int new_speed)
    {
        playerData.UpdateSpeed(playerName, new_speed);
        if (!playerData.GetSpeedLock(playerName))
        {
            charaMove = GameObject.Find(playerName).GetComponent<CharacterMovement>();
            charaMove.speed = new_speed;
        }
    }

    public void Lock(string playerName="Player")
    {
        charaMove = GameObject.Find(playerName).GetComponent<CharacterMovement>();
        // If lock for multiple times, only the first time affect the speed
        //Debug.Log(playerData);
        if (!playerData.GetSpeedLock(playerName))
        {
            playerData.UpdateSpeedLock(playerName, true);
            playerData.UpdateSpeed(playerName, charaMove.speed);
            charaMove.speed = 0;
        }
    }

    public void Unlock(string playerName = "Player")
    {
        charaMove = GameObject.Find(playerName).GetComponent<CharacterMovement>();
        // If unlock for multiple times, only the first time affect the speed
        if (playerData.GetSpeedLock(playerName))
        {
            playerData.UpdateSpeedLock(playerName, false);
            charaMove.speed = playerData.GetSpeed(playerName);
        }
    }
}
