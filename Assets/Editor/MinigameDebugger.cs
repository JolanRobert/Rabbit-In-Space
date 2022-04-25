using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MinigameManager))]
public class MinigameDebugger : Editor {
    
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        if (!Application.isPlaying) return;
        
        MinigameManager minigameManager = (MinigameManager) target;
        
        if(!minigameManager.resultPending) return;
        if (GUILayout.Button("Win")) minigameManager.EndMinigame(true);
        if (GUILayout.Button("Lose")) minigameManager.EndMinigame(false);
    }
}
