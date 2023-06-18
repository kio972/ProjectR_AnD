using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isIngame = false;

    public void PlayCutScene(bool value)
    {
        Canvas[] canvas = FindObjectsOfType<Canvas>(true);
        foreach(Canvas ui in canvas)
        {
            if (ui.tag == "MainUI")
                continue;

            ui.gameObject.SetActive(!value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
