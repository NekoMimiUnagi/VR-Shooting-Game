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
//        text.text = "Dooooge";
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon)
        {
            text.text = weapon.GetRemainingAmmo();
        }
        else
        {
            GameObject weaponHolder = GameObject.FindWithTag("Weapon");
            weapon = weaponHolder.transform.GetChild(0).GetComponentInChildren<ShootWeapon>();
        }
    }
}
