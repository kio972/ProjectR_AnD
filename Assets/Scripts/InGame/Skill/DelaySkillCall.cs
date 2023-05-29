using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySkillCall : MonoBehaviour
{
    public GameObject skillPrefab;
    public float scale = 1f;

    private void OnParticleSystemStopped()
    {
        EffectManager.Instance.PlayEffect(skillPrefab, transform, Vector3.zero, scale);
    }
}
