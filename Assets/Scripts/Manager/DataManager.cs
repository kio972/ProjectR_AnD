using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private List<Dictionary<string, string>> skill_Active_Dic;
    private List<Dictionary<string, string>> skill_Passive_Dic;
    private List<Dictionary<string, string>> characterDic;

    public List<Dictionary<string, string>> Skill_Active_Dic { get => skill_Active_Dic; }
    public List<Dictionary<string, string>> Skill_Passive_Dic { get => skill_Passive_Dic; }
    public List<Dictionary<string, string>> CharacterDic { get => characterDic; }

    // csv���� �ּ�(Resource���� ��)
    private string skill_Active_DataPath = "Data/";
    private string skill_Passive__DataPath = "Data/";
    private string character_DataPath = "Data/";

    private void Init()
    {
        // csv���� �ҷ����� �Լ�
        skill_Active_Dic = CSVLoader.LoadCSV(Resources.Load<TextAsset>(skill_Active_DataPath));
        skill_Passive_Dic = CSVLoader.LoadCSV(Resources.Load<TextAsset>(skill_Passive__DataPath));
        characterDic = CSVLoader.LoadCSV(Resources.Load<TextAsset>(character_DataPath));
    }
}
