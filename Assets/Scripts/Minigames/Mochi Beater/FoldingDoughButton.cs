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
    public class FoldingDoughButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler//, IBeginDragHandler, IEndDragHandler
    {
        public bool dragValidity;
        public bool isDragging;
        public Vector2 dragPosition;
        //public Vector2 deltaDrag;

        public void OnPointerUp(PointerEventData eventData)
        {
            isDragging = true;
            dragValidity = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isDragging = false;

            if (dragValidity)
            {
                //deltaDrag = Camera.main.ScreenToWorldPoint(eventData.position) - (Vector3)dragPosition;
                MochiBeaterManager.Instance.TryFold();
            }
        }

        /*public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            dragValidity = true;
            dragPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        }*/
    
        /*public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;

            if (dragValidity)
            {
                //deltaDrag = Camera.main.ScreenToWorldPoint(eventData.position) - (Vector3)dragPosition;
                MochiBeaterManager.Instance.TryFold();
            }
        }*/

        public void StompFinger()
        {
            dragValidity = false;
            isDragging = false;
        }
    }
}

