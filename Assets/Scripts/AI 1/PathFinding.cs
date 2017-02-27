using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// basic A* pathfinding class 
/// **Fixes Implementation of Heap
/// </summary>

	public class PathFinding : MonoBehaviour {

		PathRequestManager requestManager;
		Grid grid;

		void Awake(){
			requestManager = GetComponent<PathRequestManager>();
			grid = GetComponent<Grid> ();

		}

    // calls a coroutine so that we can begin a Enumeration.
		public void StartFindPath(Vector3 startPos, Vector3 targetPos){
			StartCoroutine(FindPath(startPos,targetPos));
		}
		IEnumerator FindPath(Vector3 startPos, Vector3 targetPos){

			Vector3[] waypoints = new Vector3[0];
			bool pathSuccess = false;

			Node startNode = grid.NodeFromWorldPoint (startPos);
			Node targetNode = grid.NodeFromWorldPoint (targetPos);


			if (startNode.walkable && targetNode.walkable) {
				Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
				HashSet<Node> closedSet = new HashSet<Node> ();
				openSet.Add (startNode);

				while (openSet.Count > 0) {
					Node currentNode = openSet.RemoveFirst ();
	/////////////Optimized With Heap/////////////
	//			for(int i= 1; i < openSet.Count; i ++){
	//				if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost){
	//					currentNode = openSet[i];
	//				}
	//			}
	//			openSet.Remove(currentNode);
					closedSet.Add (currentNode);

					if (currentNode == targetNode) {
						pathSuccess = true;
						break;
					}

					foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
						if (!neighbour.walkable || closedSet.Contains (neighbour)) {
							continue;
						}

						int newMoveCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour);
						if (newMoveCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
							neighbour.gCost = newMoveCostToNeighbour;
							neighbour.hCost = GetDistance (neighbour, targetNode);
							neighbour.parent = currentNode;

							if (!openSet.Contains (neighbour))
								openSet.Add (neighbour);
							else {
								openSet.UpdateItem (neighbour);
							}
						}
					}
				}
			}
			yield return null;
			if(pathSuccess){
				waypoints = RetracePath(startNode, targetNode);
			}
			requestManager.FinishedProcessingPath (waypoints, pathSuccess);

		}
    // Array of Vectors used to recall nodes and retrace path selected by Pathfinding algorythim this is done by creating an
    // an array of Vector three Nodes from the "start position" to the "end position" these are generally assigned within the Inspector but can be randomized in game.
		Vector3[] RetracePath (Node startNode, Node endNode){
			List<Node> path = new List<Node> ();
			Node currentNode = endNode;

			while (currentNode !=startNode) {
				path.Add(currentNode);
				currentNode = currentNode.parent;
			}
			Vector3[] waypoints = SimplifyPath (path);
			Array.Reverse (waypoints);
			return waypoints;


		}

		Vector3[] SimplifyPath(List<Node> path){
			List<Vector3> waypoints = new List<Vector3> ();
			Vector2 directionOld = Vector2.zero;
			for (int i = 1; i <path.Count; i ++) {
				Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
				if(directionNew != directionOld){
					waypoints.Add(path[i].worldPosition);

				}
				directionOld = directionNew;
			}
			return waypoints.ToArray ();
		}

		int GetDistance(Node nodeA, Node nodeB){
			int dstX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
			int dstY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

			if (dstX > dstY)
				return 14 * dstY + 10 * (dstX - dstY);
			return 14 * dstX + 10 * (dstY - dstX);
		}
	}
