using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractOnButton : MonoBehaviour
{
    public GameController control;
    private bool isHit;

    private void Start()
    {
        isHit = false;
        //control = GetComponent<GameController>();
        control.PrintDiagnostics();
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && !isHit)
        {
            control.LessCheckpoint();
            isHit = true;
            print("Checkpoint hit");
        }
    }


}
