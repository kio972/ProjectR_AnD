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
    private Dictionary<ControlKey, Transform> keyDisplays;

    private void SetKeyDisplay(ControlKey targetKey, KeyCode value)
    {
        // �Լ� �ϼ� �ʿ�
        if(keyDisplays.ContainsKey(targetKey))
        {
            switch(value)
            {
                case KeyCode.Mouse0:
                    break;
                case KeyCode.Mouse1:
                    break;
                case KeyCode.Mouse2:
                    break;
                default:
                    {
                        string targetString = value.ToString();
                        targetString = targetString.Replace("KeyCode.", "");
                        
                    }
                    break;
            }
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

}
