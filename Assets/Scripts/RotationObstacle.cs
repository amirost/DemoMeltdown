using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObstacle : MonoBehaviour
{

    public float rotationSpeed = 10f;

    public enum RotationDirection { Clockwise = 1,  Anticlockwise = -1 }

    public RotationDirection rotationDirection;

    void Update()
    {

            transform.Rotate(Vector3.up * rotationSpeed * (int)rotationDirection * Time.deltaTime);
        

    }
}
