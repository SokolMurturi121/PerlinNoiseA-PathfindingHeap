using UnityEngine;
using System.Collections;

public class WanderState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public WanderState(StatePatternEnemy statePatternEnemy){
		enemy = statePatternEnemy;
	}


	public void UpdateState(){
		
	}
		
	public void OnTriggerEnter(Collider other){

	}

	public void ToPatrolState(){

	}

	public void ToWanderState(){

	}

	public void ToAlertState(){

	}

	public void ToChaseState(){

	}
}
