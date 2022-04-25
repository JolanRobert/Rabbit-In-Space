using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ErrorCounter))]
public class ErrorCounterDebugger : Editor {
    
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        if (!Application.isPlaying) return;
        
        ErrorCounter errorCounter = (ErrorCounter) target;
        
        if (GUILayout.Button("Fail")) errorCounter.Fail();
    }
}
