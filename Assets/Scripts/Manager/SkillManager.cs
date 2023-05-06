using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool SkillUpdater();

public class SkillManager : Singleton<SkillManager>
{
    public SkillMain basicSkill;
    public SkillMain dashSkill;
    public SkillMain specialSkill;

    public SkillMain curSkill;

    public List<SkillUpdater> activatedSkills = new List<SkillUpdater>();

    private bool isAttackReadyState = false;

    private Transform guideObject;

    private void Update()
    {
        List<SkillUpdater> needRemoveSkills = new List<SkillUpdater>();
        foreach(SkillUpdater skill in activatedSkills)
        {
            //스킬 업데이트 수행
            bool needRemove = skill();
            //스킬 쿨타임 완료시 제거리스트에 추가
            if (needRemove)
                needRemoveSkills.Add(skill);
        }
        //업데이트 제거 수행
        foreach(SkillUpdater skill in needRemoveSkills)
        {
            activatedSkills.Remove(skill);
        }
    }

    public void ActivateSkill(SkillUpdater updater)
    {
        activatedSkills.Add(updater);
    }

    private void DrawGuide(bool value, Controller player = null)
    {
        if (guideObject == null)
            return;

        if (value)
        {
            Vector3 mousePos = UtillHelper.GetMouseWorldPosition(player.transform.position);
            Vector3 dir = (mousePos - player.transform.position).normalized;
            guideObject.gameObject.SetActive(true);
            guideObject.position = player.transform.position;
            guideObject.rotation = Quaternion.LookRotation(dir);
        }
        else
            guideObject.gameObject.SetActive(false);
    }

    public IEnumerator SkillUpdate(Controller attacker, SkillMain skill, KeyCode skillKey)
    {
        if (isAttackReadyState)
            yield break;
        isAttackReadyState = true;
        curSkill = skill;
        while (true)
        {
            if (curSkill == null)
            {
                DrawGuide(false);
                isAttackReadyState = false;
                yield break;
            }

            DrawGuide(true, attacker);

            if (Input.GetKeyUp(skillKey))
                break;
            yield return null;
        }

        DrawGuide(false);
        isAttackReadyState = false;
        skill.SkillCheck(attacker);
        yield return null;
    }

    public void UseBasicSkill(Controller attacker)
    {
        StartCoroutine(SkillUpdate(attacker, basicSkill, InputManager.Instance.player_BasicAttackKey));
    }

    public void UseDashSkill(Controller attacker)
    {
        StartCoroutine(SkillUpdate(attacker, dashSkill, InputManager.Instance.player_Skill1Key));
    }

    public void UseSpecialSkill(Controller attacker)
    {
        StartCoroutine(SkillUpdate(attacker, specialSkill, InputManager.Instance.player_SpecialAttackKey));
    }

    public void Init()
    {
        GameObject basicSkillObject = new GameObject();
        basicSkillObject.name = "BasicSkill";
        basicSkillObject.transform.SetParent(transform);
        basicSkill = basicSkillObject.AddComponent<SkillBasic>();
        GameObject dashSkillObject = new GameObject();
        dashSkillObject.name = "DashSkill";
        dashSkillObject.transform.SetParent(transform);
        dashSkill = dashSkillObject.gameObject.AddComponent<SkillDash>();
        if (guideObject == null)
            guideObject = Instantiate(Resources.Load<Transform>("Prefab/UI/GuideObject"));
    }
}
