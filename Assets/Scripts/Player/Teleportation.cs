using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Teleportation : MonoBehaviour
{
    public GameObject shootingRangeNotice;

    private List<GameObject> teleportationTargets = new List<GameObject>();
    private List<Bounds> bounds = new List<Bounds>();
    private bool readyFlag = false;
    private PlayerData playerData = null;
    private GameObject mainCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        // Add virtual place of all scenes for lobby
        if ("Lobby" == SceneManager.GetActiveScene().name)
        {
            for (int i = 1; i <= 3; ++i)
            {
                GameObject scene = GameObject.Find($"Scene {i} Shooting Range");
                teleportationTargets.Add(scene);
                bounds.Add(scene.GetComponent<Collider>().bounds);
            }
        }
        // Add virtual place of lobby for all scenes
        else
        {
            GameObject lobby = GameObject.Find("Lobby");
            teleportationTargets.Add(lobby);
            bounds.Add(lobby.GetComponent<Collider>().bounds);
        }

        // restore position and facing direction after teleportation
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        if (playerData.Exists(gameObject.name))
        {
            (Vector3 vector, Quaternion rotation) = playerData.GetRelativeTransform(gameObject.name);
            string fromSceneName = playerData.GetFromSceneName(gameObject.name);
            if ("Lobby" == fromSceneName)
            {
                transform.position = bounds[0].center + vector;
            }
            else
            {
                int index = int.Parse(fromSceneName.Substring(fromSceneName.Length - 1, 1));
                transform.position = bounds[index-1].center + vector;
            }
            mainCamera.transform.rotation = rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool inFlag = false;
        for (int i = 0; i < teleportationTargets.Count; ++i)
        {
            // get player's project point on the XZ plane
            Vector3 p_point = new Vector3(transform.position.x,
                                          teleportationTargets[i].transform.position.y,
                                          transform.position.z);

            // all logics happen if player is in any virtual shooting range
            if (bounds[i].Contains(p_point))
            {
                inFlag = true;
                if (null == shootingRangeNotice)
                {
                    shootingRangeNotice = GameObject.Find("ShootingRangeNotice");
                }
                shootingRangeNotice.transform.Find("Ready").gameObject.SetActive(true);

                // if shooting, the player is ready
                if (Input.GetButtonDown("js7"))
                {
                    readyFlag = !readyFlag;
                }

                if (readyFlag)
                {
                    // pop text to mention player to push shoot button to cancel ready status
                    if ("Lobby" == SceneManager.GetActiveScene().name)
                    {
                        shootingRangeNotice.GetComponentInChildren<TMP_Text>().text = "Press OK to cancel ready";
                    }

                    // teleport to corresponding space
                    Vector3 positionToCenter = p_point - bounds[i].center;
                    playerData.UpdateRelativeTransform(gameObject.name, positionToCenter, mainCamera.transform.rotation);
                    playerData.UpdateFromSceneName(gameObject.name, SceneManager.GetActiveScene().name);
                    if ("Lobby" == SceneManager.GetActiveScene().name)
                    {
                        SceneManager.LoadScene($"Scene{i+1}", LoadSceneMode.Single);
                    }
                    else
                    {
                        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
                    }
                }
                else
                {
                    // pop text to mention player to push shoot button to active ready status
                    if ("Lobby" == SceneManager.GetActiveScene().name)
                    {
                        shootingRangeNotice.GetComponentInChildren<TMP_Text>().text = "Press OK to get ready";
                    }
                    else
                    {
                        shootingRangeNotice.GetComponentInChildren<TMP_Text>().text = "Press OK to go lobby";
                    }
                }
            }
        }

        // if a player is not in any virtual shooting range, the play cannot be ready.
        if (!inFlag)
        {
            readyFlag = false;

            // close ready notice
            if (null == shootingRangeNotice)
            {
                shootingRangeNotice = GameObject.Find("ShootingRangeNotice");
            }
            shootingRangeNotice.transform.Find("Ready").gameObject.SetActive(false);
        }
    }
}
