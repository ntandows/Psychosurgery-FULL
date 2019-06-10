using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int numCheckpoints; //number of checkpoints left
    private int numEnemies; //number of enemies left

    private bool isDone; //if level is done or not
    private bool isSet; //if the end goal is ready to be activated
    
    public Text winText; //text for win message
    public Text checkpointText; //text for checkpoints left
    public Text cooldownText;
    public EndGoalController endControl; //pointer to end game controller
    public GameObject explosion; //reference for explosion GameObject
    public float cooldown; //cooldown for camera switch ability

    // Start is called before the first frame update
    void Start()
    {
        numCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        isDone = false;
        isSet = false;

        winText.text = "";
        checkpointText.text = "Checkpoints Left: " + numCheckpoints;
        cooldownText.text = "Camera Switch: N/A";

    }

    // Update is called once per frame
    void Update()
    {
        //keeps checking if end goal if ready to be updated
        if (!isSet && numCheckpoints == 0)
        {
            endControl.SetGoalStatus(true);
            isSet = true;
        }
        
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
            checkpointText.text = "All checkpoints hit\nFind the end goal!";
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
