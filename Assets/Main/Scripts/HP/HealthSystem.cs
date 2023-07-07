using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour,IHealth
{
    [SerializeField] float health = 100;

    Animator animator;
    public GameObject hitVFX;
    public GameObject VfxPosition;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Damage");
        HitVFX(VfxPosition.transform.position);
        if (health<=0)
        {
            Debug.Log("Player die");
        }
        CameraShake.Instance.ShakeCamera(0.3f, 0.2f);
    }
    public void HitVFX(Vector3 position)
    {
        GameObject hit = Instantiate(hitVFX,position,Quaternion.identity);
        Destroy(hit, 3);
    }
}
