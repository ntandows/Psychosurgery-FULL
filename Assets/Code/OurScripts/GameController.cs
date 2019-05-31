using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int numCheckpoints;
    private int numEnemies;

    /*
     * setter when checkpoint is reached
     */
    public void LessCheckpoint()
    {
        numCheckpoints--;
    }

    /*
     * setter when enemy is eliminated
     */
    public void LessEnemy()
    {
        numEnemies--;
    }

    /*
     * test print statement
     */
    public void PrintDiagnostics()
    {
        print("PRINTING DIAGNOSTICS");
        print("Initial numCheckpoints: " + numCheckpoints);
        print("Initial numEnemies: " + numEnemies);
        print("Checkpoints left: " + numCheckpoints);

    }

    // Start is called before the first frame update
    void Start()
    {
        numCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(numCheckpoints == 0 || numEnemies == 0)
        {
            Win();
            return;
        }
    }

    void Win() 
    { 
        
    }
}
