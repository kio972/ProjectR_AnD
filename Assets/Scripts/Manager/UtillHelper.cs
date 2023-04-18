using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public enum AdjacentDirection
{
    Right,
    Left,
    Up,
    Down,
    None,
}

public static class UtillHelper
{
    public static IEnumerator RotateTowards(Transform transform, Vector3 targetPos, float rotateTime, System.Action callback = null)
    {
        float elapsedTime = 0f;
        Vector3 lookDir = targetPos - transform.position;
        lookDir.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookDir);
        while (elapsedTime < rotateTime)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / rotateTime);
            yield return null;
        }

        // 코루틴 종료시 callback 호출
        callback?.Invoke();
    }

    public static T GetSkill<T>() where T : SkillMain
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type[] types = assembly.GetTypes();
        foreach (Type type in types)
        {
            if (type.IsSubclassOf(typeof(SkillMain)) && type.Name == typeof(T).Name)
            {
                return (T)Activator.CreateInstance(type);
            }
        }

        return null;
    }

    public static Vector3 GetMouseWorldPosition(Vector3 playerPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float yPosition = playerPosition.y;

        worldPosition = new Vector3(worldPosition.x, yPosition, worldPosition.z);

        return worldPosition;
    }

    public static AdjacentDirection GetOppositeDirection(AdjacentDirection direction)
    {
        switch (direction)
        {
            case AdjacentDirection.Right:
                return AdjacentDirection.Left;
            case AdjacentDirection.Left:
                return AdjacentDirection.Right;
            case AdjacentDirection.Up:
                return AdjacentDirection.Down;
            case AdjacentDirection.Down:
                return AdjacentDirection.Up;
            default:
                return AdjacentDirection.None;
        }
    }

    public static AdjacentDirection GetAdjacentDirection(CellManager currentCellManager, CellManager adjacentCellManager)
    {
        int currentRow = currentCellManager.mapRow;
        int currentCol = currentCellManager.mapCol;
        int adjacentRow = adjacentCellManager.mapRow;
        int adjacentCol = adjacentCellManager.mapCol;

        int rowDiff = Mathf.Abs(currentRow - adjacentRow);
        int colDiff = Mathf.Abs(currentCol - adjacentCol);

        if (rowDiff == 1 && colDiff == 0)
        {
            if (adjacentRow > currentRow) return AdjacentDirection.Up;
            else return AdjacentDirection.Down;
        }
        else if (rowDiff == 0 && colDiff == 1)
        {
            if (adjacentCol > currentCol) return AdjacentDirection.Right;
            else return AdjacentDirection.Left;
        }
        else
            return AdjacentDirection.None;
    }

    public static IEnumerator ScaleLerp(Transform target, float startScale, float endScale, float lerpTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < lerpTime)
        {
            elapsedTime += Time.deltaTime;
            float nextScale = Mathf.Lerp(startScale, endScale, elapsedTime / lerpTime);
            target.localScale = new Vector3(nextScale, nextScale, nextScale);
            yield return null;
        }
        target.localScale = new Vector3(endScale, endScale, endScale);
    }

    public static void ActiveTrigger(Transform transform, string triggerName)
    {
        Animator animator = GetComponetInChildren<Animator>(transform);
        if (animator != null)
            animator.SetTrigger(triggerName);
    }

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static IEnumerator DelayedFunctionCall(UnityAction func, float delayTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < delayTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        func();
    }

    public static IEnumerator ReActiveCollider(Collider2D collider, float delayTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < delayTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        collider.isTrigger = false;
    }

    public static T Find<T>(Transform transform, string path, bool init = false) where T : Component
    {
        Transform trans = transform.Find(path);
        T findObject = null;
        if (trans != null)
        {
            findObject = trans.GetComponent<T>();
            if (init)
                trans.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
        }
        
        return findObject;
    }

    public static T GetComponetInChildren<T>(Transform transform, bool init = false) where T : Component
    {
        T t = transform.GetComponentInChildren<T>();
        if (t != null && init)
            t.SendMessage("Init", SendMessageOptions.DontRequireReceiver);

        return t;
    }

    public static T FindobjectOfType<T>(bool init = false) where T : Component
    {
        T t = GameObject.FindObjectOfType<T>();
        if (t != null)
        {
            if (init)
                t.transform.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
        }
        return t;
    }

    // 타겟이 되는 transform / 타겟으로부터의 경로 / 연결할 함수
    public static Button BindingFunc(Transform transform, string path, UnityAction action)
    {
        Button button = Find<Button>(transform, path);
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
        return button;
    }

    public static Button BindingFunc(Transform transform, UnityAction action)
    {
        Button button = transform.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
        return button;
    }

    public static T Instantiate<T>(string path, Transform parent, bool init = false, bool active = true) where T : UnityEngine.Component
    {
        T objectType = Resources.Load<T>(path);
        if (objectType != null)
        {
            objectType = UnityEngine.Object.Instantiate(objectType);
            if (objectType != null)
            {
                if (init)
                    objectType.SendMessage("Init", SendMessageOptions.DontRequireReceiver);

                objectType.gameObject.SetActive(active);
            }
        }
        return objectType;

    }

    public static T CreateObject<T>(Transform parent, bool init = false) where T : Component
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
        obj.transform.SetParent(parent);
        T t = obj.GetComponent<T>();
        if (init)
            t.SendMessage("Init", SendMessageOptions.DontRequireReceiver);

        return t;
    }
}
