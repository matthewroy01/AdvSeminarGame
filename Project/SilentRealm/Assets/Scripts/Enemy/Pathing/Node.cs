using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    Written by: Nicholas Robbins
    Last Edited: 1/13/2018
    Purpose: A point in space used as a waypoint for pathing. Stores data to create the best paths in the gridgraph 
*/
public class Node : MonoBehaviour {

    [Header("3D Variables")]
    public int nodeID; //Special identifier given to the node by the gridgraph
    public LayerMask wallLayer; //Wall detection
    public List<Node> connectedNodes; //All of the other nodes that this node has light of sight to
    public List<float> nodeDistance; //The distance that this node has to the other nodes that it has line of sight
    public GridGraph refGridGraph; //reference to the grid graph

    
   

    //PathFinding Variables
    public float gCost;
    public float hCost;
    public Node Parent;

    void Start()
    {
        //nodeID = refGridGraph.getNextID();
        //findConnections();
        //refGridGraph.refNodes.Add(this);

    }

    void Update()
    {

    }

    public void resetNode()
    {
        nodeID = refGridGraph.getNextID();
        connectedNodes.Clear();
        nodeDistance.Clear();
        resetPathing();
    }

    public void findConnections()
    {
        //Find all other node objects within the scene
        GameObject[] PossibleConnections = GameObject.FindGameObjectsWithTag("Node");
        float distance;
        
        
        for (int i = 0; i < PossibleConnections.Length; i++)
        {
            RaycastHit hit;
            distance = Vector3.Distance(transform.position, PossibleConnections[i].transform.position);

            //If the node cannot see the other node
            if (Physics.Raycast(transform.position, (PossibleConnections[i].transform.position  -  transform.position), out hit, distance, wallLayer))
            {

                Debug.DrawLine(transform.position, hit.point, Color.red, 1f);

            }
            else
            {
                //If the node can see the other node    
                if(nodeID != PossibleConnections[i].GetComponent<Node>().nodeID)
                {
                    //Make a connection to the viewable node
                    connectedNodes.Add(PossibleConnections[i].GetComponent<Node>());
                    nodeDistance.Add(distance);
                    Debug.DrawLine(transform.position, PossibleConnections[i].transform.position, Color.blue, 1f);
                }
            }
        }
    }

    void resetPathing()
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

    /*------------------ 2D Functionality ------------------*/




}
