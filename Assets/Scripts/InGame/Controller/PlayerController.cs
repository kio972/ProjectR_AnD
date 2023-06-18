using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : Controller
{
    [SerializeField]
    private List<Renderer> bodyRenderer;
    [SerializeField]
    private List<Renderer> wingsRenderer;

    private Coroutine damageCoroutine;
    private List<Coroutine> coroutines;
    [SerializeField]
    private CinemachineImpulseSource impulseSource;

    private void SwapMaterial(Renderer render, int targetMaterialIndex)
    {
        Material[] temp = render.materials;
        temp[0] = temp[targetMaterialIndex];
        temp[targetMaterialIndex] = render.material;
        render.materials = temp;
    }

    private void RemoveMaterial(Renderer render)
    {
        List<Material> temp = new List<Material>(render.materials);
        Material tempMaterial = temp[0];
        temp.Remove(tempMaterial);
        render.materials = temp.ToArray();
    }

    public override void Dead()
    {
        animator.SetBool("Dead", true);
        isDead = true;
        agent.enabled = false;
        Collider collider = GetComponentInChildren<Collider>();
        if (collider != null)
            collider.enabled = false;
        WingAnimationController wing = GetComponentInChildren<WingAnimationController>();
        if (wing != null)
            wing.SetBool("isFlapping", false);

        StopDamagedEffect();

        foreach (Renderer renderer in bodyRenderer)
            RemoveMaterial(renderer);
        foreach (Renderer renderer in wingsRenderer)
            RemoveMaterial(renderer);

        foreach (Renderer renderer in bodyRenderer)
            coroutines.Add(StartCoroutine(UtillHelper.IChangeFloat(renderer.material, 1, "_Dissolve", 4f)));
        foreach (Renderer renderer in wingsRenderer)
            coroutines.Add(StartCoroutine(UtillHelper.IChangeFloat(renderer.material, 1, "_Dissolve", 4f)));
    }

    private IEnumerator IDamagedEffect()
    {
        float changeTime = 0.1f;
        coroutines = new List<Coroutine>();
        foreach (Renderer renderer in bodyRenderer)
            coroutines.Add(StartCoroutine(UtillHelper.IChangeColor(renderer.material, Color.red, "_GlowColor", changeTime)));
        foreach(Renderer renderer in wingsRenderer)
            coroutines.Add(StartCoroutine(UtillHelper.IChangeColor(renderer.material, Color.red, "_GlowColor", changeTime)));

        float elapsedTime = 0f;
        while(elapsedTime < changeTime + 0.1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        coroutines = new List<Coroutine>();
        foreach (Renderer renderer in bodyRenderer)
            coroutines.Add(StartCoroutine(UtillHelper.IChangeColor(renderer.material, Color.black, "_GlowColor", changeTime)));
        foreach (Renderer renderer in wingsRenderer)
            coroutines.Add(StartCoroutine(UtillHelper.IChangeColor(renderer.material, Color.white, "_GlowColor", changeTime)));
    }

    private void StopDamagedEffect()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            foreach (Coroutine co in coroutines)
                StopCoroutine(co);
        }
    }

    private void DamagedEffect()
    {
        StopDamagedEffect();
        damageCoroutine = StartCoroutine(IDamagedEffect());
    }

    public override float TakeDamage(float damage)
    {
        if (isDead)
            return 0;

        float finalDamage = Mathf.Round(damage * (1 - damageReduce));
        if (finalDamage <= 0)
            return 0;

        DamagedEffect();
        impulseSource.GenerateImpulse();
        return base.TakeDamage(damage);
    }
}
