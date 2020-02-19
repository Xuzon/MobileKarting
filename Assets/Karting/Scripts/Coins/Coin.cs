using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    protected float disabledTime = 5;
    protected Renderer rend;
    protected Collider coll;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CoinManager.singleton.AddCoin();
            DisableForTime(disabledTime);
        }
    }

    protected void DisableForTime(float time)
    {
        IEnumerator DisableRoutine()
        {
            SetObject(false);
            yield return new WaitForSeconds(disabledTime);
            SetObject(true);
        }
        StartCoroutine(DisableRoutine());
    }

    protected void SetObject(bool toSet)
    {
        rend.enabled = toSet;
        coll.enabled = toSet;
    }
}
