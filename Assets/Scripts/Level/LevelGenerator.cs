using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag.Events;
using ZigZag.Pooling;

namespace ZigZag.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private int platformsOnScreenNumber = 20;
        [SerializeField]
        private int platformsOnLevel = 30;
        [SerializeField]
        [Range(0.1f, 0.7f)]
        private float opositeDirectionSpawnChance = 0.5f;
        [SerializeField]
        [Range(0.01f, 0.3f)]
        private float crystalSpawnChance = 0.2f;
        [SerializeField]
        private Transform finish;

        private GameEventListener listener;
        private Vector3 platformToSpawnPos = Vector3.zero;
        private bool spawnedRight = true;
        private float step;
        private int platformCounter;

        //TODO clamp platforms on screen and on level
        private void Awake()
        {
            TryGetComponent(out listener);
        }

        private void Start()
        {
            step = ObjectPooler.GetObjectFromPool("Platform").transform.lossyScale.x;
            for (int i = 0; i < platformsOnScreenNumber; i++)
            {
                AddPlatform();
            }
        }

        private void AddFinish()
        {
            finish.gameObject.SetActive(true);
            finish.position = platformToSpawnPos;
            listener.Response.RemoveAllListeners();
        }

        private void SpawnCrystal(Vector3 position)
        {
            var crystal = ObjectPooler.GetObjectFromPool("Crystal");
            crystal.gameObject.SetActive(true);
            crystal.transform.position = position;
            Debug.Log(position);
        }

        public void AddPlatform()
        {
            if (platformCounter == platformsOnLevel)
            {
                AddFinish();
                return;
            }

            var platform = ObjectPooler.GetObjectFromPool("Platform");
            platform.gameObject.SetActive(true);
            platform.transform.position = platformToSpawnPos;

            if (Random.Range(0f, 1f) < crystalSpawnChance)
            {
                SpawnCrystal(platformToSpawnPos);
            }

            if (Random.Range(0f, 1f) < opositeDirectionSpawnChance)
            {
                spawnedRight = !spawnedRight;
                var pos = platformToSpawnPos;
                pos.x += step;
                platformToSpawnPos = pos;
            }
            else
            {
                var pos = platformToSpawnPos;
                pos.z += step;
                platformToSpawnPos = pos;
            }
            platformCounter++;
        }
    }
}