using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagazineNotice : MonoBehaviour
{
    private ShootWeapon weapon;
    private float timeLeft = 300f;
    public TMPro.TMP_Text ammoRemain;
    public TMPro.TMP_Text damage;
    public TMPro.TMP_Text weaponScore;
    public TMPro.TMP_Text ammoScore;
    public TMPro.TMP_Text time;
    // Start is called before the first frame update
    void Start()
    {
        // ammoRemain = gameObject.GetComponentInChildren<TMP_Text>();
        // damage = gameObject.GetComponentInChildren<TMP_Text>();
        // weaponScore = gameObject.GetComponentInChildren<TMP_Text>();
        // ammoScore = gameObject.GetComponentInChildren<TMP_Text>();
//        text.text = "Dooooge";
        damage.text = "damage:";
        weaponScore.text = "weapon score:";
        ammoScore.text = "ammo score:";
        time.text = "5:00";
    }

    // Update is called once per frame
    void Update()
    {   
        if(timeLeft>0)
        {
            timeLeft -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeLeft / 60F);
            int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
            time.text  = string.Format("{0:00}:{1:00}", minutes, seconds); 
            if (weapon)
            {
                ammoRemain.text = weapon.GetRemainingAmmo();
                damage.text = weapon.GetDamage().ToString();
                weaponScore.text = weapon.GetWeaponScore().ToString();
                ammoScore.text = weapon.GetAmmoScore().ToString();
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
        else{
                time.text = "0:00 Time's Up!";

            }
            
    }
}
