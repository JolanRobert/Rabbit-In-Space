using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenCheckActivity : MonoBehaviour
{
    void OnEnable()
    {
        AccessGarden.OnOpenGarden.Invoke();
    }
}
