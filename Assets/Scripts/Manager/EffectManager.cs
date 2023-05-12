using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    private List<GameObject> fxEffects = new List<GameObject>();

    public void PlayEffect(string effectName, Transform pos, Vector3 modifyPos = new Vector3())
    {
        string particleAddress = "Prefab/Effects/" + effectName;
        GameObject fxEffect = Resources.Load<GameObject>(particleAddress);
        PlayEffect(fxEffect, pos, modifyPos);
    }

    public void PlayEffect(GameObject particle, Transform pos, Vector3 modifyPos = new Vector3())
    {
        if (particle == null)
            return;

        string particleName = particle.name;
        foreach (GameObject fxEffect in fxEffects)
        {
            if (fxEffect.name.Contains(particleName))
            {
                ParticleSystem particleSystem = fxEffect.GetComponentInChildren<ParticleSystem>(true);
                if(!particleSystem.isPlaying)
                {
                    fxEffect.gameObject.SetActive(true);
                    Vector3 targetPos = pos.position;
                    if (modifyPos != new Vector3())
                    {
                        Vector3 modifiedOffset = pos.rotation * modifyPos;
                        targetPos += modifiedOffset;
                    }
                    fxEffect.transform.position = targetPos;
                    fxEffect.transform.rotation = pos.rotation;
                    particleSystem.Play();
                    return;
                }
            }
        }

        InstanceParticle(particle, pos, modifyPos);
    }

    private void InstanceParticle(GameObject particle, Transform pos, Vector3 modifyPos = new Vector3())
    {
        GameObject effect = Instantiate(particle, transform);
        fxEffects.Add(effect);
        Vector3 targetPos = pos.position;
        if (modifyPos != new Vector3())
        {
            Vector3 modifiedOffset = pos.rotation * modifyPos;
            targetPos += modifiedOffset;
        }
        effect.transform.position = targetPos;
        effect.transform.rotation = pos.rotation;
        ParticleSystem particleSystem = effect.GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
    }
}
