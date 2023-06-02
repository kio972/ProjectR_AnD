using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private List<Dictionary<string, object>> skill_Active_Dic;
    private List<Dictionary<string, object>> skill_Passive_Dic;
    private List<Dictionary<string, object>> characterDic;
    private List<Dictionary<string, object>> mapTableDic;

    public List<Dictionary<string, object>> MapTableDic { get => mapTableDic; }
    public List<Dictionary<string, object>> Skill_Active_Dic { get => skill_Active_Dic; }
    public List<Dictionary<string, object>> Skill_Passive_Dic { get => skill_Passive_Dic; }
    public List<Dictionary<string, object>> CharacterDic { get => characterDic; }

    // csv파일 주소(Resource폴더 내)
    private string skill_Active_DataPath = "Data/";
    private string skill_Passive__DataPath = "Data/PassiveSkill";
    private string character_DataPath = "Data/CharacterTable";
    private string mapTable_DataPath = "Data/SpawnTable";

    public int Find_SpawnMonsterID(int mapID, int spawnPointIndex)
    {
        for (int i = 0; i < mapTableDic.Count; i++)
        {
            int mapTableID = Convert.ToInt32(mapTableDic[i]["MapID"]);
            int spawnPoint = Convert.ToInt32(mapTableDic[i]["SpawnPoint"]);
            if (mapID == mapTableID && spawnPointIndex == spawnPoint)
                return Convert.ToInt32(mapTableDic[i]["MonsterID"]);
        }
        return -1;
    }

    private void Init()
    {
        // csv파일 불러오는 함수
        //skill_Active_Dic = CSVLoader.LoadCSV(Resources.Load<TextAsset>(skill_Active_DataPath));
        skill_Passive_Dic = CSVLoader.LoadCSV(Resources.Load<TextAsset>(skill_Passive__DataPath));
        characterDic = CSVLoader.LoadCSV(Resources.Load<TextAsset>(character_DataPath));
        mapTableDic = CSVLoader.LoadCSV(Resources.Load<TextAsset>(mapTable_DataPath));
    }
}
