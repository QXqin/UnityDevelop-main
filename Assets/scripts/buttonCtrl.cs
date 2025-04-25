using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class buttonCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _sizeInit = 1f;
    private float _sizeOnButtonDown = 0.95f;
    private float _animationSpeedOnButtonUp = 2f;
    private bool _isPressed = false;
    private Coroutine _coroutine = null;

    [SerializeField] private float _delayTime = 0f;
    [SerializeField] private bool _isOneShot = false;

    [Header("可选：淡出效果")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * _sizeInit * _sizeOnButtonDown;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(OnButtonUpCoroutine());
    }

    protected virtual void OnButtonClickEvent()
    {
        // 你自己的按钮点击逻辑可在子类重写
    }

    protected virtual void OnEnable()
    {
        _sizeInit = transform.localScale.x;
    }

    protected IEnumerator OnButtonUpCoroutine()
    {
        while (transform.localScale.x < _sizeInit)
        {
            transform.localScale += Time.deltaTime * _animationSpeedOnButtonUp * Vector3.one * _sizeInit;
            yield return null;
        }
        yield return new WaitForSeconds(_delayTime);

        // ⚡ 开始淡出
        if (fadeImage != null)
        {
            fadeImage.raycastTarget = true;
            yield return fadeImage.DOFade(1f, fadeDuration).WaitForCompletion();
        }

        if (_isOneShot)
        {
            if (!_isPressed)
            {
                _isPressed = true;
                OnButtonClickEvent();
            }
        }
        else
        {
            OnButtonClickEvent();
        }
    }
}
