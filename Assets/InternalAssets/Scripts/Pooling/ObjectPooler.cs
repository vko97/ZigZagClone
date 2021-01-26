using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZigZag.Pooling
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public int size;
            public GameObject prefab;
            public string tag;
        }
        public List<Pool> pools;

        private static Dictionary<string, List<GameObject>> poolDictionary;
        private static Transform trans;
        private static List<Pool> _pools;

        private void Awake()
        {
            trans = transform;
            _pools = pools;
            poolDictionary = new Dictionary<string, List<GameObject>>();
            foreach (Pool pool in pools)
            {
                List<GameObject> poolObjects = new List<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.transform.SetParent(transform);
                    obj.SetActive(false);
                    poolObjects.Add(obj);
                }

                poolDictionary.Add(pool.tag, poolObjects);
            }
        }

        public static GameObject GetObjectFromPool(string tag)
        {
            if (!poolDictionary.TryGetValue(tag, out var pool))
            {
                return null;
            }
            return GetPoolObject(pool, _pools.First(p => p.tag.Equals(tag)).prefab);
        }

        private static GameObject GetPoolObject(List<GameObject> pool, GameObject prefab)
        {
            var obj = pool.FirstOrDefault(g => g.activeSelf == false);

            if (!obj)
            {
                obj = Instantiate(prefab);
                obj.transform.SetParent(trans);
                obj.SetActive(false);
                pool.Add(obj);
            }
            return obj;
        }
    }
}