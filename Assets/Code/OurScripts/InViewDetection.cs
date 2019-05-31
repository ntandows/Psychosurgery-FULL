using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InViewDetection : MonoBehaviour
{

    public Transform player;
    public float radius;
    public float angle;

    private bool inview = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Vector3 leftView = Quaternion.AngleAxis(angle, transform.up) * transform.forward * radius;
        Vector3 rightView = Quaternion.AngleAxis(-angle, transform.up) * transform.forward * radius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftView);
        Gizmos.DrawRay(transform.position, rightView);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * radius);

        if (!inview)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * radius);

        
    }

    public static bool inView(Transform checkObject, Transform target, float checkAngle, float checkRadius)
    {
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkObject.position, checkRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkObject.forward, directionBetween);

                    if (angle <= checkAngle)
                    {
                        Ray ray = new Ray(checkObject.position, target.position - checkObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, checkRadius))
                        {
                            if(hit.transform == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    private void Update()
    {
        inview = inView(transform, player, angle, radius);
    }
}
