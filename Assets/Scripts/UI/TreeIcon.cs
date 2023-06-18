using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeIcon : MonoBehaviour
{
    public int skillId;
    public int replaceSkillId = -1;
    [SerializeField]
    private Transform deActive;
    [SerializeField]
    private Transform active;

    public void SetActive(bool value)
    {
        deActive.gameObject.SetActive(!value);
        active.gameObject.SetActive(value);
    }

}
