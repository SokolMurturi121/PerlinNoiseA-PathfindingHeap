using UnityEngine;
using System.Collections;

public class AlertState : IEnemyState {

	private readonly StatePatternEnemy enemy;
    private float searchTimer;

	public AlertState(StatePatternEnemy statePatternEnemy)
    {
		enemy = statePatternEnemy;
	}

	public void UpdateState()
    {
        look();
        search();

    }
	
	public void OnTriggerEnter(Collider other)
    {
		
	}

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f;
    }
	
	public void ToWanderState()
    {
        Debug.Log("Can not transition to wander state");
    }
	
	public void ToAlertState()
    {
        Debug.Log("Can not transition to same state");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0f;
    }

    private void look()
    {
        if (enemy.feildOfView.targetSeen == true)
        {
            enemy.target = enemy.feildOfView.ChaseTarget;
            ToChaseState();
            Debug.Log("I SEE PLAYER");
        }
    }
    private void search()
    {
        enemy.meshRendererFlag.material.color = Color.yellow;
        enemy.unit.StopAllCoroutines();
        enemy.transform.Rotate(0f, enemy.searchingTurnSpeed * Time.deltaTime, 0f);
        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.searchingDuration)
        ToPatrolState();
    }
}