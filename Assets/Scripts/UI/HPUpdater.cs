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
        // ���� ü�¹ٴ� �ٷ� ������Ʈ
        float nextAmount = controller.hp / controller.maxHp;
        hp_Bar.fillAmount = nextAmount;
        // delayTime���� ���
        float elapsedTime = 0f;
        while(elapsedTime < delayTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //lerpTime�� ���� delayed_Bar.fillAmount�� ���� hp_Bar.fillAmount�� ����
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

        // controller�� ��ġ�� ���� ��ǥ�迡�� ��ũ�� ��ǥ��� ��ȯ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(controller.transform.position);

        // ��ũ�� ��ǥ�迡�� RectTransform ��ǥ��� ��ȯ�Ͽ� ��ġ �̵�
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
