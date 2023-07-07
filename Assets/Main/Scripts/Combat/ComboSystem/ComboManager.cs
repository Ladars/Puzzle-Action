using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;

public class ComboManager : MonoBehaviour
{
    public WeaponManager currentWeapon;
    public float releaseTime;

    private Animator m_animator;
    StarterAssetsInputs _input;
    ThirdPersonController thirdPersonController;

    private float m_releaseTimer;
    private bool m_isOnNeceTime;
    private ComboConfig m_currentComboConfig;

    [SerializeField]private int m_lightAttackIdx =0;//轻攻击下标
    private int m_heavyAttackIdx=0;//重攻击下标

    public const float m_animationFadeTime = 0.1f;//动画过渡时间

    
    void Init()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        m_animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
        
    }
    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        if (Time.time >m_releaseTimer)
        {
            StopCombo();
        }
        HandleCombo();
    }
    private void HandleCombo()
    {
        if (m_isOnNeceTime)
        {
            return;
        }
        if (_input.GetLightAttackDown())
        {
            NormalAttack(true);
            thirdPersonController.canMovePlayer(m_currentComboConfig.m_releaseTime+0.3f);
        }
        if (_input.GetHeavyAttackDown())
        {
            NormalAttack(false);
            thirdPersonController.canMovePlayer(m_currentComboConfig.m_releaseTime+0.3f);
        }
    }
    IEnumerator PlayCombo(ComboConfig comboConfig)
    {
        m_isOnNeceTime = true;
        m_releaseTimer = Time.time + releaseTime;
        m_currentComboConfig = comboConfig;
        m_animator.CrossFade(comboConfig.m_animatorStateName,m_animationFadeTime);
        float timeOrigin = 0f;
        while (true)
        {
            if (timeOrigin>=comboConfig.m_releaseTime) break;
            timeOrigin += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        m_isOnNeceTime = false;
        yield break;
    }
    private void NormalAttack(bool isLight)
    {
        List<ComboConfig> configs = isLight ? currentWeapon.config.m_lightComboConfig : currentWeapon.config.m_heavyComboConfig;
        int comboIdx = isLight ? m_lightAttackIdx : m_heavyAttackIdx;
        StartCoroutine(PlayCombo(configs[comboIdx]));
        if (comboIdx >= configs.Count - 1) comboIdx = 0; else comboIdx++;
        if (isLight)
        {
            m_lightAttackIdx = comboIdx;
        }
        else
        {
            m_heavyAttackIdx = comboIdx;
        }
    }
    private void StopCombo()
    {
        m_lightAttackIdx = 0;
        m_heavyAttackIdx = 0;
    }
}
