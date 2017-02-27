using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

    public float speed = 2;
    public float searchingTurnSpeed = 120f;
	public float searchingDuration = 4f;
	//public float sightrange
	public Transform[] wayPoints;
	public Vector3[] myWaypoints;
    public Transform target;
    public FeildOfView feildOfView;
    public Unit unit;
    public PathRequestManager pathRequestManager;
    public PathFinding pathfinding;
    //public Transform eyes
    public Vector3 offset = new Vector3();
	public MeshRenderer meshRendererFlag;

	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public IEnemyState currentState;
	[HideInInspector] public PatrolState patrolState;
	[HideInInspector] public WanderState wanderState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AlertState alertState;


	private void Awake(){

		chaseState = new ChaseState (this);
		alertState = new AlertState (this);
		patrolState = new PatrolState (this);
		wanderState = new WanderState (this);

	}

	// Use this for initialization
	void Start ()
    {
		currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update ()
    {
		currentState.UpdateState ();
        target = wayPoints[patrolState.nextWaypoint];
    }

	private void OnTriggerEnter(Collider other){
		currentState.OnTriggerEnter (other);
	}
}
