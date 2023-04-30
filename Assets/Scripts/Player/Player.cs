using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private Color color;
    private Vector3 relativePosition;
    private Quaternion faceTo;
    private string fromSceneName = "";

    public Player()
    {
        ;
    }

    public Player(Color newColor)
    {
        color = newColor;
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
}
