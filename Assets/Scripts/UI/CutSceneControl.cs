using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneControl : MonoBehaviour
{
    void StartCutScene()
    {
        GameManager.Instance.PlayCutScene(true);
    }
    void EndCutScene()
    {
        GameManager.Instance.PlayCutScene(false);
    }
}
