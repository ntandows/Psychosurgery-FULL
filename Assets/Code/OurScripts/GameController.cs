using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int numCheckpoints;
    private int numEnemies;
    private bool isDone;
    private bool isSet;


    public Text winText;
    public Text checkpointText;
    public EndGoalController endControl;

    /*
     * setter when checkpoint is reached
     */
    public void LessCheckpoint()
    {
        numCheckpoints--;
        checkpointText.text = "Checkpoints left: " + numCheckpoints;
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

    // Start is called before the first frame update
    void Start()
    {
        numCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        isDone = false;
        isSet = false;
        winText.text = "";
        checkpointText.text = "Checkpoints Left: " + numCheckpoints;
        //PrintDiagnostics();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSet && numCheckpoints == 0)
        {
            endControl.SetGoalStatus();
            isSet = true;
        }
    }

    public void hitWin()
    {
        isDone = true;
        Win();
    }

    private void Win() 
    {
        winText.text = "Level complete!";
    }
}
