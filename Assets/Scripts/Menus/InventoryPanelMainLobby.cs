using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelMainLobby : MonoBehaviour
{
    private Inventory inventory;
    private GameObject displayedWeapon;
    private GameObject selectedWeapon;
    private GameObject noWeaponNotice;
    private int selectedIndex = 0;

    private Vector3 minScale = new Vector3(0.05f, 0.05f, 0.05f);
    private Vector3 maxScale = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 weaponScale = new Vector3(0.2f, 0.2f, 0.2f);
    private Quaternion weaponRotate = Quaternion.identity;

    private SpeedController speedCtrl;
    private ReticleActivator reticleActivator;
    private Camera mainCamera;

    private GameObject buttonPrev;
    private GameObject buttonNext;

    // Start is called before the first frame update
    void Start()
    {
        // use two controller to control speed anc reticle
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        reticleActivator = GameObject.Find("ReticleActivator").GetComponent<ReticleActivator>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        // find buttons form children
        buttonPrev = transform.Find("Prev").gameObject;
        buttonNext = transform.Find("Next").gameObject;
        noWeaponNotice = transform.Find("NoWeaponNotice").gameObject;

        // select the first weapon from the inventory
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        selectedWeapon = inventory.Get(selectedIndex);
        if (null != selectedWeapon)
        {
            noWeaponNotice.SetActive(false);
            DisplayWeapon(selectedWeapon);
            displayedWeapon.SetActive(false);
            buttonPrev.SetActive(true);
            buttonNext.SetActive(true);
        }
        else
        {
            noWeaponNotice.SetActive(true);
            buttonPrev.SetActive(false);
            buttonNext.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Load camera for non-lobby scenes
        if (GetComponent<Canvas>().worldCamera is null)
        {
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        // display selected weapon if exists, otherwise try to get one from the inventory
        if (null != selectedWeapon)
        {
            // in case of the inventory script is loaded after this script
            if (noWeaponNotice.activeSelf)
            {
                noWeaponNotice.SetActive(false);
                DisplayWeapon(selectedWeapon);
                buttonPrev.SetActive(true);
                buttonNext.SetActive(true);
            }

            // in case of reopening the panel
            if (null == displayedWeapon)
            {
                DisplayWeapon(selectedWeapon);
            }

            // for the first time this panel displays
            if (!displayedWeapon.activeSelf)
            {
                displayedWeapon.SetActive(true);
            }

            // update position
            displayedWeapon.transform.position = GetScreenCenterWorldPosition();
        }
        else
        {
            selectedWeapon = inventory.Get(selectedIndex);
        }

        // can press buttons only there is no notification
        if (!noWeaponNotice.activeSelf)
        {
            // select previous or next weapon
            if (Input.GetButtonDown("js3")) // Press Y
            {
                selectedIndex = (selectedIndex - 1 + inventory.Count()) % inventory.Count();
                selectedWeapon = inventory.Get(selectedIndex);
                DisplayWeapon(selectedWeapon);
            }
            else if (Input.GetButtonDown("js10")) // Press A
            {
                selectedIndex = (selectedIndex + 1) % inventory.Count();
                selectedWeapon = inventory.Get(selectedIndex);
                DisplayWeapon(selectedWeapon);
            }

            // scale up or down
            float trendV = Input.GetAxisRaw("Vertical");
            if (0.2 < trendV)
            {
                weaponScale = Vector3.Lerp(displayedWeapon.transform.localScale, maxScale, 0.0015f);
                displayedWeapon.transform.localScale = weaponScale;
            }
            else if(trendV < -0.2)
            {
                weaponScale = Vector3.Lerp(displayedWeapon.transform.localScale, minScale, 0.0075f);
                displayedWeapon.transform.localScale = weaponScale;
            }

            // rotate
            float trendH = Input.GetAxisRaw("Horizontal");
            if (0.2 < trendH)
            {
                displayedWeapon.transform.Rotate(0, 0.2f, 0);
                weaponRotate = displayedWeapon.transform.rotation;
            }
            else if(trendH < -0.2)
            {
                displayedWeapon.transform.Rotate(0, -0.2f, 0);
                weaponRotate = displayedWeapon.transform.rotation;
            }
        }

        if (Input.GetButtonDown("js5")) // Press B
        {
            Destroy(displayedWeapon);
            Hide();
        }
    }

    private void DisplayWeapon(GameObject selectedWeapon)
    {
        // destroy old displayed gameobject
        Destroy(displayedWeapon);
        
        // set weapon position
        Vector3 screenCenter = GetScreenCenterWorldPosition();
        if (Quaternion.identity == weaponRotate)
        {
            weaponRotate = selectedWeapon.transform.rotation;
        }
        // instantiate new displayed gameobject based on selected gameobject
        displayedWeapon = Instantiate(selectedWeapon, screenCenter, weaponRotate);

        // remove audio component
        if (displayedWeapon.TryGetComponent<AudioSource>(out AudioSource audioSource))
        {
            Destroy(audioSource);
        }
        // adjust scale
        displayedWeapon.transform.localScale = weaponScale;
        displayedWeapon.SetActive(true);
    }

    private Vector3 GetScreenCenterWorldPosition()
    {
        // set the screen middle point position in the world coordinator
        Vector3 screenCenter = mainCamera.ScreenToWorldPoint(
                                    new Vector3(mainCamera.pixelWidth / 2,
                                                mainCamera.pixelHeight / 2,
                                                0.5f));
        return screenCenter;
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
