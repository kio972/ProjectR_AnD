using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageController : Singleton<LanguageController>
{
    private List<Dictionary<string, object>> uiDics;
    public TextAsset uiCSV;

    public string GetValue(string key)
    {
        //�Ƹ� �����ʿ�
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
        // uiDic�� �ҷ����� �Լ� �ۼ� �ʿ�
        uiDics = CSVLoader.LoadCSV(uiCSV);
    }

}
