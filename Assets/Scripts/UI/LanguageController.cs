using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageController : Singleton<LanguageController>
{
    private List<Dictionary<string, object>> uiDics;
    public TextAsset uiCSV;

    public string GetValue(string key)
    {
        //아마 수정필요
        if (uiDics == null)
            return key;

        foreach (Dictionary<string, object> uiDic in uiDics)
        {
            if (uiDic.ContainsKey(key))
            {
                return uiDic[key].ToString();
            }
        }

        return key;
    }


    void Start()
    {
        if (uiCSV == null)
            return;
        // uiDic을 불러오는 함수 작성 필요
        uiDics = CSVLoader.LoadCSV(uiCSV);
    }

}
