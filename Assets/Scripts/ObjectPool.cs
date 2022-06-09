using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Init(GameObject prefab, Transform target)
    {
        for(int i = 0; i < _capacity; i++)
        {
            CreateObject(prefab, target, i == 0 ? true : false);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }

    private void CreateObject(GameObject prefab, Transform target, bool isActiveByDefault = false)
    {
        GameObject spawned = Instantiate(prefab, _container);
        spawned.GetComponent<Enemy>().TargetChase = target;
        spawned.SetActive(isActiveByDefault);
        if(isActiveByDefault)
        {
            spawned.transform.position = new Vector3(13.52f, -2.75f, 1);
        }
        _pool.Add(spawned);
    }
}