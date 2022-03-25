using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using MochiBeater;

namespace MochiBeater
{
    public class FoldingDoughButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public bool isDragging;
        public Vector2 dragPosition;
        public Vector2 deltaDrag;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            Debug.Log("BeginDragFoldingButton");
            dragPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            Debug.Log("OnEndDragFoldingButton");
    
            deltaDrag = Camera.main.ScreenToWorldPoint(eventData.position) - (Vector3)dragPosition;
            MochiBeaterManager.Instance.TryFold(deltaDrag);
        }
    }
}

