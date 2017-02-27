using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolState : IEnemyState
{

    private StatePatternEnemy enemy;
    public int nextWaypoint;
    public Vector3[] path;

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
      //  if (Vector3.Distance(enemy.transform.position, enemy.wayPoints[nextWaypoint].position) < 5.0f) {
      //      enemy.unit.StopCoroutine("FollowPath");
      //      enemy.unit.StartCoroutine("FollowPath");
     //       enemy.pathRequestManager.RequestPath(enemy.transform.position, enemy.wayPoints[nextWaypoint].position, enemy.unit.OnPathFound);
      //  }
        look();
        patrol();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            ToAlertState();
    }

    public void ToPatrolState()
    {
        Debug.Log("Can not transition to same state");
    }

    public void ToWanderState()
    {
        Debug.Log("No Wander state");
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }
    //private look function to check if player is within FOV which then sets chase target to that target
    void patrol()
    {
        enemy.meshRendererFlag.material.color = Color.green;
        enemy.transform.LookAt(enemy.wayPoints[nextWaypoint].position);
        enemy.pathRequestManager.RequestPath(enemy.transform.position, enemy.wayPoints[nextWaypoint].position, enemy.unit.OnPathFound);
        if (Vector3.Distance(enemy.transform.position, enemy.wayPoints[nextWaypoint].position) < 5.0f)
        {
            nextWaypoint = (nextWaypoint + 1) % enemy.wayPoints.Length;
            enemy.pathRequestManager.RequestPath(enemy.transform.position, enemy.wayPoints[nextWaypoint].position, enemy.unit.OnPathFound);
        }

    }
    private void look()
    {
       if( enemy.feildOfView.targetSeen == true)
        {
            //enemy.target = enemy.feildOfView.ChaseTarget;
            ToChaseState();
            Debug.Log("I SEE PLAYER");
        }
    }

}
