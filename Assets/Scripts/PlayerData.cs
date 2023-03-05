using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Transfer data among scenes when teleporting
public class PlayerData : MonoBehaviour
{
    public static PlayerData instance { get; private set; }

    // Key is the name of player object. Thus, this can store data of all players.
    private Dictionary<string, Vector3> relativePosition = new Dictionary<string, Vector3>();
    private Dictionary<string, Quaternion> faceTo = new Dictionary<string, Quaternion>();
    private Dictionary<string, string> fromScene = new Dictionary<string, string>();

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update relative position and face direction
    public void Update(string name, Vector3 vec, Quaternion rot)
    {
        // update relative position
        relativePosition[name] = vec;

        // update face direction
        faceTo[name] = rot;
    }

    // update which scene a player leaves
    public void Update(string name, string sceneName)
    {
        fromScene[name] = sceneName;
    }

    public bool Exists(string name)
    {
        return relativePosition.ContainsKey(name) && fromScene.ContainsKey(name);
    }

    public (Vector3, Quaternion) Get(string name)
    {
        return (relativePosition[name], faceTo[name]);
    }

    public string GetFromSceneName(string name)
    {
        if (fromScene.ContainsKey(name))
        {
            return fromScene[name];
        }
        return "";
    }
}
