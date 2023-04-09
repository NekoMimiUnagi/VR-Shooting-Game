using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Transfer data among scenes when teleporting
public class PlayerData : MonoBehaviour
{
    public static PlayerData instance { get; private set; }

    // Key is the name of player object. Thus, this can store data of all players.
    private Dictionary<string, Player> players = new Dictionary<string, Player>();

    //private Dictionary<string, Vector3> relativePosition = new Dictionary<string, Vector3>();
    //private Dictionary<string, Quaternion> faceTo = new Dictionary<string, Quaternion>();
    //private Dictionary<string, string> fromScene = new Dictionary<string, string>();
    //private HashSet<string> gameStarted = new HashSet<string>();

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

    public void UpdateNickName(string name, string nickName)
    {
        if (!players.ContainsKey(name))
        {
            players[name] = new Player(nickName);
        }
        else
        {
            players[name].UpdateNickName(nickName);
        }
    }

    public void UpdateColor(string name, Color color)
    {
        if (!players.ContainsKey(name))
        {
            players[name] = new Player(name, color);
        }
        else
        {
            players[name].UpdateColor(color);
        }
    }

    public void UpdateRelativeTransform(string name, Vector3 vec, Quaternion rot)
    {
        if (!players.ContainsKey(name))
        {
            players[name] = new Player(name);
        }

        players[name].UpdateRelativeTransform(vec, rot);
    }

    public void UpdateFromSceneName(string name, string sceneName)
    {
        if (!players.ContainsKey(name))
        {
            players[name] = new Player(name);
        }

        players[name].UpdateFromSceneName(sceneName);
    }

    public void StartGame(string name)
    {
        players[name] = new Player(name);
    }

    public bool Exists(string name)
    {
        return players.ContainsKey(name);
    }

    public string GetNickName(string name)
    {
        if (players.ContainsKey(name))
        {
            return players[name].GetNickName();
        }
        return "";
    }

    public Color GetColor(string name)
    {
        if (players.ContainsKey(name))
        {
            return players[name].GetColor();
        }
        return new Color();
    }

    public (Vector3, Quaternion) GetRelativeTransform(string name)
    {
        if (players.ContainsKey(name))
        {
            return (players[name].GetRelativeTransform());
        }
        return (new Vector3(), new Quaternion());
    }

    public string GetFromSceneName(string name)
    {
        if (players.ContainsKey(name))
        {
            return players[name].GetFromSceneName();
        }
        return "";
    }
}
