using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform player;

    private NavMeshAgent enemy;
    private InViewDetection inViewObject;
    private bool isPatrol;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.GetComponent<NavMeshAgent>();
        inViewObject = gameObject.GetComponent<InViewDetection>();
        isPatrol = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(inViewObject.GetInView())
        {
            //print("In view");
            isPatrol = false;
            enemy.SetDestination(player.position);
        }
        else
        {
            isPatrol = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.tag != "player" && isPatrol)
        {
            if (other.tag == "1")
            {
                enemy.SetDestination(pos2.position);
            }
            if (other.tag == "2")
            {
                enemy.SetDestination(pos3.position);
            }
            if (other.tag == "3")
            {
                enemy.SetDestination(pos4.position);
            }
            if (other.tag == "4")
            {
                enemy.SetDestination(pos1.position);
            }
        }
        
    }
}
