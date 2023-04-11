using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyBindController : MonoBehaviour
{
    // 1. ������ Ű ��ư�� ����
    // 2. Ű�Է� ���
    // 3. KeyCode.None�� �ƴ϶��, �Ҵ� �Ұ� Ű�� �ش��ϴ��� Ȯ��. => �Ҵ� �Ұ� Ű ����Ʈ �ۼ� �ʿ�
    // 4. �ش� Ű�ڵ尡 �̹� ��������� Ȯ��, ���� ������̶�� ��ü

    [SerializeField]
    private Dictionary<ControlKey, KeyBinder> keyDisplays = new Dictionary<ControlKey, KeyBinder>();

    private bool isUpdate = false;
    private ControlKey curKey = ControlKey.None;

    private void SetKeyDisplay(ControlKey targetKey, KeyCode value)
    {
        // �Լ� �ϼ� �ʿ�
        if(keyDisplays.ContainsKey(targetKey))
        {
            keyDisplays[targetKey].ChangeKeyDisplay(value);
        }
    }

    public void ChangeKey(ControlKey targetKey, KeyCode value)
    {
        // ���� �Ҵ��ϰ��� �ϴ� Ű�� valid�� Ű�� �ƴ϶��, �Լ��� ����
        if (!InputManager.Instance.IsValidKey(value))
            return;

        // ���� value ���� ����ϴ� ControlKey�� �ִ��� Ȯ��
        ControlKey key = InputManager.Instance.GetKey(value);
        KeyCode tempValue = KeyCode.None;

        // value ���� ����ϴ� ControlKey�� �ִ� ���, �ش� ���� �ӽ� ������ ����
        if (key != ControlKey.None)
            tempValue = InputManager.Instance.GetValue(targetKey);

        // targetKey�� value �Ҵ�
        InputManager.Instance.SetKey(targetKey, value);
        SetKeyDisplay(targetKey, value);

        // tempValue ���� ���� ControlKey�� �ִ� ���, �ش� ���� ����
        if (tempValue != KeyCode.None)
        {
            InputManager.Instance.SetKey(key, tempValue);
            SetKeyDisplay(key, tempValue);
        }
    }

    public KeyCode GetCurrentKeyDown()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                return keyCode;
            }
        }
        return KeyCode.None;
    }

    public void Init()
    {
        keyDisplays.Add(ControlKey.MoveFront, UtillHelper.Find<KeyBinder>(transform, "Move_Front", true));
        keyDisplays.Add(ControlKey.MoveBack, UtillHelper.Find<KeyBinder>(transform, "Move_Back", true));
        keyDisplays.Add(ControlKey.MoveLeft, UtillHelper.Find<KeyBinder>(transform, "Move_Left", true));
        keyDisplays.Add(ControlKey.MoveRight, UtillHelper.Find<KeyBinder>(transform, "Move_Right", true));
        keyDisplays.Add(ControlKey.Basic, UtillHelper.Find<KeyBinder>(transform, "Attack_Basic", true));
        keyDisplays.Add(ControlKey.Skill1, UtillHelper.Find<KeyBinder>(transform, "Attack_Skill1", true));
        keyDisplays.Add(ControlKey.Skill2, UtillHelper.Find<KeyBinder>(transform, "Attack_Skill2", true));
        keyDisplays.Add(ControlKey.Special, UtillHelper.Find<KeyBinder>(transform, "Attack_Special", true));
    }

    public void UpdateKeyInput(ControlKey curKey)
    {
        isUpdate = true;
        this.curKey = curKey;
    }

    private void UpdateCheck()
    {
        KeyCode inputKey = GetCurrentKeyDown();
        if(inputKey != KeyCode.None)
        {
            ChangeKey(curKey, inputKey);
            isUpdate = false;
            this.curKey = ControlKey.None;
        }
    }

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if (!isUpdate)
            return;

        UpdateCheck();
    }
}
