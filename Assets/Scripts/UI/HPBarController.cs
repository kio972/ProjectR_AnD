using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarController : Singleton<HPBarController>
{
    Canvas overlayCanvas;
    Canvas cameraCanvas;
    public Canvas CameraCanvas { get { return cameraCanvas; } }

    HPUpdater player_HP;
    HPUpdater boss_HP;
    List<HPUpdater> monsters_HP = new List<HPUpdater>();

    private void Monster_HP_Pooling(Controller controller)
    {
        // monsters_HP내에 비활성화된 체력바가 있다면, 재활용
        foreach (HPUpdater hp in monsters_HP)
        {
            if(hp.gameObject.activeSelf == false)
            {
                hp.gameObject.SetActive(true);
                hp.Init(controller);
                return;
            }
        }

        // monsters_HP내에 비활성화된 체력바가 없다면 새로 생성하고 리스트에 추가
        HPUpdater newHpBar = UtillHelper.Instantiate<HPUpdater>("Prefab/UI/Monster_HP_Bar", cameraCanvas.transform);
        newHpBar.Init(controller);
        monsters_HP.Add(newHpBar);
    }

    public void InstantiateHPBar(Controller controller)
    {
        switch (controller.unitType)
        {
            case UnitType.Player:
                player_HP.Init(controller);
                break;
            case UnitType.Monster:
                Monster_HP_Pooling(controller);
                break;
            case UnitType.Boss:
                boss_HP.Init(controller);
                boss_HP.gameObject.SetActive(true);
                break;
        }
    }

    private void Init()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach(Canvas canvas in canvases)
        {
            if (canvas.gameObject.name == "IngameOverlayUI")
                overlayCanvas = canvas;
            if (canvas.gameObject.name == "IngameCameraUI")
                cameraCanvas = canvas;
        }

        if(overlayCanvas == null)
            overlayCanvas = UtillHelper.Instantiate<Canvas>("Prefab/UI/IngameOverlayUI", null);

        if (cameraCanvas == null)
            cameraCanvas = UtillHelper.Instantiate<Canvas>("Prefab/UI/IngameCameraUI", null);

        player_HP = UtillHelper.Find<HPUpdater>(overlayCanvas.transform, "Player_HP_Bar");
        boss_HP = UtillHelper.Find<HPUpdater>(overlayCanvas.transform, "Boss_HP_Bar");
        boss_HP.gameObject.SetActive(false);
        HPUpdater[] monsters = cameraCanvas.GetComponentsInChildren<HPUpdater>();
        foreach(HPUpdater monster in monsters)
            monsters_HP.Add(monster);
        foreach (HPUpdater monster in monsters_HP)
            monster.gameObject.SetActive(false);
    }
}
