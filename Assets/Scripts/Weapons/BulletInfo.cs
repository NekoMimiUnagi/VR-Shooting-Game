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
    //private string bulletName = "Bullet";
    private int weaponScore = 0;
    private int weaponDamage = 0;
    private int ammoScore = 0; 
    private int playerID = 0;
    void Start()
    {   
        
        ///-------------------------------------------------------------------------------------//
        // edit this part for multiplayer
        //  foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        //     {
        //     player = go;

        //     }

        // GetComponent<PlayerNetwork>().OwnerClientId
        ///-------------------------------------------------------------------------------------//
        weapon = GameObject.FindWithTag("Weapon").transform.GetChild(0).GetComponent<ShootWeapon>();
        weaponScore = weapon.GetWeaponScore();
        weaponDamage = weapon.GetDamage();
        ammoScore = weapon.GetAmmoScore();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (go.GetComponent<PlayerNetwork>().IsOwner)
            {
                player = go.GetComponent<Player>();
                playerID = (int)go.GetComponent<PlayerNetwork>().OwnerClientId;
                playerColor = go.GetComponent<Renderer>().material.color;
                Debug.Log("bulletinfo: " + playerColor);
                break;
            }
        }

        //player = GameObject.Find("Player").GetComponent<Player>();
        //playerID = 0;
        //playerColor = Color.white;
        /*
        if(player != null)
        {
            Debug.Log("Player not null");
            if(player.GetColor() != null){
                Debug.Log("playerColor not null");
                playerColor = player.GetColor();
            }
            if (player.GetNickName() != null)
            {
                Debug.Log("playerName not null");
                playerName = player.GetNickName();
            }
        }
        */

        if (weapon != null)
        {
            if (weapon.GetWeaponName() != null)
            {
                Debug.Log("weaponName not null");
                weaponName = weapon.GetWeaponName();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BulletInfo GetBulletInfo(){
        return this;
    }
    
    public int GetPlayerID(){
        return playerID;
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
