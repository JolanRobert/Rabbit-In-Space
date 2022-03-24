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
    public class FoldingDoughButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public Animator animator;
        public Image img;
        public bool isDragging;
        public Vector2 dragPosition;
        public Vector2 deltaDrag;
        public float minX, minY, maxX, maxY;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            Debug.Log("BeginDragFoldingButton");
            //img.DOColor(Color.yellow, .2f);
            dragPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            Debug.Log("OnEndDragFoldingButton");
            //img.DOColor(Color.white, .2f);
    
            deltaDrag = Camera.main.ScreenToWorldPoint(eventData.position) - (Vector3)dragPosition;
            MochiBeaterManager.Instance.TryFold(deltaDrag);
        }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}

