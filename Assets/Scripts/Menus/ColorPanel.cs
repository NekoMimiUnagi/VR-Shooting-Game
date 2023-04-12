using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPanel : MonoBehaviour
{
    public static ColorPanel instance { get; private set; }

    private SpeedController speedCtrl;
    private ReticleActivator reticleActivator;
    private string originMenuName = "";

    private GameObject player;
    private RawImage colorDisplayer;
    private GameObject notice;

    private float waitTime = 0;
    private bool waitFlag = false;

    private List<Color> colors = new List<Color> { Color.blue, Color.black, Color.cyan, Color.green,
                                                   Color.magenta, Color.red, Color.white, Color.yellow };
    private int selectedColorIndex = 0;
    private Dictionary<Color, string> colorUsage = new Dictionary<Color, string>();

    // Start is called before the first frame update
    void Start()
    {
        // use two controller to control speed anc reticle
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        reticleActivator = GameObject.Find("ReticleActivator").GetComponent<ReticleActivator>();

        player = GameObject.Find("Player");
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
                    selectedColorIndex = (selectedColorIndex + 1) % colors.Count;
                    waitFlag = true;
                    colorDisplayer.color = colors[selectedColorIndex];
                }
                else if (trend < -0.2)
                {
                    selectedColorIndex = (selectedColorIndex - 1 + colors.Count) % colors.Count;
                    waitFlag = true;
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
