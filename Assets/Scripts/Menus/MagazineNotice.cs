using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagazineNotice : MonoBehaviour
{
    private ShootWeapon weapon;
    private float timeLeft = 300f;
    private DestroyableTarget target;
    // need to create new object to store this value
    private GameObject scoreSystem;
    public TMPro.TMP_Text ammoRemain;
    public TMPro.TMP_Text damage;
    public TMPro.TMP_Text totalScore;
    public TMPro.TMP_Text time;
    // Start is called before the first frame update
    void Start()
    {
        // ammoRemain = gameObject.GetComponentInChildren<TMP_Text>();
        // damage = gameObject.GetComponentInChildren<TMP_Text>();
        // weaponScore = gameObject.GetComponentInChildren<TMP_Text>();
        // ammoScore = gameObject.GetComponentInChildren<TMP_Text>();
//        text.text = "Dooooge";
        scoreSystem = GameObject.FindWithTag("ScoreSystem");
        damage.text = "Total damage:";
        totalScore.text = "Total score:";
        time.text = "5:00";
    }

    // Update is called once per frame
    void Update()
    {
        // Load camera for non-lobby scenes
        if (GetComponent<Canvas>().worldCamera is null)
        {
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        if(timeLeft>0)
        {
            timeLeft -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeLeft / 60F);
            int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
            time.text  = string.Format("{0:00}:{1:00}", minutes, seconds); 
            if (weapon)
            {
                ammoRemain.text = weapon.GetRemainingAmmo();
                
                // damage.text = weapon.GetDamage().ToString();
                // weaponScore.text = weapon.GetWeaponScore().ToString();
                // ammoScore.text = weapon.GetAmmoScore().ToString();
            }
            else
            {
                GameObject weaponHolder = GameObject.FindWithTag("Weapon");
                if (0 < weaponHolder.transform.childCount)
                {
                    weapon = weaponHolder.transform.GetChild(0).GetComponentInChildren<ShootWeapon>();
                }
            }
            // need to attach score system to the player
            if (scoreSystem)
            {
                Debug.Log("ScoreSystem found");

                int playerID = 0;
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (go.GetComponent<PlayerNetwork>().IsOwner)
                    {
                        playerID = (int)go.GetComponent<PlayerNetwork>().OwnerClientId;
                        break;
                    }
                }

                ScoreSystem.PlayerStats playerStats = scoreSystem.GetComponent<ScoreSystem>().GetPlayerStats(playerID);
                if (playerStats != null)
                {   
                    Color playerColor = playerStats.color;
                    Debug.Log(playerID + " panelcolor:" + playerColor);
                    totalScore.color = playerColor;
                    damage.color = playerColor;

                    int currentTotalScore = playerStats.totalScore;
                    int currentTotalDamage = playerStats.totalDamage;
                    damage.text = "Total damage: " + playerStats.totalDamage.ToString();
                    totalScore.text = "Total score: " + playerStats.totalScore.ToString();
                }
                // foreach(GameObject playerGO in GameObject.FindGameObjectsWithTag("Player")){
                //     int playerID = playerGO.GetComponent<Player>().GetPlayerID();
                //     ScoreSystem.PlayerStats playerStats = scoreSystem.GetComponent<ScoreSystem>().GetPlayerStats(playerID);
                //     if (playerStats != null)
                //     {   
                //         Color playerColor = playerStats.color;
                //         int currentTotalScore = playerStats.totalScore;
                //         int currentTotalDamage = playerStats.totalDamage;
                      
                //         damage.text = "damage: " + playerStats.totalDamage.ToString();
                //         TotalScore.text = "Total score: " + playerStats.totalScore.ToString();
                //     }
                // }
                
            }
            else
            {
                Debug.Log("ScoreSystem not found");
            }
        }
        else
        {
            time.text = "0:00 Time's Up!";
        }    
    }
}
