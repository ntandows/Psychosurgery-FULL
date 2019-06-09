﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int numCheckpoints;
    private int numEnemies;
    private int nextCamera;
    private bool isDone;
    private bool isSet;
    private bool isMainActive;
    private Camera mainCamera;
    private Camera[] allCams;

    public Text winText;
    public Text checkpointText;
    public EndGoalController endControl;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        numCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        isDone = false;
        isSet = false;
        winText.text = "";
        checkpointText.text = "Checkpoints Left: " + numCheckpoints;
        isMainActive = true;
        allCams = Camera.allCameras;
        mainCamera = Camera.main;
        foreach(Camera i in allCams)
        {
            i.enabled = false;
        }
        mainCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSet && numCheckpoints == 0)
        {
            endControl.SetGoalStatus();
            isSet = true;
        }


        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isMainActive)
            {
                print("Switch Cameras");
                SwitchRandomCameras();
            }

            else
            {
                print("Switch To Main");
                SwitchToMainCamera();
            }
        }

    }

    private void SwitchRandomCameras()
    {
        nextCamera = Random.Range(1, allCams.Length);
        allCams[nextCamera].enabled = true;
        Camera.main.enabled = false;
        isMainActive = false;
        print(nextCamera);
    }

    private void SwitchToMainCamera()
    {
        allCams[nextCamera].enabled = false;
        mainCamera.enabled = true;
        isMainActive = true;
    }

    /*
     * setter when checkpoint is reached
     */
    public void LessCheckpoint()
    {
        numCheckpoints--;
        if (numCheckpoints == 0)
        {
            checkpointText.fontSize = 18;
            checkpointText.text = "All checkpoints hit, find the end goal!";
        }
        else
        {
            checkpointText.text = "Checkpoints left: " + numCheckpoints;
        }
    }

    /*
     * setter when enemy is eliminated
     */
    public void LessEnemy()
    {
        numEnemies--;
    }

    public int GetNumCheckpoints()
    {
        return this.numCheckpoints;
    }

    public int GetNumEnemies()
    {
        return this.numEnemies;
    }

    /*
     * test print statement
     */
    public void PrintDiagnostics()
    {
        print("PRINTING DIAGNOSTICS");
        print("numCheckpoints: " + numCheckpoints);
        print("numEnemies: " + numEnemies);

    }


    public void hitWin()
    {
        isDone = true;
        Win();
    }

    private void Win() 
    {
        if (isDone)
        {
            var remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < remainingEnemies.Length; i++)
            {
                Instantiate(explosion, remainingEnemies[i].transform.position, remainingEnemies[i].transform.rotation);
                Destroy(remainingEnemies[i]);
            }
            winText.text = "Level complete!";
        }

    }
}
