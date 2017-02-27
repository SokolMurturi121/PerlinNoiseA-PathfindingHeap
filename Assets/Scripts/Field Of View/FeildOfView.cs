using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeildOfView : MonoBehaviour {

    public float viewRadius;
    [Range(0,360)]
    public float ViewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public bool targetSeen;
    public Transform ChaseTarget;
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        targetSeen = false;
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay) {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisbleTargets();
        }
    }

    public void FindVisbleTargets() {
        visibleTargets.Clear();
        targetSeen = false;
        ChaseTarget = null;
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInView.Length; i++) {

            Transform target = targetsInView[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2) { 
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    targetSeen = true;
                    ChaseTarget = target;
                }
                else
                {
                    targetSeen = false;
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal ) {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
