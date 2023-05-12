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
        firstDialog.Add(new string[2] {"�ٻ��", "���� ���� ���̾� õ���� õ����� �ΰ����� ���� ���ڱ���." });
        secondDialog.Add(new string[2] { "�÷��̾�", "���� ���� �ֽ��ϱ�?" });
        thirdDialog.Add(new string[2] { "�ٻ��", "õ����� ������ �ٲ�� Ư���� ���� �Ͼ�ܴ�." });
        thirdDialog.Add(new string[2] { "�ٻ��", "�׷��� ���� �ٻ� õ����� ���� ���� ���� ������ �� õ����� ��ȭ �� �ַ�." });
        thirdDialog.Add(new string[2] { "�ٻ��", "�ʿ��� �ð��� ������ �Կ��ܴ�." });
        thirdDialog.Add(new string[2] { "�ٻ��", "���� õ�縦 ��ȭ���ټ��� �װ� �� ���� �ҰŶ���." });
        thirdDialog.Add(new string[2] { "�ٻ��", "�׷��� �ִ��� ���� õ�縦 ��ȭ���ַ�." });

        dialogController.CallDialog(firstDialog, CallSecond);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
