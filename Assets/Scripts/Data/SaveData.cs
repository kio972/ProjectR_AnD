using System;

[Serializable]
public class SaveData
{
    //Basic Setting
    public float totalVolume = 1f;
    public float bgmVolume = 1f;
    public float fxVolume = 1f;
    public string language = "Korean";
    public bool angleMode = false;
    public bool visibleTimer = false;

    //Control Setting
    public string controlMode = "Mixed";
    public float mouseSensitivity = 1.0f;
    public int moveKey = 324;
    public int aimKey = 323;
    public int attackKey = 113;
    public int skill1Key = 119;
    public int skill2Key = 101;
    public int specialAttackKey = 114;

    //Display Setting
    public int iSizeW = 1920;
    public int iSizeH = 1080;
    public int maxFPS = -1;
    public bool screenShaking = true;

    //Difficulty Setting


    //Player Data
    public float curHp;
    public float maxHp;
    public int[] synergys;

}
