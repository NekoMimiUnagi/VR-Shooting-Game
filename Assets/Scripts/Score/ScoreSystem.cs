using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{   
    [SerializeField] private List<Player> playersList = new List<Player>();
    [SerializeField] private List<DestroyableTarget> destroyedTargetsList = new List<DestroyableTarget>();
    [SerializeField] private Dictionary<string, PlayerStats> playerStatsDict = new Dictionary<string, PlayerStats>();
    private int totalScore = 0;
    private int totalDamage = 0;
    private string playerName = "";
    // Start is called before the first frame update
    void Start()
    {
        // Loop through all the players in the game
        foreach (GameObject playerGO in GameObject.FindGameObjectsWithTag("Player"))
        {
            // Create a new Player object for each player in the game
            Player player = new Player(playerGO.name, playerGO.GetComponent<Renderer>().material.color);
            playersList.Add(player);
            playerStatsDict.Add(player.GetNickName(), new PlayerStats(player.GetColor(), 0, 0));
        }
        // Loop through all the targets in the game
        foreach (GameObject targetGO in GameObject.FindGameObjectsWithTag("Target"))
        {
            // Create a new DestroyableTarget object for each target in the game
            if ( targetGO.GetComponent<DestroyableTarget>().isDestroyed == true)
            {
                DestroyableTarget target = new DestroyableTarget();
                destroyedTargetsList.Add(target);
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Loop through all the players in the game and target, to check if they have identical names
        foreach (Player player in playersList)
        {
            // Update the player's score and damage
            foreach( DestroyableTarget target in destroyedTargetsList)
            {   
                BulletInfo bullet = target.GetBulletInfo();
                if (player.GetNickName() == bullet.GetPlayerName())
                {
                    int weaponScore = bullet.GetWeaponScore();
                    // weaponDamage = bullet.GetDamage();
                    int ammoScore = bullet.GetAmmoScore();
                    int targetScore = target.GetTargetScore();

                    playerStatsDict[player.GetNickName()].totalScore += weaponScore + ammoScore + targetScore;
                    // playerStatsDict[player.GetNickName()].totalScore += target.GetTargetScore();
                    playerStatsDict[player.GetNickName()].totalDamage += target.GetTargetDamage();
                }
            }
        }
    }

    public PlayerStats GetPlayerStats(string playerName)
    {
        return playerStatsDict[playerName];
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
