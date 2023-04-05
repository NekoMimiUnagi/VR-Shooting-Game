using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumePanel : MonoBehaviour
{
    public static VolumePanel instance { get; private set; }

    private SpeedController speedCtrl;
    private ReticleActivator reticleActivator;
    private string originMenuName = "";

    private float waitTime = 0;
    private bool waitFlag = false;

    private Slider slider;
    private AudioSource audioSource;
    private float volume = 0;

    // Start is called before the first frame update
    void Start()
    {
        // use two controller to control speed anc reticle
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        reticleActivator = GameObject.Find("ReticleActivator").GetComponent<ReticleActivator>();

        audioSource = GameObject.Find("Sound").GetComponentInChildren<AudioSource>();
        slider = gameObject.GetComponentInChildren<Slider>();
        volume = slider.value;
        audioSource.volume = volume;

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
        if (!waitFlag)
        {
            if (trend > 0.2)
            {
                volume = Mathf.Min(volume + 0.01f, 1);
                slider.value = volume;
                audioSource.volume = volume;
                waitFlag = true;
            }
            else if (trend < -0.2)
            {
                volume = Mathf.Max(volume - 0.01f, 0);
                slider.value = volume;
                audioSource.volume = volume;
                waitFlag = true;
            }
        }
        else
        {
            // add delay after each choice for continuous moving up or down
            waitTime += Time.deltaTime;
            if (waitTime > 0.01f)
            {
                waitTime = 0;
                waitFlag = false;
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
