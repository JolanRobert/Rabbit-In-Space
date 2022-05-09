using System.Collections.Generic;
using UnityEngine;

namespace Skewer {
public class SkewerTrigger : MonoBehaviour {
    
    private Collider2D collider;

    [SerializeField] private int skewerID;
    public List<DraggableDango> myDangos = new List<DraggableDango>();
    public float minPosX;
    
    [SerializeField] private float xDelta;

    void Start() {
        collider = GetComponent<Collider2D>();
        minPosX = (collider.bounds.center - collider.bounds.extents).x;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (myDangos.Count == 3) return;
        
        if (other.TryGetComponent(out DraggableDango dango)) {
            Vector3 dangoPos = dango.transform.position;
            var minX = (collider.bounds.center + collider.bounds.extents).x - xDelta;
            var maxX = (collider.bounds.center + collider.bounds.extents).x + xDelta;
            if (dangoPos.x >= minX && dangoPos.x <= maxX) {
                dango.SetTrigger(this);
                
                DangoModel.Instance.SetPlayerDango(skewerID,myDangos.Count,dango.dangoColor);
                dango.gameObject.layer = LayerMask.NameToLayer("FixedDango");
                myDangos.Add(dango);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.TryGetComponent(out DraggableDango dango)) {
            dango.SetTrigger(null);

            DangoModel.Instance.SetPlayerDango(skewerID,myDangos.Count-1,DangoColor.NONE);
            dango.gameObject.layer = LayerMask.NameToLayer("Dango");
            myDangos.Remove(dango);
        }
    }
}
}