using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraChangeCollectable : MonoBehaviour
{
    private bool isActive;

    public Text cooldownText;

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
            cooldownText.text = "Camera Switch: Q";
            Destroy(this.gameObject);
        }
    }
}
