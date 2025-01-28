using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : SingletonMonobehavior<UI_HUD>
{
    [SerializeField] private TransitionAnimation _transitionAnimation;
    [SerializeField] private Image _healthIndicator;

    [SerializeField] private float _maxHealthIndicatorRatio = 0.75f;
    [SerializeField] private float _maxHealthIndicatorAlpha = 0.75f;

    #region Monobehaviour
    private void OnEnable()
    {
        EventHandler.PlayerGetDamage -= SyncPlayerHealth;
        EventHandler.PlayerGetDamage += SyncPlayerHealth;
    }

    private void OnDisable()
    {
        EventHandler.PlayerGetDamage -= SyncPlayerHealth;
    }

    private void Update()
    {

    }
    #endregion


    private void SyncPlayerHealth(float health, float maxHealth)
    {
        float ratio = health / maxHealth;
        float indicatorRatio = Mathf.Clamp01(((ratio) / (_maxHealthIndicatorRatio)));
        float alpha = (1 - indicatorRatio) * _maxHealthIndicatorAlpha;
        _healthIndicator.color = new Color(1, 1, 1, alpha);
    }

    #region Fade
    public void FadeIn(TransitionType type, Action func = null)
    {
        _transitionAnimation.DoFadeIn(type, func);
    }

    public void FadeOut(TransitionType type, Action func = null)
    { 
        _transitionAnimation.DoFadeOut(type, func);
    }

    public void FadeOutAndIn(TransitionType type, float wait, Action outFunc = null, Action inFunc = null)
    {
        _transitionAnimation.DoFadeOutAndIn(type, wait, outFunc, inFunc);
    }
    #endregion

}
