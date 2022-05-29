using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemToggler : MonoBehaviour
{
    public static EventSystemToggler Instance;
    private EventSystem eventSystem;
    private StandaloneInputModule inputModule;
        

    void Awake() {
        Instance = this;
    }

    private void Start() {
        eventSystem = GetComponent<EventSystem>();
        inputModule = GetComponent<StandaloneInputModule>();
    }

    public void Cut() { 
        Debug.Log(0);
        eventSystem.enabled = false;
        inputModule.enabled = false;
    }

    public void Enable() {
        Debug.Log(1);
        eventSystem.enabled = true;
        inputModule.enabled = true;
    }
}
