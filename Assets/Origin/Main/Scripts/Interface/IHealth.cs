using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    public void TakeDamage(float damage);
    public void HitVFX(Vector3 position);
}
