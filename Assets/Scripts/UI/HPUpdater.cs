using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UnitType
{
    Player,
    Monster,
    Boss,
}

public class HPUpdater : MonoBehaviour
{
    private Controller controller;
    private Image hp_Bar;
    private Image delayed_Bar;
    private TextMeshProUGUI monsterName;

    public float hp_Rate;

    public float delayTime = 1f;
    public float lerpTime = 0.2f;

    private Coroutine curCoroutine = null;

    private void HPBarEnd()
    {
        gameObject.SetActive(false);
    }

    public void Init(Controller controller)
    {
        this.controller = controller;
        hp_Bar = UtillHelper.Find<Image>(transform, "FillAmount");
        delayed_Bar = UtillHelper.Find<Image>(transform, "DelayZone");
        monsterName = UtillHelper.Find<TextMeshProUGUI>(transform, "MonsterName");
        if(controller != null)
        {
            hp_Bar.fillAmount = controller.hp / controller.maxHp;
            delayed_Bar.fillAmount = hp_Bar.fillAmount;
            if (monsterName != null)
                monsterName.text = controller._name;
        }
    }

    private IEnumerator IUpdateHP()
    {
        // 메인 체력바는 바로 업데이트
        float nextAmount = controller.hp / controller.maxHp;
        hp_Bar.fillAmount = nextAmount;
        // delayTime동안 대기
        float elapsedTime = 0f;
        while(elapsedTime < delayTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //lerpTime에 걸쳐 delayed_Bar.fillAmount의 값을 hp_Bar.fillAmount로 변경
        float startAmount = delayed_Bar.fillAmount;
        float endAmount = nextAmount;
        elapsedTime = 0f;
        while(elapsedTime < lerpTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpTime);
            delayed_Bar.fillAmount = Mathf.Lerp(startAmount, endAmount, t);
            yield return null;
        }
        delayed_Bar.fillAmount = endAmount;
        if (endAmount <= 0)
            HPBarEnd();
        yield return null;
    }

    private void UpdatePosition()
    {
        if (controller == null)
            return;

        if (controller.unitType != UnitType.Monster)
            return;

        // controller의 위치를 월드 좌표계에서 스크린 좌표계로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(controller.transform.position);

        // 스크린 좌표계에서 RectTransform 좌표계로 변환하여 위치 이동
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            HPBarController.Instance.CameraCanvas.transform as RectTransform, screenPos, Camera.main, out anchoredPos);
        anchoredPos.y += 150;
        (this.transform as RectTransform).anchoredPosition = anchoredPos;
    }

    private void UpdateHp()
    {
        if (curCoroutine != null)
            StopCoroutine(curCoroutine);
        curCoroutine = StartCoroutine(IUpdateHP());
    }

    private bool IsNeedUpdate()
    {
        if (controller == null)
            return false;

        float prevHp = hp_Bar.fillAmount;
        float curHp = controller.hp / controller.maxHp;
        if (Mathf.Abs(prevHp - curHp) > 0.01f)
            return true;

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
            return;

        if (IsNeedUpdate())
            UpdateHp();
        UpdatePosition();
    }
}
