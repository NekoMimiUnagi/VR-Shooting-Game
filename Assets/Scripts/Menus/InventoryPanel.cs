using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel instance { get; private set; }

    private List<RawImage> images;
    private Inventory inventory;
    private int rowLength = 3;
    private int selectedV = 0;
    private int selectedH = 0;
    private bool waitFlag = false;
    private float waitTime = 0;

    private SpeedController speedCtrl;
    private ReticleActivator reticleActivator;

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
        // use two controller to control speed anc reticle
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        reticleActivator = GameObject.Find("ReticleActivator").GetComponent<ReticleActivator>();

        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        RawImage[] images_tmp = gameObject.GetComponentsInChildren<RawImage>();
        int selected = selectedV * rowLength + selectedH;
        images = new List<RawImage>(images_tmp);
        for (int i = 0; i < inventory.Count(); ++i)
        {
            GameObject go = inventory.Get(i);
            if (null != go)
            {
                RuntimePreviewGenerator.BackgroundColor = Color.white;
                images[i].texture = RuntimePreviewGenerator.GenerateModelPreview(go.transform);
            }
            else
            {
                images[i].texture = null;
            }
        }
        if (0 < images.Count)
        {
            images[selected].GetComponent<RawImage>().color = new Color(1, 1, 0, 1);
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float trendV = Input.GetAxisRaw("Vertical");
        float trendH = Input.GetAxisRaw("Horizontal");
        //Debug.Log(trendV);
        //Debug.Log(trendH);
        if (-0.2f <= trendV && trendV <= 0.2f &&
            -0.2f <= trendH && trendH <= 0.2f)
        {
            waitTime = 0;
            waitFlag = false;
        }
        if (!waitFlag)
        {
            int selected_old = selectedV * rowLength + selectedH;
            bool moveFlag = false;
            if (trendV > 0.2)
            {
                selectedV = Mathf.Max(selectedV - 1, 0);
                moveFlag = true;
            }
            else if (trendV < -0.2)
            {
                selectedV = Mathf.Min(selectedV + 1, (int)Mathf.Ceil(1.0f * images.Count / rowLength) - 1);
                moveFlag = true;
            }
            if (trendH > 0.2)
            {
                selectedH = Mathf.Min(selectedH + 1, rowLength - 1);
                moveFlag = true;
            }
            else if (trendH < -0.2)
            {
                selectedH = Mathf.Max(selectedH - 1, 0);
                moveFlag = true;
            }
            if (moveFlag)
            {
                images[selected_old].GetComponent<RawImage>().color = Color.white;
                int selected = selectedV * rowLength + selectedH;
                images[selected].GetComponent<RawImage>().color = new Color(1, 1, 0, 1);
                waitFlag = true;
            }
        }
        else
        {
            waitTime += Time.deltaTime;
            if (waitTime > 0.5f)
            {
                waitTime = 0;
                waitFlag = false;
            }
        }

        if (Input.GetButtonDown("js5"))
        {
            int selected = selectedV * rowLength + selectedH;
            GameObject go = inventory.Get(selected);
            if (null != go)
            {
                // choose weapon but do not remove (optional)
                go.SetActive(true);
                //go.GetComponent<GrabMovement>().enabled = true;
                //inventory.Remove(selected);
                //images.RemoveAt(selected);

                // close the panel
                Hide();
            }
            else
            {
                // close the panel
                Hide();
            }
        }
    }

    public void Show()
    {
        reticleActivator.Hide();
        speedCtrl.Lock();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        reticleActivator.Show();
        speedCtrl.Unlock();
        gameObject.SetActive(false);
    }
}
