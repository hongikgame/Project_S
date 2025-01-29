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
    [SerializeField] private RectTransform[] _dieTransitionArray;

    public Action FinishFade;

    private void Awake()
    {
        Reset();
    }

    public void DoFadeOut(TransitionType type, Action func = null)
    {
        Reset();

        if (type == TransitionType.Stage)
        {
            _stageTransitionArray[0].gameObject.SetActive(true);
            _stageTransitionArray[1].gameObject.SetActive(true);

            _stageTransitionArray[0].anchoredPosition = new Vector2(0, _init);
            _stageTransitionArray[1].anchoredPosition = new Vector2(0, -_init);

            _stageTransitionArray[0].DOAnchorPosY(_limiter, _trainsitionTimer).SetEase(Ease.InOutSine);
            _stageTransitionArray[1].DOAnchorPosY(-_limiter, _trainsitionTimer).SetEase(Ease.InOutSine).OnComplete(() => func?.Invoke());
        }
        else if (type == TransitionType.Die)
        {
            _dieTransitionArray[0].gameObject.SetActive(true);
            _dieTransitionArray[1].gameObject.SetActive(true);

            _dieTransitionArray[0].anchoredPosition = new Vector2(0, _init);
            _dieTransitionArray[1].anchoredPosition = new Vector2(0, -_init);

            _dieTransitionArray[0].DOAnchorPosY(_limiter, _trainsitionTimer).SetEase(Ease.InOutSine);
            _dieTransitionArray[1].DOAnchorPosY(-_limiter, _trainsitionTimer).SetEase(Ease.InOutSine).OnComplete(() => func?.Invoke());
        }
    }

    public void DoFadeIn(TransitionType type, Action func = null)
    {
        Reset();

        if (type == TransitionType.Stage)
        {
            _stageTransitionArray[0].gameObject.SetActive(true);
            _stageTransitionArray[1].gameObject.SetActive(true);

            _stageTransitionArray[0].anchoredPosition = new Vector2(0, _limiter);
            _stageTransitionArray[1].anchoredPosition = new Vector2(0, -_limiter);

            _stageTransitionArray[0].DOAnchorPosY(_init, _trainsitionTimer).SetEase(Ease.InOutSine);
            _stageTransitionArray[1].DOAnchorPosY(-_init, _trainsitionTimer).SetEase(Ease.InOutSine).OnComplete(() => func?.Invoke());
        }
        else if (type == TransitionType.Die)
        {
            _dieTransitionArray[0].gameObject.SetActive(true);
            _dieTransitionArray[1].gameObject.SetActive(true);

            _dieTransitionArray[0].anchoredPosition = new Vector2(0, _limiter);
            _dieTransitionArray[1].anchoredPosition = new Vector2(0, -_limiter);

            _dieTransitionArray[0].DOAnchorPosY(_init, _trainsitionTimer).SetEase(Ease.InOutSine);
            _dieTransitionArray[1].DOAnchorPosY(-_init, _trainsitionTimer).SetEase(Ease.InOutSine).OnComplete(() => func?.Invoke());
        }
    }

    public void DoFadeOutAndIn(TransitionType type, float wait, Action outFunc, Action inFunc)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutAndInCoroutine(type, wait, outFunc, inFunc));
    }

    private void Reset()
    {
        _stageTransitionArray[0].gameObject.SetActive(false);
        _stageTransitionArray[1].gameObject.SetActive(false);
        _dieTransitionArray[0].gameObject.SetActive(false);
        _dieTransitionArray[1].gameObject.SetActive(false);
    }

    private IEnumerator FadeOutAndInCoroutine(TransitionType type, float wait, Action outFunc, Action inFunc)
    {
        DoFadeOut(type, outFunc);

        yield return new WaitForSeconds(wait + _trainsitionTimer);

        DoFadeIn(type, inFunc);
    }
}

public enum TransitionType
{
    Stage,
    Die,
}

