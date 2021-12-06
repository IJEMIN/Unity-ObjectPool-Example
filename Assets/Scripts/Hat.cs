using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Hat : MonoBehaviour
{
    public IObjectPool<Hat> poolToReturn;
    public bool IsTriggered { get; private set; }

    public void Reset()
    {
        IsTriggered = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (IsTriggered)
        {
            return;
        }
        
        if (other.collider.CompareTag("Ground"))
        {
            IsTriggered = true;
            StartCoroutine(DestroyHat());
        }
    }

    private IEnumerator DestroyHat()
    {
        yield return new WaitForSeconds(3f);
        poolToReturn.Release(this);
    }
}