using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainVisibilityController : MonoBehaviour
{
    public Transform player;
    private Renderer currentRenderer = null;
    private Material originalMaterial = null;
    //public Material transparantMaterial;
    public float transparentValue = 0.3f;
    public float materialChangeTime = 0.1f;
    private List<KeyValuePair<Renderer, Coroutine>> coroutines = new List<KeyValuePair<Renderer, Coroutine>>();

    IEnumerator SetMaterialColor(Renderer renderer, Color targetColor, float changeTime = 0.1f)
    {
        Material material = renderer.GetComponent<Renderer>().material;
        Color startColor = material.color;
        float elapsedTime = 0f;

        while (elapsedTime < changeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / changeTime);
            material.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
        material.color = targetColor;

        for (int i = 0; i < coroutines.Count; i++)
        {
            if (coroutines[i].Key == renderer)
            {
                coroutines.RemoveAt(i);
                break;
            }
        }
    }

    private void ResetMaterial()
    {
        if (currentRenderer == null)
            return;

        CorutineRunningCheck(currentRenderer);

        Color color = new Color(currentRenderer.material.color.r, currentRenderer.material.color.g, currentRenderer.material.color.b, 1);
        Coroutine coroutine = StartCoroutine(SetMaterialColor(currentRenderer, color, materialChangeTime));
        coroutines.Add(new KeyValuePair<Renderer, Coroutine>(currentRenderer, coroutine));
        currentRenderer = null;
    }

    private void SetMaterial(Renderer renderer)
    {
        if (renderer == null)
            return;

        // Check if the renderer already has a coroutine running
        CorutineRunningCheck(renderer);

        Color color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, transparentValue);
        Coroutine coroutine = StartCoroutine(SetMaterialColor(renderer, color, materialChangeTime));
        coroutines.Add(new KeyValuePair<Renderer, Coroutine>(renderer, coroutine));
    }

    private void CorutineRunningCheck(Renderer renderer)
    {
        for (int i = 0; i < coroutines.Count; i++)
        {
            if (coroutines[i].Key == renderer)
            {
                StopCoroutine(coroutines[i].Value);
                coroutines.RemoveAt(i);
                break;
            }
        }
    }

    private void CollisionCheck()
    {
        if (player == null)
            return;

        Vector3 origin = player.transform.position;
        Vector3 direction = (Camera.main.transform.position - player.transform.position).normalized;
        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(origin, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Wall")))
        {
            Renderer renderer = hit.transform.GetComponent<Renderer>();
            if (currentRenderer != renderer)
            {
                ResetMaterial();
                SetMaterial(renderer);
                currentRenderer = renderer;
            }
        }
        else
        {
            ResetMaterial();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (false)
            return;

        CollisionCheck();
    }
}
