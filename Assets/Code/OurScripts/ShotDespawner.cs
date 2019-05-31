using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDespawner : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Shot"))
        {
            Destroy(other.gameObject);
        }
    }
}
