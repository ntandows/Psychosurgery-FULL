using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int numCheckpoints; //number of checkpoints left
    private int numEnemies; //number of enemies left
    private int nextCamera; //value of randomly generated camera
    private bool isDone; //if level is done or not
    private bool isSet; //if the end goal is ready to be activated
    private bool hasCameraChange; //if player has acquired ability to change cameras
    private bool isMainActive; //if main camera is the active camera or not
    private Camera mainCamera; //pointer to the main camera
    private Camera[] allCams; //list of all cameras in the level
    private GameObject tempCollectable;
    private CameraChangeCollectable change;

    public Text winText; //text for win message
    public Text checkpointText; //text for checkpoints left
    public Text cooldownText;
    public EndGoalController endControl; //pointer to end game controller
    public GameObject explosion; //reference for explosion GameObject

    // Start is called before the first frame update
    void Start()
    {
        numCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        isDone = false;
        isSet = false;
        hasCameraChange = false;

        winText.text = "";
        checkpointText.text = "Checkpoints Left: " + numCheckpoints;
        cooldownText.text = "Camera Switch: N/A";

        isMainActive = true;
        allCams = Camera.allCameras;
        mainCamera = Camera.main;
        foreach(Camera i in allCams)
        {
            i.enabled = false;
        }
        mainCamera.enabled = true;

        tempCollectable = GameObject.FindGameObjectWithTag("Collectable");
        change = tempCollectable.GetComponent<CameraChangeCollectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSet && numCheckpoints == 0)
        {
            endControl.SetGoalStatus(true);
            isSet = true;
        }


        if(!change.GetIsActive() && Input.GetKeyDown(KeyCode.Q))
        {
            if(!hasCameraChange)
            {
                hasCameraChange = true;
            }
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
            //blows every enemy up
            for (int i = 0; i < remainingEnemies.Length; i++)
            {
                Instantiate(explosion, remainingEnemies[i].transform.position, remainingEnemies[i].transform.rotation);
                Destroy(remainingEnemies[i]);
            }
            winText.text = "Level complete!";
        }

    }
}
