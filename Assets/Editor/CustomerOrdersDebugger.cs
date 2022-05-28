using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
[CustomEditor(typeof(CustomerOrderManager))]
public class CustomerOrdersDebugger : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (!Application.isPlaying) return;
        if (!KitchenManager.Instance.inService) return;

        CustomerOrderManager orderManager = (CustomerOrderManager) target;
        
        if(GUILayout.Button("Force Serve")) orderManager.ForceServe();
    }
}
