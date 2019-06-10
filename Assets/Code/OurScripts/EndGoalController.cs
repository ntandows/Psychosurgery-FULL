using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoalController : MonoBehaviour
{
    private bool isReady; //must hit all checkpoints before proceeding
    private bool isHit;

    public GameController control;

    // Start is called before the first frame update
    void Start()
    {
        isReady = false;
        isHit = false;
    }

    public bool GetGoalStatus()
    {
        return isReady;
    }

    public void SetGoalStatus(bool value)
    {
        isReady = value;
    }

    public bool GetHitStatus()
    {
        return isHit;
    }

    public void SetHitStatus(bool value)
    {
        isHit = value;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReady == false && control.GetNumCheckpoints() == 0)
        {
            isReady = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isReady && Input.GetKeyDown(KeyCode.E))
        {
            control.hitWin();
            isHit = true;
        }
    }
}
