using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader
{
    public static List<Dictionary<string, string>> LoadCSV(TextAsset textFile)
    {
        string str = textFile.text;

        string[] lines = str.Split('\n');

        string[] heads = lines[0].Split(',');

        heads[heads.Length - 1] = heads[heads.Length - 1].Replace("\r", "");

        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

        for (int i = 1; i < lines.Length; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] col = lines[i].Split(',');
            col[col.Length - 1] = col[col.Length - 1].Replace("\r", "");
            for (int j = 0; j < heads.Length; j++)
            {
                string value = col[j];

                dic.Add(heads[j], col[j]);
            }

            list.Add(dic);
        }

        return list;
    }
}
