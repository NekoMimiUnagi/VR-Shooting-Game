using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int id;
    private Color color;
    private Vector3 relativePosition;
    private Quaternion faceTo;
    private string fromSceneName = "";
    private int ID;
    private int numPlayer = 0;
    private bool gameStarted = false;

    public Player()
    {
        ;
    }

    public Player(int newID)
    {
        id = newID;
    }

    public Player(Color newColor)
    {
        color = newColor;
        ID = numPlayer;
        numPlayer++;
    }

    public Player(int newID, Color newColor)
    {
        id = newID;
        color = newColor;
    }

    public void UpdateID(int newID)
    {
        id = newID;
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

    public int GetID()
    {
        return id;
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
