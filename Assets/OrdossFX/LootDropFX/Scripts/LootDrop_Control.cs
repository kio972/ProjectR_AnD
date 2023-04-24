using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop_Control : MonoBehaviour
{
    public GameObject LootLight;
    public AudioSource DeathAudio;
    public AudioSource PickUpAudio;
    public AudioSource LifeAudio;
    public List<ParticleSystem> LoopOff;
    public ParticleSystem MeshSystem;
    

    void OnEnable()
    {   
        var lifetime = MeshSystem.main.startLifetime.constant;
        Invoke("OnParticlesDeath", lifetime);
        GetComponent<SphereCollider>().enabled = true;
    }

    private void OnDisable()
    {
        CancelInvoke("OnParticlesDeath");
    }


    void OnParticlesDeath()
    {
        
        GetComponent<SphereCollider>().enabled = false;
        LifeAudio.GetComponent<LootDrop_AudioCurves>().FadeOut = true;
        LootLight.GetComponent<LootDrop_Light>().FadeOut = true;
        foreach (var partsys in LoopOff)
        {
            var main = partsys.main;
            partsys.Stop();
        }
        DeathAudio.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        CancelInvoke("OnParticlesDeath");
        //Debug.Log("Collide work");
        PickUpAudio.Play();
        foreach (var partsys in LoopOff)
        {
            var main = partsys.main;
            partsys.Stop();
        }
        GetComponent<SphereCollider>().enabled = false;
        LifeAudio.GetComponent<LootDrop_AudioCurves>().FadeOut = true;
        LootLight.GetComponent<LootDrop_Light>().FadeOut = true;
    }

}
