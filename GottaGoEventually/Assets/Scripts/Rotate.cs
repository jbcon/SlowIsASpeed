using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public Vector3 rotateVec;

    void Start()
    { 
    }
    void Update()
    {
        transform.Rotate(rotateVec); 
    }
}
