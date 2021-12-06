using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HatCreator : MonoBehaviour
{
    private ObjectPool<Hat> _hatPool; 
    public Hat hatPrefab;

    private void Start()
    {
        _hatPool = new ObjectPool<Hat>(
            createFunc: () =>
            {
                var createdHat = Instantiate(hatPrefab);
                createdHat.poolToReturn = _hatPool;
                return createdHat;
            },
            actionOnGet: (hat) =>
            {
                hat.gameObject.SetActive(true);
                hat.Reset();
            },
            actionOnRelease: (hat) =>
            {
                hat.gameObject.SetActive(false);
            },
            actionOnDestroy: (hat) =>
            {
                Destroy(hat.gameObject);
            }, maxSize: 30);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var count = Random.Range(1, 10);

            for (var i = 0; i < count; i++)
            {
                CreateHat();
            }
        }
    }

    private void CreateHat()
    {
        var position = Random.insideUnitSphere + new Vector3(0, 3, 0);
        var rotation = Random.rotation;
        var hat = _hatPool.Get();
        hat.transform.position = position;
        hat.transform.rotation = rotation;
    }
}