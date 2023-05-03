using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using TMPro;

public class Teleportation : NetworkBehaviour
{
    private List<GameObject> teleportationTargets = new List<GameObject>();
    private List<Bounds> bounds = new List<Bounds>();
    private Player playerData = null;
    private GameObject mainCamera = null;
    private SpeedController speedCtrl = null;
    private GameObject shootingRangeNotice = null;

    private bool readyFlag = false;
    private bool newToScene = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return;

        playerData = GetComponent<Player>();
        GameObject mainMenu = GameObject.Find("MainMenu");
        speedCtrl = GameObject.Find("SpeedController").GetComponent<SpeedController>();
        mainCamera = GameObject.FindWithTag("MainCamera");

        // restore speed after teleportation
        if (null == mainMenu || !mainMenu.activeSelf)
        {
            CharacterMovement charaMove = GameObject.Find(gameObject.name).GetComponent<CharacterMovement>();
            charaMove.speed = speedCtrl.GetSpeed();
        }
    }

    private void RestoreTransform()
    {
        // restore position and facing direction after teleportation
        string fromSceneName = playerData.GetFromSceneName();
        (Vector3 vector, Quaternion rotation) = playerData.GetRelativeTransform();

        float y = 0f;
        // rotate relative position vector based on scenes
        if ("Scene1" == SceneManager.GetActiveScene().name)
        {
            ;
        }
        else if ("Scene2" == SceneManager.GetActiveScene().name)
        {
            vector = Quaternion.AngleAxis(90, Vector3.up) * vector;
            y = 2.1f; // test result in the scene2
        }
        else if ("Scene2" == SceneManager.GetActiveScene().name)
        {
            ;
        }
        else if ("MainLobby" == SceneManager.GetActiveScene().name)
        {
            if ("Scene1" == fromSceneName)
            {
                ;
            }
            else if ("Scene2" == fromSceneName)
            {
                vector = Quaternion.AngleAxis(-90, Vector3.up) * vector;
            }
            else if ("Scene3" == fromSceneName)
            {
                ;
            }
        }
        // assign stored position to the player in the current scene
        if ("" == fromSceneName)
        {
            return;
        }
        else if ("MainLobby" == fromSceneName)
        {
            // teleport to shooting range
            GameObject shootingRange = GameObject.Find("ShootingRange");
            Bounds shootingRangeBound = shootingRange.GetComponent<Collider>().bounds;
            transform.position = shootingRangeBound.center + vector;
            transform.position = new Vector3(transform.position.x,
                                             y,
                                             transform.position.z);
        }
        else
        {
            int index = int.Parse(fromSceneName.Substring(fromSceneName.Length - 1, 1));
            Vector3 position = bounds[index - 1].center + vector;
            transform.position = new Vector3(position.x, 1, position.z);
        }

        // assign stored rotation to the player in the current scene
        mainCamera.transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        //Debug.Log(newToScene + "--" + playerData.GetFromSceneName() + "--" + SceneManager.GetActiveScene().name + "--" + teleportationTargets.Count);

        // Add virtual place of all scenes for the lobby
        if ("MainLobby" == SceneManager.GetActiveScene().name && teleportationTargets.Count != 3)
        {
            teleportationTargets = new List<GameObject>();
            bounds = new List<Bounds>();
            for (int i = 1; i <= 3; ++i)
            {
                GameObject scene = GameObject.Find($"Scene {i} Shooting Range");
                teleportationTargets.Add(scene);
                bounds.Add(scene.GetComponent<Collider>().bounds);
            }
        }
        // Add virtual place of lobby for all scenes
        else if ("MainLobby" != SceneManager.GetActiveScene().name && teleportationTargets.Count != 1)
        {
            teleportationTargets = new List<GameObject>();
            bounds = new List<Bounds>();
            // add shooting range place
            GameObject lobby = GameObject.Find("Lobby");
            teleportationTargets.Add(lobby);
            bounds.Add(lobby.GetComponent<Collider>().bounds);
        }

        // restore transform position and rotation
        if (newToScene && playerData.GetFromSceneName() != SceneManager.GetActiveScene().name)
        {
            RestoreTransform();
            newToScene = false;
        }

        // find the notice canvas and save it for future using
        if (null == shootingRangeNotice)
        {
            shootingRangeNotice = GameObject.Find("ShootingRangeNotice");
        }

        // iterate all shooting fields to judge which field is the current player in
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
                // set transform for teleporting to corresponding space and from scene
                Vector3 positionToCenter = p_point - bounds[i].center;
                playerData.UpdateRelativeTransform(positionToCenter, mainCamera.transform.rotation);
                playerData.UpdateFromSceneName(SceneManager.GetActiveScene().name);

                inFlag = true;
                shootingRangeNotice.transform.Find("Ready").gameObject.SetActive(true);

                // if press B, the player is ready
                if (Input.GetButtonDown("js5"))
                {
                    readyFlag = !readyFlag;
                    if (readyFlag)
                    {
                        // pop text to mention player to push shoot button to cancel ready status
                        if ("MainLobby" == SceneManager.GetActiveScene().name)
                        {
                            shootingRangeNotice.GetComponentInChildren<TMP_Text>().text = "Press B to cancel ready";
                        }
                        GetComponent<PlayerNetwork>().SetReady(i + 1);
                    }
                    else
                    {
                        // pop text to mention player to push shoot button to active ready status
                        if ("MainLobby" == SceneManager.GetActiveScene().name)
                        {
                            shootingRangeNotice.GetComponentInChildren<TMP_Text>().text = "Press B to get ready";
                        }
                        else
                        {
                            shootingRangeNotice.GetComponentInChildren<TMP_Text>().text = "Press B to go lobby";
                        }
                        GetComponent<PlayerNetwork>().SetReady(0);
                    }

                    if (readyFlag && IsServer)
                    {
                        bool allReady = GetComponent<PlayerNetwork>().AllReady();
                        Debug.Log(allReady);
                        // if not all ready, the host cannot be ready
                        if (!allReady)
                        {
                            readyFlag = !readyFlag;
                            GetComponent<PlayerNetwork>().SetReady(0);
                        }
                        else
                        {
                            // reset ready status
                            GetComponent<PlayerNetwork>().ClearAllReady();
                            // reset scene status
                            GetComponent<PlayerNetwork>().ResetSceneStatusClientRpc();
                            // switch scene
                            string nextScene = "";
                            if ("MainLobby" == SceneManager.GetActiveScene().name)
                            {
                                nextScene = "Scene" + (i + 1).ToString();
                            }
                            else
                            {
                                nextScene = "MainLobby";
                            }
                            NetworkManager.SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
                        }
                    }
                }

                break;
            }
        }

        // if a player is not in any virtual shooting range, the play cannot be ready.
        if (!inFlag)
        {
            readyFlag = false;
            if (shootingRangeNotice.activeSelf)
            {
                shootingRangeNotice.transform.Find("Ready").gameObject.SetActive(false);
            }
        }
    }

    public void ResetSceneStatus()
    {
        // reset scene status
        newToScene = true;
        readyFlag = false;
    }
}
