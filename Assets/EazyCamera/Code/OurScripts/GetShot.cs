using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Shot"))
        {
            Destroy(other.gameObject);
            //Destroy(this.gameObject);
        }
    }
}
