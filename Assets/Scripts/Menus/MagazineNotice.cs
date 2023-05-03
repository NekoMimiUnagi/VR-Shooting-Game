using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagazineNotice : MonoBehaviour
{
    private ShootWeapon weapon;
    private TMPro.TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Load camera for non-lobby scenes
        if (GetComponent<Canvas>().worldCamera is null)
        {
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        if (weapon)
        {
            text.text = weapon.GetRemainingAmmo();
        }
        else
        {
            GameObject weaponHolder = GameObject.FindWithTag("Weapon");
            if (0 < weaponHolder.transform.childCount)
            {
                weapon = weaponHolder.transform.GetChild(0).GetComponentInChildren<ShootWeapon>();
            }
        }
    }
}
