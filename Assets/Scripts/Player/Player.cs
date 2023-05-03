using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string nickName;
    private Color color;
    private Vector3 relativePosition;
    private Quaternion faceTo;
    private string fromSceneName = "";
    private float speed = 0;
    private bool speedLock = false;
    private int ID;
    private int numPlayer = 0;

    public Player(string newNickName)
    {
        nickName = newNickName;
     
    }
    public Player(Color newColor)
    {
        color = newColor;
        ID = numPlayer;
        numPlayer++;
   
    }

    public Player(string newNickName, Color newColor)
    {
        nickName = newNickName;
        color = newColor;
    }

    
    public void UpdateNickName(string newNickName)
    {
        nickName = newNickName;
    }

    public void UpdateColor(Color newColor)
    {
        color = newColor;
    }

    public void UpdateRelativeTransform(Vector3 newRelativePosition,
                                        Quaternion newFaceDirection)
    {
        relativePosition = newRelativePosition;
        faceTo = newFaceDirection;
    }

    public void UpdateFromSceneName(string newFromSceneName)
    {
        fromSceneName = newFromSceneName;
    }

    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void UpdateSpeedLock(bool lockStatus)
    {
        speedLock = lockStatus;
    }

    public string GetNickName()
    {
        return nickName;
    }

    public int GetPlayerID()
    {
        return ID;
    }
    public Color GetColor()
    {
        return color;
    }

    public (Vector3, Quaternion) GetRelativeTransform()
    {
        return (relativePosition, faceTo);
    }

    public string GetFromSceneName()
    {
        return fromSceneName;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public bool GetSpeedLock()
    {
        return speedLock;
    }
}
