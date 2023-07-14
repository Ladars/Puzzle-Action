using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
     bool hasDealDamage;

    [SerializeField] float weaponLenth;
    [SerializeField] float weaponDamage;

    private void Start()
    {
        canDealDamage = false;
        hasDealDamage = false;
    }
    private void Update()
    {
        if (canDealDamage &&!hasDealDamage)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,-transform.up,out hit, weaponLenth))
            {              
                if (hit.transform.TryGetComponent(out HealthSystem health))
                {
                    Debug.Log("true");
                    health.TakeDamage(weaponDamage);
                    hasDealDamage = true;
                }

            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealDamage = false;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,transform.position-transform.up*weaponLenth);
    }

}
