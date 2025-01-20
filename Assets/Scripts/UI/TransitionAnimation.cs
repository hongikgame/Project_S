using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TransitionAnimation : MonoBehaviour
{
    [SerializeField] private float _limiter;
    [SerializeField] private float _init;
    [SerializeField] private float _trainsitionTimer;

    [SerializeField] private RectTransform[] _stageTransitionArray;
    [SerializeField] private RectTransform[] _sceneTransitionArray;

    public Action FinishFade;

    private void Awake()
    {
        //Reset();
        DoFadeOut(TransitionType.Stage);
    }

    public void DoFadeIn(TransitionType type)
    {
        Reset();

        if (type == TransitionType.Stage)
        {
            _stageTransitionArray[0].DOAnchorPosY(_limiter, _trainsitionTimer).SetEase(Ease.InOutSine);
            _stageTransitionArray[1].DOAnchorPosY(-_limiter, _trainsitionTimer).SetEase(Ease.InOutSine);
        }
    }

    public void DoFadeOut(TransitionType type)
    {
        if (type == TransitionType.Stage)
        {
            _stageTransitionArray[0].DOAnchorPosY(_init, _trainsitionTimer).SetEase(Ease.InOutSine);
            _stageTransitionArray[1].DOAnchorPosY(-_init, _trainsitionTimer).SetEase(Ease.InOutSine);
        }
    }

    private void Reset()
    {
        _stageTransitionArray[0].DOAnchorPosY(-_init, 0).SetEase(Ease.InOutSine);
        _stageTransitionArray[1].DOAnchorPosY(_init, 0).SetEase(Ease.InOutSine);

        _sceneTransitionArray[0].DOAnchorPosY(-_init, 0).SetEase(Ease.InOutSine);
        _sceneTransitionArray[1].DOAnchorPosY(_init, 0).SetEase(Ease.InOutSine);
    }


}

public enum TransitionType
{
    Stage,
    Scene,
}

