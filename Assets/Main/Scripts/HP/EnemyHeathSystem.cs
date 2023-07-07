using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHeathSystem : MonoBehaviour, IHealth, IAgent
{
    [SerializeField] float health = 100;

    Animator animator;
    public GameObject hitVFX;
    public GameObject VfxPosition;
    public GameObject ragdoll;
    public Action onHurt;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void HitVFX(Vector3 position)
    {
        GameObject hit = Instantiate(hitVFX, position, Quaternion.identity);
        Destroy(hit, 3);
    }

    private void Update()
    {
        if (health<=0)
        {
            Destroy(gameObject);
            Instantiate(ragdoll, transform.position, transform.rotation);
        }
    }

    public void GetDamage(float damage, Vector3 pos)
    {
        onHurt();
        animator.SetTrigger("GetDamage");
        if (health > 0)
        {
            health-=damage;
        }
        else
        {
            Destroy(gameObject);
            Instantiate(ragdoll, transform.position, transform.rotation);
        }
        HitVFX(VfxPosition.transform.position);

    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
