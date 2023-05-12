using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDialog : MonoBehaviour
{
    public List<string[]> firstDialog = new List<string[]>();
    public List<string[]> secondDialog = new List<string[]>();
    public List<string[]> thirdDialog = new List<string[]>();
    public DialogController dialogController;
    public SpriteList spriteList;

    private void EndDialog()
    {
        SceneController.Instance.MoveScene("Stage1Scene", 1f);
    }

    private void CallThird()
    {
        dialogController.CallDialog(thirdDialog, EndDialog);
        dialogController.illurst.sprite = spriteList.sprites[1];
    }

    private void CallSecond()
    {
        dialogController.CallDialog(secondDialog, CallThird);
        dialogController.illurst.sprite = spriteList.sprites[0];
    }
    
    // Start is called before the first frame update
    void Start()
    {
        firstDialog.Add(new string[2] {"바사고", "작은 꼬마 아이야 천계의 천사들이 인계해줄 수가 없겠구나." });
        secondDialog.Add(new string[2] { "플레이어", "무슨 일이 있습니까?" });
        thirdDialog.Add(new string[2] { "바사고", "천사들이 괴물로 바뀌는 특이한 일이 일어났단다." });
        thirdDialog.Add(new string[2] { "바사고", "그래서 지금 바쁜 천사들을 위해 나와 같이 괴물이 된 천사들을 정화 해 주렴." });
        thirdDialog.Add(new string[2] { "바사고", "너에게 시간의 물약을 먹였단다." });
        thirdDialog.Add(new string[2] { "바사고", "많은 천사를 정화해줄수록 네가 더 성장 할거란다." });
        thirdDialog.Add(new string[2] { "바사고", "그러니 최대한 많은 천사를 정화해주렴." });

        dialogController.CallDialog(firstDialog, CallSecond);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
