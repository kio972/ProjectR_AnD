using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySkillCall : MonoBehaviour
{
    public GameObject skillPrefab;

    private void OnParticleSystemStopped()
    {
        EffectManager.Instance.PlayEffect(skillPrefab, transform, Vector3.zero, 2.6f);
    }
}
