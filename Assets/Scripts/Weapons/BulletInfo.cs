using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private ShootWeapon weapon;
    // set default value
    private Color playerColor = Color.white;
    private string playerName = "Player";
    private string weaponName = "Weapon";
    private string bulletName = "Bullet";
    private int weaponScore = 0;
    private int weaponDamage = 0;
    private int ammoScore = 0; 
    
    void Start()
    {   
        
        weapon = GameObject.FindWithTag("Weapon").GetComponent<ShootWeapon>();
        weaponScore = weapon.GetWeaponScore();
        weaponDamage = weapon.GetDamage();
        ammoScore = weapon.GetAmmoScore();

        // player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player = weapon.transform.parent.parent.parent.parent.GetComponent<Player>();
        // player = this.gameObject.transform.parent.parent.parent.parent.GetComponent<Player>();
        // weapon = this.gameObject.transform.parent.parent.parent.GetComponent<ShootWeapon>();
        // player = bullet.Find("Player").GetComponent<Player>();
        // weapon = bullet.transform.parent.parent.parent.GetComponent<ShootWeapon>();
        if(player != null)
        {
            if( player.GetColor() != null){
                Debug.Log("playerColor not null");
                playerColor = player.GetColor();
            }
            if (player.GetNickName() != null)
            {
                Debug.Log("playerName not null");
                playerName = player.GetNickName();
            }
        }
        // Debug.Log(weapon.GetWeaponName());
        if (weapon != null)
        {
            Debug.Log("weapon not null");
            if (weapon.GetWeaponName() != null)
            {   Debug.Log("weaponName not null");
                weaponName = weapon.GetWeaponName();
            }
        }
        
        
        // Debug.Log(playerColor);
        // Debug.Log(playerName);
        // Debug.Log(weaponName);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public BulletInfo GetBulletInfo(GameObject bullet){
        return this;
    }

    public Color GetPlayerColor(){
        return playerColor;
    }
    public string GetPlayerName(){
        return playerName;
    }
    public string GetWeaponName(){
        return weaponName;
    }
    public string GetBulletName(){
        if (weaponName == "Rifle")
            return "RifleBullet";
        else if (weaponName == "Shotgun")
            return "ShotgunBullet";
        else if (weaponName == "Bow")
            return "BowBullet";
        else if (weaponName == "Crossbow")
            return "CrossbowBullet";

        return "RifleBullet";
    }
    public int GetWeaponScore(){
        return weaponScore;
    }
    public int GetWeaponDamage(){
        return weaponDamage;
    }
    public int GetAmmoScore(){
        return ammoScore;
    }




}
