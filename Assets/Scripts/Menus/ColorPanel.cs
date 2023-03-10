using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPanel : MonoBehaviour
{
    public static ColorPanel instance { get; private set; }

    private SpeedController speedCtrl;
    private ReticleActivator reticleActivator;
    private string fromGameObjectName = "";
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

        player = GameObject.FindWithTag("Player");
        colorDisplayer = gameObject.GetComponentInChildren<RawImage>();
        colorDisplayer.color = colors[selectedColorIndex];
        notice = gameObject.transform.Find("Notice").gameObject;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float trend = Input.GetAxisRaw("Horizontal");
        if (0f == trend)
        {
            waitTime = 0;
            waitFlag = false;
        }
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

    public void Show(string name = "")
    {
        fromGameObjectName = name;
        reticleActivator.Hide();
        speedCtrl.Lock();
        gameObject.SetActive(true);
    }

    public string Hide()
    {
        reticleActivator.Show();
        speedCtrl.Unlock();
        gameObject.SetActive(false);

        // delete and return
        string name = fromGameObjectName;
        fromGameObjectName = "";
        return name;
    }
}
