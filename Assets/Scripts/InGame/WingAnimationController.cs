using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingAnimationController : MonoBehaviour
{
    private Animator wingAnimator;
    private Coroutine blendOpenCoroutine = null;

    public void SetFlap(bool value)
    {
        wingAnimator.SetBool("isFlapping", value);
        wingAnimator.SetBool("isFlapping_L", value);
    }

    private IEnumerator ISetBlendOpen(string targetPram, float value, float lerpTime)
    {
        float elapsedTime = 0f;
        float startValue = wingAnimator.GetFloat(targetPram);
        while(elapsedTime < lerpTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpTime);
            float blendValue = Mathf.Lerp(startValue, value, t);
            wingAnimator.SetFloat(targetPram, blendValue);
            wingAnimator.SetFloat(targetPram + "_L", blendValue);
            yield return null;
        }

        wingAnimator.SetFloat(targetPram, value);
        wingAnimator.SetFloat(targetPram + "_L", value);

        yield return null;
    }

    public void ChangeBlend(string targetPram, float value, float lerpTime)
    {
        if (wingAnimator == null)
            return;

        if (blendOpenCoroutine != null)
            StopCoroutine(blendOpenCoroutine);
        blendOpenCoroutine = StartCoroutine(ISetBlendOpen(targetPram, value, lerpTime));
    }

    private void Start()
    {
        wingAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
