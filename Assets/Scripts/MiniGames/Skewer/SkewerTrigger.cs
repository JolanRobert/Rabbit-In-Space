using System.Collections.Generic;
using UnityEngine;

namespace Skewer {
public class SkewerTrigger : MonoBehaviour {

    [SerializeField] private int skewerID;
    private List<DraggableDango> myDangos = new List<DraggableDango>();
    
    [SerializeField] private float xDelta;
    
    public Collider2D collider;

    private void OnTriggerEnter2D(Collider2D other) {
        var dango = other.GetComponent<DraggableDango>();
        if (myDangos.Count == 3) return;
        
        if (dango != null) {
            Vector3 dangoPos = dango.transform.position;
            var minX = (collider.bounds.center + collider.bounds.extents).x - xDelta;
            var maxX = (collider.bounds.center + collider.bounds.extents).x + xDelta;
            if (dangoPos.x >= minX && dangoPos.x <= maxX) {
                dango.inTrigger = this;
                
                DangoModel.Instance.SetPlayerDango(skewerID,myDangos.Count,dango.dangoColor);
                dango.gameObject.layer = LayerMask.NameToLayer("FixedDango");
                myDangos.Add(dango);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        var dango = other.GetComponent<DraggableDango>();
        if (dango != null && dango.inTrigger == this) {
            dango.inTrigger = null;
            
            DangoModel.Instance.SetPlayerDango(skewerID,myDangos.Count-1,DangoColor.NONE);
            dango.gameObject.layer = LayerMask.NameToLayer("Dango");
            myDangos.Remove(dango);
        }
    }
}
}