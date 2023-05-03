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
        
        ///-------------------------------------------------------------------------------------//
        // edit this part for multiplayer
        //  foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        //     {
        //     player = go;

        //     }
        ///-------------------------------------------------------------------------------------//
        weapon = GameObject.FindWithTag("Weapon").transform.GetChild(0).GetComponent<ShootWeapon>();
        weaponScore = weapon.GetWeaponScore();
        weaponDamage = weapon.GetDamage();
        ammoScore = weapon.GetAmmoScore();

        player = GameObject.Find("Player").GetComponent<Player>();
        if(player != null)
        {
            Debug.Log("Player not null");
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

        if (weapon != null)
        {
            if (weapon.GetWeaponName() != null)
            {   Debug.Log("weaponName not null");
                weaponName = weapon.GetWeaponName();
            }
        }
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
