using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
    Written by: Nicholas Robbins
    Last Edited: 1/13/2018
    Purpose: A data structure that contains all the possible best routes from one node to another upon request of an AI entity.
    This allows for the enemies to path in the environment without having to perform pathfinding.
*/

    [System.Serializable]
public struct PathStorage
{
    public int endNodeID;
    public List<Node> nodePath;
}
[System.Serializable]
public struct ConnectionStorage
{
    public int startNodeID;
    public List<PathStorage> storedPaths;
}

public class GridGraph : MonoBehaviour {

 
    [Header("3D Connections")]
    //Reference to all the nodes
    public Node[] refNodes;
    private int mNextID;


    //Pathing storage variables
    public List<ConnectionStorage> connectionMap;
    public List<PathStorage> pathStorage;
    ConnectionStorage mapStorage;



    //A* Variables
    List<Node> openList = new List<Node>();
    HashSet<Node> closed = new HashSet<Node>();
    List<Node> shortestPath = new List<Node>();

    //A* Debugging
    public Node StartNode; 
    public Node EndNode;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public int getNextID()
    {
        mNextID += 1;
        return mNextID;
    }

    //-------------------------------- AI Pathing Functions -----------------------------------------------

    public List<Node> getAIPath(Node start, Node end)
    {
        List<Node> path;
        foreach (ConnectionStorage connection in connectionMap)
        {
            if(connection.startNodeID == start.nodeID)
            {
                foreach (PathStorage storage in connection.storedPaths)
                {
                    if (storage.endNodeID == end.nodeID)
                    {
                        path = storage.nodePath;
                        return path;
                    }
                }
            }
        }
        //Debug.LogError("No Path Found");
        return null;
    }


    //-------------------------------- Node Editor Window Functions ----------------------------------------

    public void nodeEditorMain()
    {
        //Find all node objects in the scene
        refNodes = FindObjectsOfType<Node>();
        //Make sure all of the nodes and connections are up to date 
        resetNodes();
        //Create and store node paths
        createPaths();
    }
    public void resetNodes()
    {
        mNextID = 0;
        foreach ( Node tmpNode in refNodes)
        {
            tmpNode.resetNode();
            tmpNode.findConnections();
        }
        

    }

    public void resetGraph()
    {
        mNextID = 0;
        foreach (Node tmpNode in refNodes)
        {
            tmpNode.resetNode();
        }
        if (connectionMap == null)
        {
            connectionMap = new List<ConnectionStorage>();
        }
        //if not clear the map
        else
        {
            connectionMap.Clear();
        }
    }

    private void createPaths()
    {
        //Check if the dictionary of best paths is created
        if (connectionMap == null)
        {
            connectionMap = new List<ConnectionStorage>();
        }
        //if not clear the map
        else
        {
            connectionMap.Clear();
        }
        foreach (Node tmpNode in refNodes) //Each node
       {
           //Create a list which will contain all the best paths from this node
           pathStorage = new List<PathStorage>();
           
           for (int i = 0; i < refNodes.Length; i++)  //Each connection
           {
               //If the node is not the node being checked
               if (tmpNode.nodeID != refNodes[i].nodeID)
               {

                   findPath(tmpNode, refNodes[i]);
                    //Clear the closed and open list
                    closed.Clear();
                    openList.Clear();

                    //Make sure not to save a blank path
                    if(shortestPath.Count != 0)
                    {
                        //Now that the path is found, store it in the list
                        PathStorage newPath = new PathStorage();
                        //Assign the end node as the ID for this best path
                        newPath.endNodeID = refNodes[i].nodeID;
                        //Store the best path stack in the path storage
                        newPath.nodePath = new List<Node>(shortestPath);
                        //Add the this best path with ID to the list of path storages for this node
                        pathStorage.Add(newPath);
                    }



               }
           }

            //Debug.Log("Added connections for " + tmpNode.name);

            //Add the list of paths for that one node to the map storage
            mapStorage = new ConnectionStorage();
            mapStorage.startNodeID = tmpNode.nodeID;
            mapStorage.storedPaths = pathStorage;

           //Add the map storage to the overall connection container
           connectionMap.Add(mapStorage);
       }
    }

    public void pathingDebug()
    {
        //Get the path  from start to end
        List<Node> tmp = getAIPath(StartNode, EndNode);
        //Draw the path connecting the  nodes
        Debug.DrawLine(StartNode.transform.position, tmp[0].transform.position, Color.green, 1f);
        for (int i = 0; i < tmp.Count; i++)
        {
            //Debug.Log(tmp[i].gameObject.name);
            if(i != tmp.Count - 1)
            {
                Debug.DrawLine(tmp[i].transform.position, tmp[i + 1].transform.position, Color.green, 1f);
            }
        }

    }


    //-------------------------------- Pathing Creation Functions ------------------------------------------

    public void findPath(Node start, Node end)
    {
        //Calculate needed distances for the aStar Search
        calculateNodeDistances(refNodes, start.transform.position, end.transform.position);
        AStarSearch(start, end);
    }

    private List<Node> AStarSearch(Node Start, Node End)
    {
        Node current;
        
        //Initalize the open list with the starting node
        openList.Add(Start);
        
        while(openList.Count  >  0)
        {
            current = openList[0];
            //Get the smallest node in the open  list with the lowest fcost
            for (int i = 1; i < openList.Count; i++)
            {
                if(openList[i].fCost() <  current.fCost() || openList[i].fCost() == current.fCost()&& openList[i].hCost <  current.hCost)
                {
                    current = openList[i];
                }
            }

            //remove the current node from the open list
            openList.Remove(current);
            //Add the current node to the closed list
            closed.Add(current);

            //if current is the end node then the path has been found
            if(current == End)
            {
                getPath(Start, End);
               
                

                return null;
            }

            for(int j = 0;  j <  current.connectedNodes.Count; j++)
            {
                //Check if the closed list  already containes this connection
                if(closed.Contains(current.connectedNodes[j]))
                {
                    continue;
                }
                //Add the distance from the start node to the distance from the previous node to this node
                float movementCost = current.gCost + current.nodeDistance[j];
                if(movementCost < current.connectedNodes[j].gCost || !openList.Contains(current.connectedNodes[j]))
                {
                    current.connectedNodes[j].gCost = movementCost;
                    current.connectedNodes[j].hCost = Vector3.Distance(current.connectedNodes[j].transform.position, End.transform.position);
                    current.connectedNodes[j].Parent = current;
                }

                if(!openList.Contains(current.connectedNodes[j]))
                {
                    openList.Add(current.connectedNodes[j]);
                }

            }

        }


        return null;
    }

    void getPath(Node startNode, Node endNode)
    {
        //Reset the shortest  path list
        shortestPath.Clear();
        //Start from the end  and  work back
        Node currentNode = endNode;
        while(currentNode != startNode)
        {
            //Add the current node and move to the parented node
            shortestPath.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        //The path created is in reverse to we reverse the list
        shortestPath.Reverse();

        //for (int i = 0; i < shortestPath.Count; i++)
        //{
        //    Debug.Log(shortestPath[i].gameObject.name);
        //}
    }

    void calculateNodeDistances(Node[] nodeList, Vector3 startPos, Vector3 endPos)
    {
        foreach(Node n in nodeList)
        {
            //calculate gCost -> The distance from the starting node;
            n.gCost = Vector3.Distance(startPos, n.transform.position);

            //calculate hCost -> The distance from the end node 
            n.hCost = Vector3.Distance(endPos, n.transform.position);
        }
    }




    /*------------------ 2D Functionality ------------------*/

    void NodeEditor2DMain()
    {

    }





}
