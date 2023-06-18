using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyStone : MonoBehaviour
{
    public bool isActive = false;
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private ParticleSystem effect;

    private Color originColor;

    private List<Coroutine> colorCoroutines;

    private bool playerClosed = false;

    private RewardUI rewardUI;

    private Controller player = null;

    private void OnTriggerEnter(Collider other)
    {
        Controller player = other.transform.GetComponentInParent<Controller>();
        if (player == null)
            return;
        if(player.unitType == UnitType.Player)
        {
            playerClosed = true;
            this.player = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Controller player = other.transform.GetComponentInParent<Controller>();
        if (player == null)
            return;
        if (player.unitType == UnitType.Player)
        {
            playerClosed = false;
            this.player = null;
        }
    }

    private void InteractCheck()
    {
        if(rewardUI == null)
            rewardUI = FindObjectOfType<RewardUI>();

        if (playerClosed)
        {
            rewardUI.UpdateInteractText(true, transform.position);
            if(Input.GetKeyDown(KeyCode.F))
            {
                isActive = false;
                EffectManager.Instance.PlayEffect("HealEffect", transform);
                rewardUI.UpdateInteractText(false, transform.position);
                player.ModifyHp(player.maxHp * 0.15f);
                Invoke("CallSynergy", 1.2f);
            }
        }
        else
            rewardUI.UpdateInteractText(false, transform.position);
    }

    private void CallSynergy()
    {
        if (rewardUI == null)
            rewardUI = FindObjectOfType<RewardUI>();
        if (rewardUI == null)
            return;

        //시너지 3개를 랜덤으로 호출
        
        rewardUI.CallReward();
    }

    public void SetActive(bool value)
    {
        Color color = originColor;
        if (!value)
            color = Color.black;

        if (colorCoroutines != null)
        {
            foreach (Coroutine coroutine in colorCoroutines)
                StopCoroutine(coroutine);
        }
        colorCoroutines = new List<Coroutine>();
        foreach (Renderer renderer in renderers)
        {
            Coroutine co = StartCoroutine(UtillHelper.IChangeColor(renderer.material, color, "_BaseColor", 1f));
            colorCoroutines.Add(co);
        }

        if (value)
        {
            effect.Play();
        }
        else
            effect.Stop();

        isActive = value;
    }

    private void Awake()
    {
        //renderers = GetComponentsInChildren<Renderer>();
        originColor = renderers[0].material.GetColor("_BaseColor");

        SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
            InteractCheck();
    }
}
