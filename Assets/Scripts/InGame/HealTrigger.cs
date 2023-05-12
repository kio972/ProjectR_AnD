using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Controller controller = other.gameObject.transform.GetComponentInParent<Controller>();
            controller.ModifyHp(controller.maxHp);
            EffectManager.Instance.PlayEffect("HealEffect", controller.transform);
        }
    }
}
