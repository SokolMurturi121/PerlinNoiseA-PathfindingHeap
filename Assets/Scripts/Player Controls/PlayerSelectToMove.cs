using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectToMove : MonoBehaviour {

    public GameObject wayPointToInstansiate;
    public GameObject currentGO;
    public GameObject prefabPoint;
    public Transform newTarget;
    public Transform oldTarget;
    public Plane targetPlaneTransform;
    private Vector3 targetPostionToInstansiate;

    const int LEFT_MOUSE_BUTTON = 0;

	// Use this for initialization
	void Start () {
        targetPostionToInstansiate = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //var hitlol : RaycastHit;
            float targetPoint = 0;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

            if (Physics.Raycast(ray, out hit))
            {

                //Create the waypoint at this position
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    Instantiate(wayPointToInstansiate, hit.point, Quaternion.identity);
                }
                prefabPoint = wayPointToInstansiate;
                newTarget = prefabPoint.transform;
                BoxCollider boxCollider = hit.collider as BoxCollider;
                //Transform targetTransform = hit.distance as transform;
                if (boxCollider != null)
                {
                    //call function
                    currentGO = boxCollider.gameObject;
                    if (currentGO != null)
                    {
                        currentGO.GetComponent<Unit>().target = newTarget;
                        oldTarget = newTarget;
                        // newTarget = null;
                        Destroy(wayPointToInstansiate.gameObject);

                    }
                }
            }
        }
    }
}
