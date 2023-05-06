using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skill_Condition
{
    None,
    BackAttack,
    Damaged,
    GetClose,
    Debuff,
    Skill,
}

public enum ModifyTarget
{
    Player,
    Monster,
    Boss,
}

public enum ModifyStat
{
    None,
    Damage,
    DamageRate,
    DamageReduce,
    DashDist,
    DashCount,
    Shield,
    Hp,
    MaxHp,
    Speed,
}

public struct Skill_Passive
{
    public int id;
    public string skillName;
    public Skill_Condition skillCondition;
    public int targetID;
    public ModifyTarget modifyTarget;
    public ModifyStat modifyStat;
    public float value;
    public float coolTime;
    public string description;
    public string icon_path;

    public Skill_Passive(Dictionary<string, object> data)
    {
        id = Convert.ToInt32(data["ID"]);
        skillName = (string)data["SkillName"];
        Enum.TryParse((string)data["Condition"], out skillCondition);
        targetID = Convert.ToInt32(data["ConditionID"]);
        Enum.TryParse((string)data["ModifyTarget"], out modifyTarget);
        Enum.TryParse((string)data["ModifyStat"], out modifyStat);
        float.TryParse(data["Value"].ToString(), out value);
        float.TryParse(data["CoolTime"].ToString(), out coolTime);
        description = (string)data["Description"];
        icon_path = (string)data["Icon"];
    }
}

public class PassiveMain : MonoBehaviour
{

}
