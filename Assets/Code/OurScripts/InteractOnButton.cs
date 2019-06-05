using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractOnButton : MonoBehaviour
{
    public GameController control;
    public Text interactText;

    private bool isHit;

    private void Start()
    {
        interactText.text = "";

        isHit = false;
        //control = GetComponent<GameController>();
        control.PrintDiagnostics();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.text = "Press E to interact";

            if (Input.GetKeyDown(KeyCode.E) && !isHit)
            {
                control.LessCheckpoint();
                isHit = true;
                print("Checkpoint hit");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactText.text = "";
        }
    }


}
