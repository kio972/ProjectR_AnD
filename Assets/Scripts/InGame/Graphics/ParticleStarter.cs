using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStarter : MonoBehaviour
{
    void Start()
    {
        ParticleSystem effect = GetComponentInChildren<ParticleSystem>();
        effect.Play();
    }
}
