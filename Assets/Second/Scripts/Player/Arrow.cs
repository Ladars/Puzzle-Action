using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private void OnEnable()
    {
        StartCoroutine(setArrowFalse());
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.CompareTag("Enemy"))
        {
            arrowAttack(collision, "Hit_D_Up");
            
        }
        GameObjectPoolSystem.Instance.RecyleGameObject(gameObject);
        rb.velocity = Vector3.zero;
    }
    void arrowAttack(Collision collision,string hitName)
    {
        Collider attackDetectionTargets = collision.gameObject.GetComponent<Collider>();
        if (attackDetectionTargets.transform.root.TryGetComponent(out IDamagar damagar))
        {
            damagar.TakeDamager(0,hitName );
            
        }
    }
    IEnumerator setArrowFalse()
    {
        yield return new WaitForSeconds(7);
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        
    }
}
