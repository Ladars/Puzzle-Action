using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
  
    private void OnEnable()
    {
        StartCoroutine(enumerator());
    }
    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.5f);
        transform.gameObject.SetActive(false);
    }
}
