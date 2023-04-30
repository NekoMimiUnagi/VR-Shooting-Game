using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class ColorPanel : MonoBehaviour
{
    public static ColorPanel instance { get; private set; }

    private SpeedController speedCtrl;
    private ReticleActivator reticleActivator;
    private string originMenuName = "";

    private GameObject player = null;
    private RawImage colorDisplayer;
    private GameObject notice;

    private float waitTime = 0;
    private bool waitFlag = false;

    private List<Color> colors = new List<Color> { Color.blue, Color.black, Color.cyan, Color.green,
                                                   Color.magenta, Color.red, Color.white, Color.yellow };
    private int selectedColorIndex = 0; // make it run by ServerRpc
    //private Dictionary<Color, string> colorUsage = new Dictionary<Color, string>();

    // Start is called before the first frame update
    void Start()
    {
        // use two controller to control speed anc reticle
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        reticleActivator = GameObject.Find("ReticleActivator").GetComponent<ReticleActivator>();

        colorDisplayer = transform.Find("Color").GetComponent<RawImage>();
        colorDisplayer.color = colors[selectedColorIndex];
        notice = gameObject.transform.Find("Notice").gameObject;

        RawImage background = GetComponent<RawImage>();
        background.color = new Color(0.5f, 0.5f, 0.5f, 0.8f);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!IsOwner) return;

        // find the right player and store it
        if (null == player)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (go.GetComponent<PlayerNetwork>().IsOwner)
                {
                    player = go;
                    break;
                }
            }
        }

        float trend = Input.GetAxisRaw("Horizontal");
        if (-0.2f <= trend && trend <= 0.2f)
        {
            waitTime = 0;
            waitFlag = false;
        }
        else
        {
            if (!waitFlag)
            {
                if (trend > 0.2)
                {
                    waitFlag = true;
                    selectedColorIndex = (selectedColorIndex + 1) % colors.Count;
                    player.GetComponent<PlayerNetwork>().SetColorServerRpc(colors[selectedColorIndex]);
                    colorDisplayer.color = colors[selectedColorIndex];
                }
                else if (trend < -0.2)
                {
                    waitFlag = true;
                    selectedColorIndex = (selectedColorIndex - 1 + colors.Count) % colors.Count;
                    player.GetComponent<PlayerNetwork>().SetColorServerRpc(colors[selectedColorIndex]);
                    colorDisplayer.color = colors[selectedColorIndex];
                }
            }
            else
            {
                // add delay after each choice for continuous moving up or down
                waitTime += Time.deltaTime;
                if (waitTime > 0.5f)
                {
                    waitTime = 0;
                    waitFlag = false;
                }
            }
        }
    }

    // Prepared for settings at main menu before seleting single mode or multiplayer mode
    public Color GetSettingsColor()
    {
        return colors[selectedColorIndex];
    }

    public void Show(string name = "")
    {
        originMenuName = name;
        reticleActivator.Hide();
        speedCtrl.Lock();
        gameObject.SetActive(true);
    }

    public string Hide()
    {
        reticleActivator.Show();
        speedCtrl.Unlock();
        gameObject.SetActive(false);

        return originMenuName;
    }
}
