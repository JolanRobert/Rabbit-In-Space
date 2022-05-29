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
        eventSystem.enabled = false;
        inputModule.enabled = false;
    }

    public void Enable() {
        eventSystem.enabled = true;
        inputModule.enabled = true;
    }
}
