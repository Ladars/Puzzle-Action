using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Test : MonoBehaviour
{
    public abstract Vector3 RotateAround(Vector3 point, Vector3 origin, Quaternion rotation);
    public abstract Vector3 RayToPlanePoint(Ray ray, Vector3 planePoint, Vector3 planeNormal);
    public abstract void MinDistancePoints(Ray line1, Ray line2, out Vector3 point1, out Vector3 point2);
}
