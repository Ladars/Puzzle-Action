using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestImplement : Test
{
    public override void MinDistancePoints(Ray line1, Ray line2, out Vector3 point1, out Vector3 point2)
    {
        Vector3 direction1 = line1.direction.normalized;
        Vector3 direction2 = line2.direction.normalized;
        Vector3 pointOnLine1 = line1.origin;
        Vector3 pointOnLine2 = line2.origin;

        float a = Vector3.Dot(direction1, direction1);
        float b = Vector3.Dot(direction1, direction2);
        float e = Vector3.Dot(direction2, direction2);

        float d = a * e - b * b;
      
        Vector3 r = pointOnLine1 - pointOnLine2;
        float c = Vector3.Dot(direction1, r);
        float f = Vector3.Dot(direction2, r);

        float s = (b * f - c * e) / d;
        float t = (a * f - b * c) / d;

        point1 = pointOnLine1 + direction1 * s;
        point2 = pointOnLine2 + direction2 * t;        
    }

    public override Vector3 RayToPlanePoint(Ray ray, Vector3 planePoint, Vector3 planeNormal)
    {
        float rayDistance;

        if (Vector3.Dot(ray.direction, planeNormal) == 0f)
        {
            return Vector3.zero;
        }
        else
        {
            rayDistance = Vector3.Dot((planePoint - ray.origin), planeNormal) / Vector3.Dot(ray.direction, planeNormal);
        }
        Vector3 intersectionPoint = ray.origin + ray.direction * rayDistance;
        return intersectionPoint;
    }

    public override Vector3 RotateAround(Vector3 point, Vector3 origin, Quaternion rotation)
    {        
        Vector3 translatedPoint = point - origin;
        Vector3 rotatedPoint = rotation * translatedPoint;
        Vector3 finalPoint = rotatedPoint + origin;
        return finalPoint;
    }

    
}
