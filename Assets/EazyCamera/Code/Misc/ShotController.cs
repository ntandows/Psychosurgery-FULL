using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float speed;
    //public GameObject shotSpawn;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(0.0f, 0.0f, 1.0f * speed);
        rb.velocity = transform.forward * speed;
    }


}
