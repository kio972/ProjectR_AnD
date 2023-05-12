using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public SpawnController spawnGroup;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(spawnGroup != null)
            {
                spawnGroup.SpawnMonsters();
                this.gameObject.SetActive(false);
            }
        }
    }
}
