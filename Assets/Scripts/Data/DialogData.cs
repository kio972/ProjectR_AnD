using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData : Singleton<DialogData>
{
    [SerializeField]
    private string indexKey = "Index";
    [SerializeField]
    private string speakerKey = "Speaker";
    [SerializeField]
    private string dialogKey = "Dialog";

    public TextAsset dialogCSV;
    private List<Dictionary<string, object>> dialogDictionary;
    public List<Dictionary<string, object>> DialogDictionary
    {
        get
        {
            if (dialogDictionary == null)
                Init();
            return dialogDictionary;
        }
    }

    public List<string[]> LoadDialog(int index)
    {
        List<string[]> values = new List<string[]>();
        
        foreach (var dict in DialogDictionary)
        {
            if (dict.ContainsKey(indexKey))
            {
                string[] pair = new string[2];
                pair[0] = dict[speakerKey].ToString();
                pair[1] = dict[dialogKey].ToString();
                values.Add(pair);
            }
        }

        return values;
    }

    private void Init()
    {
        if (dialogCSV == null)
            return;

        dialogDictionary = CSVLoader.LoadCSV(dialogCSV);
    }
}
