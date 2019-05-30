using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractOnButton : MonoBehaviour
{
    private GameController control;
    private bool isHit;

    private void Start()
    {
        isHit = false;
        control = gameObject.GetComponent(typeof(GameController)) as GameController;
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
