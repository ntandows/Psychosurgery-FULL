using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeCollectable : MonoBehaviour
{
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isActive = false;
            Destroy(this.gameObject);
        }
    }
}
