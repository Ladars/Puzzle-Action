using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public Material[] materials;
    [SerializeField] float elapsedTime = 0f;
    [SerializeField] float fadeDuration;
    float m_DissolveAmount;
    int currentIndex = 0;
    int previousIndex;
    bool isSwitchingWeapon = false;


    private void Update()
    {
        if (isSwitchingWeapon)
        {
            // If switching weapon, interpolate the dissolveAmount from 1 to 0
            if (elapsedTime<fadeDuration)
            {
                elapsedTime += Time.deltaTime;
            }
            m_DissolveAmount = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            // Update the dissolve parameter for the current material
            materials[currentIndex].SetFloat("_DissolveAmount", m_DissolveAmount);

            // Once the dissolveAmount reaches 0, the weapon is fully materialized
            if (m_DissolveAmount <= 0.0f)
            {
                isSwitchingWeapon = false;
                m_DissolveAmount = 0.0f;
            }
        }
    }

    public void SwitchWeapon(int newIndex)
    {
        if (newIndex != currentIndex)
        {
            elapsedTime = 0;
            // Set the current weapon's dissolveAmount to 1 to start dissolving it
            materials[currentIndex].SetFloat("_DissolveAmount", 1.0f);

            // Update currentIndex to the new weapon
            currentIndex = newIndex;

            // Start switching weapon
            isSwitchingWeapon = true;
        }
    }
}
