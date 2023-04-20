using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{

    private void SceneMove()
    {
        SceneController.Instance.MoveScene("CellScene", 0.5f);
    }

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(SceneMove);
    }
}
