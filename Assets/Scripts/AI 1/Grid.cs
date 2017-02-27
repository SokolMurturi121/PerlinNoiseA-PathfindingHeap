using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public bool displayGridGizmos;
	public Transform player;
	Node[,] grid;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public LayerMask unWakableMask;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start(){

		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt (gridWorldSize.x /nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y /nodeDiameter);
		CreateGrid ();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateGrid();
        }
    }

    public int MaxSize{
		get{
			return gridSizeX * gridSizeY;
		}
	}
// this function will be called at the start of the game and also if we want to do dynamic updates of the enviroment
	void CreateGrid(){
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y *nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius, unWakableMask));
				grid[x,y] = new Node(walkable,worldPoint,x,y);
			}
		}
	}
// list which will be called during path finding to see if path value of surrounding neighbour nodes have higher or lower H values paths will be determined based on these values 
	public List<Node> GetNeighbours(Node node){
		List<Node> neighbours = new List<Node> ();

		for (int x = -1; x<= 1; x++) {
			for (int y = -1; y<= 1; y++) {
				if(x ==0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY){
					neighbours.Add(grid[checkX,checkY]);
				} 
				
			}
		}
		return neighbours;
	}

    //this will be used to track the position of target locations based upon their node within the world.
	public Node NodeFromWorldPoint (Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid [x, y];

	}
// debug mech used to help determin paths and grid status
	void OnDrawGizmos(){

		if (grid != null && displayGridGizmos) {
			//Node playerNode = NodeFromWorldPoint (player.position);
			foreach (Node n in grid) {
				//Gizmos.color = (n.walkable) ? Color.white : Color.red;
				//if (playerNode == n) {
				//	Gizmos.color = Color.cyan;
				//}
                Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
                Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}
}
