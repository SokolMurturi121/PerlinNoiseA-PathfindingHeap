using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public ChaseState(StatePatternEnemy statePatternEnemy)
    {
		enemy = statePatternEnemy;
	}


	public void UpdateState()
    {
        look();
        chase();

    }
	
	public void OnTriggerEnter(Collider other)
    {
		
	}
	
	public void ToPatrolState()
    {
		
	}
	
	public void ToWanderState()
    {
		
	}
	
	public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
	}
	
	public void ToChaseState()
    {
		
	}

    private void look()
    {
        if (enemy.feildOfView.targetSeen == true)
        {
            enemy.target = enemy.feildOfView.ChaseTarget;
            ToChaseState();
            Debug.Log("I SEE PLAYER");
        }
        else
        {
            ToAlertState();
        }
    }
    private void chase()
    {
        enemy.meshRendererFlag.material.color = Color.red;
        enemy.transform.LookAt(enemy.feildOfView.ChaseTarget);
        enemy.pathRequestManager.RequestPath(enemy.transform.position, (enemy.target.transform.position + enemy.offset), enemy.unit.OnPathFound);
        if (Vector3.Distance(enemy.transform.position, enemy.target.transform.position) < 50.0f)
        {
            enemy.unit.speed = 2f;
            enemy.speed = 2f;
            shoot();
        }
    }
    private void shoot()
    {
        Debug.Log("PEW PEW PEW");
    }
}
