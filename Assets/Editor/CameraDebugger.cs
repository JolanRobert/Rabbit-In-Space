using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CameraController))]
public class CameraDebugger : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        if (!Application.isPlaying) return;
        
        CameraController camera = (CameraController) target;
        
        if (GUILayout.Button("Focus")) camera.FocusElement(PlayerManager.Instance.transform);
        if (GUILayout.Button("Reset")) camera.Reset();
    }
}
