using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cutter
{
    public class Dough : MonoBehaviour
    {
        [SerializeField] private GameObject halfDoughPrefab;
        [SerializeField] private float popStrength;
        private float heightToFail;
        private void Start()
        {
            heightToFail = CutterManager.instance.startPosY;
        }
        private void FixedUpdate()
        {
            if (transform.position.y < heightToFail)
            {
                CutterManager.instance.Fail();
                Destroy(gameObject);
            }
        }
        private GameObject halfDough;
        private Rigidbody halfDoughRb;
        public void Slice()
        {
            CutterManager.instance.SucceedSlice();
            
            halfDough = Instantiate(halfDoughPrefab, transform.position, transform.rotation, transform.parent);
            halfDoughRb = halfDough.GetComponent<Rigidbody>();
            halfDoughRb.velocity = GetComponent<Rigidbody>().velocity;
            halfDoughRb.AddForce(-transform.right * popStrength);
            halfDoughRb.AddTorque(transform.forward * popStrength * Random.Range(0f,1f));
            Destroy(halfDough,2.5f);
            
            halfDough = Instantiate(halfDoughPrefab, transform.position, transform.rotation, transform.parent);
            halfDoughRb = halfDough.GetComponent<Rigidbody>();
            halfDough.transform.localScale = new Vector3(-1, 1, 1);
            halfDoughRb.velocity = GetComponent<Rigidbody>().velocity;
            halfDoughRb.AddForce(transform.right * popStrength);
            halfDoughRb.AddTorque(transform.forward * popStrength * Random.Range(0f,1f));
            Destroy(halfDough,2.5f);
            
            Destroy(gameObject);
        }
    }
}
