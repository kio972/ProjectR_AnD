using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop_ActivatonDelay : MonoBehaviour {

        public GameObject objectToActivate;
        public float Delay;

        private void Start()
        {
            StartCoroutine(ActivationRoutine());
        }

        private IEnumerator ActivationRoutine()
        {
            yield return new WaitForSeconds(Delay);

            objectToActivate.SetActive(true);

        }
    }
