using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance { get; private set; }

    private SpeedController speedCtrl;
    private ReticleActivator reticleActivator;
    private string getOriginMenuName = "";

    private List<Image> buttons = new List<Image>();
    private int selectedButtonIndex = 0;
    private bool waitFlag = false;
    private float waitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        // use two controller to control speed anc reticle
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        reticleActivator = GameObject.Find("ReticleActivator").GetComponent<ReticleActivator>();

        PlayerData playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        GameObject player = GameObject.FindWithTag("Player");
        // Show main menu when game starts
        if ("MainMenu" == gameObject.name && !playerData.Exists(player.name))
        {
            getOriginMenuName = gameObject.name;
            reticleActivator.Hide();
            speedCtrl.Lock();
            playerData.StartGame(player.name);
        }
        else
        {
            gameObject.SetActive(false);
        }

        // load all buttons in the panel
        foreach (Transform child in gameObject.transform)
        {
            if (null == child)
                continue;
            buttons.Add(child.gameObject.GetComponent<Image>());
        }
        buttons[0].color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        // select button by joy-stick up and down
        float trend = Input.GetAxisRaw("Vertical");
        if (0f == trend)
        {
            waitTime = 0;
            waitFlag = false;
        }
        if (!waitFlag)
        {
            if (trend > 0.2)
            {
                buttons[selectedButtonIndex].color = Color.white;
                selectedButtonIndex = Mathf.Max(selectedButtonIndex - 1, 0);
                waitFlag = true;
                buttons[selectedButtonIndex].color = Color.yellow;
            }
            else if (trend < -0.2)
            {
                buttons[selectedButtonIndex].color = Color.white;
                selectedButtonIndex = Mathf.Min(selectedButtonIndex + 1, buttons.Count - 1);
                waitFlag = true;
                buttons[selectedButtonIndex].color = Color.yellow;
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

        // ignore submit button
        if (Input.GetButton("Submit"))
        {
            ;
        }

        // press B to activate buttons
        if (Input.GetButtonDown("js5"))
        {
            buttons[selectedButtonIndex].GetComponent<Button>().onClick.Invoke();
        }
    }

    public void Show(string name = "")
    {
        getOriginMenuName = name;
        reticleActivator.Hide();
        speedCtrl.Lock();
        gameObject.SetActive(true);
    }

    public string Hide()
    {
        reticleActivator.Show();
        speedCtrl.Unlock();
        gameObject.SetActive(false);

        return getOriginMenuName;
    }
}
