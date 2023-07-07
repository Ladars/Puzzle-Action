using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEvent : MonoBehaviour
{
    public ComboManager comboManager;
   public void EnableDetection()
    {
        comboManager.currentWeapon.ToggleDetection(true);
    }
    public void DisableDetection()
    {
        comboManager.currentWeapon.ToggleDetection(false);
    }
}
