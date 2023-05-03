using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSpeed : MonoBehaviour
{
    private SpeedController speedCtrl;
    private List<string> modes = new List<string>{ "Speed: Low",
                                                   "Speed: Medium",
                                                   "Speed: High" };
    private List<int> speeds = new List<int> { 5, 10, 20};
    private int toggle = 0;

    // Start is called before the first frame update
    void Start()
    {
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        gameObject.GetComponentInChildren<TMP_Text>().text = modes[toggle];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speed()
    {
        toggle = (toggle + 1) % speeds.Count;
        gameObject.GetComponentInChildren<TMP_Text>().text = modes[toggle];
        speedCtrl.SetSpeed(speeds[toggle]);
    }

    // Prepared for settings at main menu before seleting single mode or multiplayer mode
    public int GetSettingsSpeed()
    {
        return speeds[toggle];
    }
}
