using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "ComboSystem/CreateNewWeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public List<ComboConfig> m_lightComboConfig = new List<ComboConfig>();
    public List<ComboConfig> m_heavyComboConfig = new List<ComboConfig>();

    public InputActionAsset inputActions;
}
