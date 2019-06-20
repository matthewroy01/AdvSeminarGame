using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeEditor : EditorWindow{
    public GridGraph refGridGraph;

    //Code to make the window appear in the engine
    [MenuItem("Designer Tools/NodeEditor")]
    public static void ShowWindow()
    { 
        GetWindow<NodeEditor>("Node Editor");
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }


    private void OnGUI()
    {
        refGridGraph = GameObject.FindObjectOfType<GridGraph>();

        GUILayout.Label("Connect all nodes and create paths", EditorStyles.boldLabel);
        if(GUILayout.Button("CREATE CONNECTIONS"))
        {
            refGridGraph.nodeEditorMain();
            EditorUtility.SetDirty(refGridGraph);

            foreach(Node tmp in refGridGraph.refNodes)
            {
                EditorUtility.SetDirty(tmp);
            }
        }

        GUILayout.Label("Test a stored path with the grid graph", EditorStyles.boldLabel);
        if (GUILayout.Button("TEST CONNECTIONS"))
        {
            refGridGraph.pathingDebug();
        }

        GUILayout.Label("Reset all nodes", EditorStyles.boldLabel);
        if (GUILayout.Button("RESET GRAPH"))
        {
            refGridGraph.resetGraph();
        }
    }

    // Use this for initialization
    //void Start () {
    //	
    //}
    //
    //// Update is called once per frame
    //void Update () {
    //	
    //}
}
