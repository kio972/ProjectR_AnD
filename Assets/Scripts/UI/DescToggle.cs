using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescToggle : MonoBehaviour
{
    public Transform desc;
    [SerializeField]
    private Button descButton;

    public bool value = true;
    
    public bool isToggle = false;
    public KeyCode toggleKey = KeyCode.None;
    public ToggleType toggleType = ToggleType.Cut;
    public ToggleDirection toggleDirection = ToggleDirection.Down;
    public float toggleTime = 0.1f;
    public Image maskImage;
    private Coroutine toggleCorutine = null;

    public enum ToggleType
    {
        Cut,
        Slide,
        Drop,
        Fade,
    }

    public enum ToggleDirection
    {
        Down, Up, Left, Right,
    }

    private void DescButton()
    {
        desc.gameObject.SetActive(value);
    }

    private IEnumerator I_FadeUI(bool value)
    {
        //데스크 알파값 조정
        yield return null;
    }

    private Vector2 GetStartPosition()
    {
        RectTransform descRectTransform = desc as RectTransform;
        switch (toggleDirection)
        {
            case ToggleDirection.Left:
                return new Vector2(-Screen.width, descRectTransform.anchoredPosition.y);
            case ToggleDirection.Right:
                return new Vector2(Screen.width, descRectTransform.anchoredPosition.y);
            case ToggleDirection.Up:
                return new Vector2(descRectTransform.anchoredPosition.x, Screen.height);
            case ToggleDirection.Down:
                return new Vector2(descRectTransform.anchoredPosition.x, -Screen.height);
            default:
                return descRectTransform.anchoredPosition;
        }
    }


    private IEnumerator I_SlideUI(bool value)
    {
        //desc의 transform을 screen resolution에따라 시작위치에서 현재위치로 lerpTime동안 이동
        desc.gameObject.SetActive(true);
        RectTransform descRectTransform = desc as RectTransform;
        Vector2 origin = descRectTransform.anchoredPosition;
        Vector2 startPos = GetStartPosition();
        Vector2 endPos = descRectTransform.anchoredPosition;
        if (!value)
        {
            Vector2 temp = startPos;
            startPos = endPos;
            endPos = temp;
        }

        float elapsedTime = 0f;
        while (elapsedTime < toggleTime)
        {
            elapsedTime += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(elapsedTime / toggleTime);
            descRectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            yield return null;
        }

        desc.gameObject.SetActive(value);
        descRectTransform.anchoredPosition = origin;
        yield return null;
    }

    private void SetMaskDirection()
    {
        if (maskImage == null)
            return;
        switch(toggleDirection)
        {
            case ToggleDirection.Down:
                maskImage.fillMethod = Image.FillMethod.Vertical;
                maskImage.fillOrigin = (int)Image.OriginVertical.Top;
                break;
            case ToggleDirection.Up:
                maskImage.fillMethod = Image.FillMethod.Vertical;
                maskImage.fillOrigin = (int)Image.OriginVertical.Bottom;
                break;
            case ToggleDirection.Left:
                maskImage.fillMethod = Image.FillMethod.Horizontal;
                maskImage.fillOrigin = (int)Image.OriginHorizontal.Right;
                break;
            case ToggleDirection.Right:
                maskImage.fillMethod = Image.FillMethod.Horizontal;
                maskImage.fillOrigin = (int)Image.OriginHorizontal.Left;
                break;
        }
    }

    private IEnumerator I_DropUI(bool value)
    {
        //maskImage의 fill amount를 0-1로 lerptime동안 변경
        if (maskImage != null)
        {
            desc.gameObject.SetActive(true);
            SetMaskDirection();
            float startValue = maskImage.fillAmount;
            float targetValue = Convert.ToSingle(value);
            float elapsedTime = 0f;
            float currentValue = startValue;
            while (elapsedTime < toggleTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsedTime / toggleTime);
                currentValue = Mathf.Lerp(startValue, targetValue, t);
                maskImage.fillAmount = currentValue;
                yield return null;
            }
            maskImage.fillAmount = targetValue;
            desc.gameObject.SetActive(value);
            yield return null;
        }
        yield return null;
    }

    private void ToggleDesc()
    {
        if(Input.GetKeyDown(toggleKey))
        {
            bool value = desc.gameObject.activeSelf;
            switch (toggleType)
            {
                case ToggleType.Cut:
                    desc.gameObject.SetActive(!value);
                    break;
                case ToggleType.Slide:
                    CallSlide(!value);
                    break;
                case ToggleType.Drop:
                    CallDrop(!value);
                    break;
                case ToggleType.Fade:
                    if(toggleCorutine != null)
                        StopCoroutine(toggleCorutine);
                    toggleCorutine = StartCoroutine(I_FadeUI(!value));
                    break;
            }
        }
    }

    public void CallSlide(bool value)
    {
        if (toggleCorutine != null)
            StopCoroutine(toggleCorutine);
        toggleCorutine = StartCoroutine(I_SlideUI(value));
    }

    public void CallDrop(bool value)
    {
        if (toggleCorutine != null)
            StopCoroutine(toggleCorutine);
        toggleCorutine = StartCoroutine(I_DropUI(value));
    }

    void Start()
    {
        if (descButton == null)
            descButton = GetComponent<Button>();

        if(descButton != null)
            descButton.onClick.AddListener(DescButton);

        
    }

    private void Update()
    {
        if (desc == null)
            return;

        if (isToggle)
            ToggleDesc();
    }
}
