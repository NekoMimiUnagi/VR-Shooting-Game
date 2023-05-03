using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Color color;
    private Vector3 relativePosition;
    private Quaternion faceTo;
    private string fromSceneName = "";
    private float speed = 0;
    private bool speedLock = false;
    private int ID;
    private int numPlayer = 0;
    private bool gameStarted = false;

    public Player()
    {
        nickName = newNickName;
    }

    public Player(Color newColor)
    {
        color = newColor;
        ID = numPlayer;
        numPlayer++;
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

    public void StartGame()
    {
        gameStarted = true;
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

    public bool GameStarted()
    {
        return gameStarted;
    }
}
