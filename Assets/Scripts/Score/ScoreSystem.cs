using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{   

    [SerializeField] private List<Player> playersList = new List<Player>();
    // [SerializeField] private List<DestroyableTarget> destroyedTargetsList = new List<DestroyableTarget>();
    [SerializeField] private List<AnimalScript> destroyedTargetsList = new List<AnimalScript>();
    [SerializeField] private Dictionary<int, PlayerStats> playerStatsDict = new Dictionary<int, PlayerStats>();

    public TMPro.TMP_Text WinningNote;
    public GameObject WinningPanel;
    // Start is called before the first frame update
    void Start()
    {
        // Loop through all the players in the game
        
        foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
        {
            // Create a new Player object for each player in the game
            // use this line when Renderer working 
            // Player player = new Player(playerGO.GetComponent<Renderer>().material.color);
            Player player = new Player(Color.white);

            playersList.Add(player);
            playerStatsDict.Add(player.GetPlayerID(), new PlayerStats(player.GetColor(), 0, 0));
        }
        // Loop through all the targets in the game
        foreach (GameObject targetGO in GameObject.FindGameObjectsWithTag("Target"))
        {
            // Create a new DestroyableTarget object for each target in the game
            // if ( targetGO.GetComponent<DestroyableTarget>().isDestroyed == true)
            // {
            //     DestroyableTarget target = new DestroyableTarget();
            //     destroyedTargetsList.Add(target);
            // }
             if ( targetGO.GetComponent<AnimalScript>().isDestroyed == true)
            {
                
                destroyedTargetsList.Add(targetGO.GetComponent<AnimalScript>());
            }
          
        }

       
    }

    // Update is called once per frame
    public void UpdatePlayerData(GameObject Target)
    {

        // Loop through all the players in the game and target, to check if they have identical names
         foreach (Player player in playersList)
        {
            // Update the player's score and damage
            // foreach( DestroyableTarget target in destroyedTargetsList)
            // foreach( AnimalScript target in destroyedTargetsList)
            // {   
            //     BulletInfo bullet = target.GetBulletInfo();
            //     Debug.Log("bullet: " + bullet);
            //     if (player.GetPlayerID() == bullet.GetPlayerID())
            //     {   int id = player.GetPlayerID();
            //         int weaponScore = bullet.GetWeaponScore();
            //         // int weaponDamage = bullet.GetWeaponDamage();
            //         int ammoScore = bullet.GetAmmoScore();
            //         int TargetScore = target.GetTargetScore();

            //         playerStatsDict[id].totalScore += weaponScore + ammoScore + TargetScore;
            //         playerStatsDict[id].totalDamage += target.GetTargetDamage();
            //         // Debug.Log("Player " + id + " total score: " + playerStatsDict[id].totalScore);
            //     }
            // }
            // Update the player's score and damage
            int playerID = player.GetPlayerID();
            BulletInfo bullet = Target.GetComponent<AnimalScript>().GetBulletInfo();
            if( playerID == bullet.GetPlayerID())
            {
                int weaponScore = bullet.GetWeaponScore();
                // int weaponDamage = bullet.GetWeaponDamage();
                int ammoScore = bullet.GetAmmoScore();
                int TargetScore = Target.GetComponent<AnimalScript>().GetTargetScore();
                int TargetDamage = Target.GetComponent<AnimalScript>().GetTargetHealth();
                playerStatsDict[playerID].totalScore += weaponScore + ammoScore + TargetScore;
                playerStatsDict[playerID].totalDamage += TargetDamage;
                if (playerStatsDict[playerID].totalScore >= 500)
                {
                    WinningNote.text = "Player " + playerID + " wins!";
                    WinningPanel.SetActive(true);
                }
            }
        }
    }

    public PlayerStats GetPlayerStats(int playerID)
    {
        return playerStatsDict[playerID];
    }

    public class PlayerStats
        {
            public Color color;
            public int totalScore;
            public int totalDamage;

            public PlayerStats(Color playerColor, int playerTotalScore, int playerTotalDamage)
            {
                color = playerColor;
                totalScore = playerTotalScore;
                totalDamage = playerTotalDamage;
            }
        }
}
