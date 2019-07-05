using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node2D : MonoBehaviour {

    public GameObject Test;

    [Header("Grid Spacing")]
    public float gridDistanceX;
    public float gridDistanceY;

    [Header("Node Variables")]
    public int nodeID; // Special identifier given to the node by the gridgraph
    public LayerMask wallLayer; // Wall detection
    public List<Node> connectedNodes; // All of the other nodes that this node has light of sight to
    public List<float> nodeDistance; // The distance that this node has to the other nodes that it has line of sight
    public GridGraph refGridGraph; // Reference to the grid graph

    // Pathfinding variables
    [HideInInspector]
    public float gCost;
    [HideInInspector]
    public float hCost;
    [HideInInspector]
    public Node Parent;

    float cornerDistance;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	//void Update () {
	//	
	//}

    public void ResetNode()
    {
        nodeID = refGridGraph.getNextID();
        connectedNodes.Clear();
        nodeDistance.Clear();
        ResetPathing();
    }

    public void FindConnections()
    {
        //Find all other node objects within the scene
        GameObject[] PossibleConnections = GameObject.FindGameObjectsWithTag("Node2D");
        float distance;

        CalculateCornerDistance();

        for (int i = 0; i < PossibleConnections.Length; i++)
        {
            RaycastHit hit;
            distance = Vector3.Distance(transform.position, PossibleConnections[i].transform.position);

            // Determine if the possible node is within the same neighborhood as this node
            if(distance == gridDistanceX || distance == gridDistanceY || distance == cornerDistance)
            {
                if(Physics.Linecast(transform.position, PossibleConnections[i].transform.position, out hit, wallLayer))
                {
                    Debug.DrawLine(transform.position, PossibleConnections[i].transform.position, Color.red, 1f);
                }
            }
            else
            {
                //If the node can see the other node    
                if (nodeID != PossibleConnections[i].GetComponent<Node>().nodeID)
                {
                    //Make a connection to the viewable node
                    connectedNodes.Add(PossibleConnections[i].GetComponent<Node>());
                    nodeDistance.Add(distance);
                    Debug.DrawLine(transform.position, PossibleConnections[i].transform.position, Color.blue, 1f);
                }
            }
        }
    }

    void CalculateCornerDistance()
    {
        // Instantiate a couple of dummy objects
        GameObject obj1 = Instantiate(new GameObject());
        GameObject obj2 = Instantiate(new GameObject());
        // Change the position of obj2 so that its at the "corner" of the other object
        obj2.transform.position = obj1.transform.position;
        obj2.transform.position = new Vector3(transform.position.x + gridDistanceX, transform.position.x + gridDistanceY, transform.position.z);
        // Calculate the corner distance
        cornerDistance = Vector2.Distance((Vector2)obj1.transform.position, (Vector2)obj2.transform.position);
        // Destroy the objects to clean up
        Destroy(obj1);
        Destroy(obj2);
    }

    void ResetPathing()
    {
        gCost = 0;
        hCost = 0;
        Parent = null;
    }

    public float fCost()
    {
        float tmp = gCost + hCost;
        return tmp;
    }
}
