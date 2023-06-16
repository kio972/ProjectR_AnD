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
    public static void UpdateUIPosition(Vector3 worldPosition, RectTransform uiPosition, Vector2 offset = new Vector2())
    {
        // 월드 좌표를 스크린 좌표로 변환
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        if (offset != new Vector2())
            screenPosition += offset;

        uiPosition.anchoredPosition = screenPosition;
    }

    public static void SetColor(Material material, Color color, string key)
    {
        material.SetColor(key, color);
    }

    public static void SetColor(Renderer renderer, Color color, string key)
    {
        renderer.material.SetColor(key, color);
    }

    public static IEnumerator IChangeColor(Material material, Color color, string key, float lerpTime)
    {
        Color originColor = material.GetColor(key);
        float elapsedTime = 0f;

        while (elapsedTime < lerpTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpTime);
            Color lerpedColor = Color.Lerp(originColor, color, t);
            material.SetColor(key, lerpedColor);
            yield return null;
        }

        // 최종적으로 목표 색상으로 설정
        material.SetColor(key, color);
    }

    public static Vector3 GetRandomPosition(Vector3 zeroPos, float radius, float deadRadius = 0f)
    {
        while(true)
        {
            Vector3 randomPosition = zeroPos;
            Vector2 modify = UnityEngine.Random.insideUnitCircle * radius;
            randomPosition.x += modify.x;
            randomPosition.z += modify.y;
            randomPosition.y = zeroPos.y;

            float distance = Mathf.Abs((zeroPos - randomPosition).magnitude);
            if (distance > deadRadius)
                return randomPosition;
        }
    }

    public static float TargetAngle(Transform myTransform, Vector3 targetPos)
    {
        // 타겟과의 방향 벡터 계산
        Vector3 targetDirection = targetPos - myTransform.position;
        targetDirection.y = 0f; // 수평 방향으로만 고려하기 위해 y 축 값은 0으로 설정

        // 내 전방 벡터와 타겟 방향 벡터 사이의 각도 계산
        float angle = Vector3.Angle(myTransform.forward, targetDirection);

        //// 내 전방 벡터와 타겟 방향 벡터의 외적을 통해 각도의 부호(양수 또는 음수) 결정
        //Vector3 cross = Vector3.Cross(myTransform.forward, targetDirection);
        //if (cross.y < 0f)
        //{
        //    angle = -angle; // 각도의 부호를 반대로 설정
        //}

        return Mathf.Abs(angle);
    }

    public static T AddSkill<T>(Transform parent, string skillName) where T: SkillMain
    {
        GameObject skillObject = new GameObject();
        skillObject.name = skillName;
        skillObject.transform.SetParent(parent);
        return skillObject.AddComponent<T>();
    }

    public static T Find_Prefab<T>(int id, List<Dictionary<string, object>> dataDic) where T : UnityEngine.Object
    {
        int index = Find_Data_Index(id, dataDic);
        string prefabPath = dataDic[index]["Prefab"].ToString();
        T prefab = Resources.Load<T>(prefabPath);
        return prefab;
    }

    public static int Find_Data_Index(object target, List<Dictionary<string, object>> targetDic, string key = "ID")
    {
        for (int i = 0; i < targetDic.Count; i++)
        {
            if (targetDic[i][key].ToString() == target.ToString())
            {
                return i;
            }
        }
        return -1; // 일치하는 데이터가 없을 경우 -1 반환
    }

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, -playerPosition.y);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            return point;
        }

        // groundPlane과 교차하지 않을 경우, playerPosition을 반환합니다.
        return playerPosition;
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
            objectType = UnityEngine.Object.Instantiate(objectType, parent);
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
