using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlKey
{
    None,
    Move,
    Aim,
    Basic,
    Skill1,
    Skill2,
    Special,
    MoveFront,
    MoveBack,
    MoveLeft,
    MoveRight,
}

public class InputManager : Singleton<InputManager>
{
    public KeyCode[] nextMessageKeys = { KeyCode.Mouse0 };
    //public KeyCode player_MoveKey = KeyCode.Mouse1;
    //public KeyCode player_AimKey = KeyCode.Mouse0;
    public KeyCode player_BasicAttackKey = KeyCode.Mouse0;
    public KeyCode player_Skill1Key = KeyCode.Mouse1;
    public KeyCode player_Skill2Key = KeyCode.E;
    public KeyCode player_SpecialAttackKey = KeyCode.R;

    public KeyCode player_MoveFront = KeyCode.W;
    public KeyCode player_MoveBack = KeyCode.S;
    public KeyCode player_MoveLeft = KeyCode.A;
    public KeyCode player_MoveRight = KeyCode.D;

    private Dictionary<ControlKey, KeyCode> controlKeys = new Dictionary<ControlKey, KeyCode>();

    private List<KeyCode> invalidKeys = new List<KeyCode>()
    {
        KeyCode.Escape,
    };

    public bool IsValidKey(KeyCode value)
    {
        foreach(KeyCode key in invalidKeys)
        {
            if (key == value)
                return false;
        }

        return true;
    }

    public KeyCode GetValue(ControlKey key)
    {
        if (controlKeys.ContainsKey(key))
            return controlKeys[key];
        else
            return KeyCode.None;
    }

    public ControlKey GetKey(KeyCode value)
    {
        foreach (var kvp in controlKeys)
        {
            if (kvp.Value == value)
            {
                return kvp.Key;
            }
        }
        // �ش� value�� ���� ControlKey Ű�� ���� ��� �⺻���� ControlKey.None ��ȯ
        return ControlKey.None;
    }

    public void SetKey(ControlKey targetKey, KeyCode value)
    {
        if (controlKeys.ContainsKey(targetKey))
        {
            controlKeys[targetKey] = value;
        }
        else
        {
            controlKeys.Add(targetKey, value);
        }
    }

    public void Init()
    {
        //���̺굥���Ϳ��� ������ �ҷ����� �Լ��� �����ʿ�
        controlKeys.Clear();
        controlKeys.Add(ControlKey.None, KeyCode.None);
        controlKeys.Add(ControlKey.MoveFront, KeyCode.W);
        controlKeys.Add(ControlKey.MoveBack, KeyCode.S);
        controlKeys.Add(ControlKey.MoveLeft, KeyCode.A);
        controlKeys.Add(ControlKey.MoveRight, KeyCode.D);
        controlKeys.Add(ControlKey.Basic, KeyCode.Mouse0);
        controlKeys.Add(ControlKey.Skill1, KeyCode.Mouse1);
        //controlKeys.Add(ControlKey.Skill2, KeyCode.E);
        controlKeys.Add(ControlKey.Special, KeyCode.R);
    }

    public void KeyBoardMove(Controller player)
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(player_MoveFront))
            direction += Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * Vector3.forward;
        if (Input.GetKey(player_MoveBack))
            direction += Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * Vector3.back;
        if (Input.GetKey(player_MoveLeft))
            direction += Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * Vector3.left;
        if (Input.GetKey(player_MoveRight))
            direction += Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * Vector3.right;

        direction = direction.normalized;

        player.Move(direction);
    }

    public void AttackCheck(Controller player)
    {
        if (Input.GetKeyDown(player_BasicAttackKey))
            SkillManager.Instance.UseBasicSkill(player);
        if (Input.GetKeyDown(player_Skill1Key))
            SkillManager.Instance.UseDashSkill(player);
        if (Input.GetKeyDown(player_SpecialAttackKey))
            SkillManager.Instance.UseSpecialSkill(player);
    }

    public void InputCheck(Controller player)
    {
        AttackCheck(player);
        KeyBoardMove(player);
    }
}
