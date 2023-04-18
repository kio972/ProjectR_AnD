using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test1 : MonoBehaviour
{
    IEnumerator Coroutine2(System.Action callback = null)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        callback?.Invoke();
    }

    IEnumerator Coroutine1()
    {
        print("Start_Coroutine");
        yield return StartCoroutine(Coroutine2(() => {}));

        print("End_Coroutine");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Coroutine1());
        }
    }
}
