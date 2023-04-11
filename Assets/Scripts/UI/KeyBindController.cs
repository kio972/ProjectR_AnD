using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyBindController : MonoBehaviour
{
    // 1. 변경할 키 버튼을 누름
    // 2. 키입력 대기
    // 3. KeyCode.None이 아니라면, 할당 불가 키에 해당하는지 확인. => 할당 불가 키 리스트 작성 필요
    // 4. 해당 키코드가 이미 사용중인지 확인, 만약 사용중이라면 교체

    [SerializeField]
    private Dictionary<ControlKey, KeyBinder> keyDisplays = new Dictionary<ControlKey, KeyBinder>();

    private bool isUpdate = false;
    private ControlKey curKey = ControlKey.None;

    private void SetKeyDisplay(ControlKey targetKey, KeyCode value)
    {
        // 함수 완성 필요
        if(keyDisplays.ContainsKey(targetKey))
        {
            keyDisplays[targetKey].ChangeKeyDisplay(value);
        }
    }

    public void ChangeKey(ControlKey targetKey, KeyCode value)
    {
        // 현재 할당하고자 하는 키가 valid한 키가 아니라면, 함수를 종료
        if (!InputManager.Instance.IsValidKey(value))
            return;

        // 현재 value 값을 사용하는 ControlKey가 있는지 확인
        ControlKey key = InputManager.Instance.GetKey(value);
        KeyCode tempValue = KeyCode.None;

        // value 값을 사용하는 ControlKey가 있는 경우, 해당 값을 임시 변수에 저장
        if (key != ControlKey.None)
            tempValue = InputManager.Instance.GetValue(targetKey);

        // targetKey에 value 할당
        InputManager.Instance.SetKey(targetKey, value);
        SetKeyDisplay(targetKey, value);

        // tempValue 값을 가진 ControlKey가 있는 경우, 해당 값을 변경
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
