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
        private int platformsOnScreenNumber = 10;
        
        [SerializeField]
        private Transform finish;
        //[SerializeField]
        //private GameEventListener platformFallListener;

        private int platformsOnLevel = 30;
        private float opositeDirectionSpawnChance = 0.5f;
        private float crystalSpawnChance = 0.2f;

        private Vector3 platformToSpawnPos = Vector3.zero;
        private bool spawnRight = true;
        private float step;
        private int platformCounter = 0;
        private bool finishAdded = false;

        //TODO clamp platforms on screen and on level
        private void Awake()
        {
            
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
            Debug.Log(platformCounter + " platforms added");
            finish.gameObject.SetActive(true);
            finish.position = platformToSpawnPos;
            finishAdded = true;
        }

        private void SpawnCrystal(Vector3 position)
        {
            var crystal = ObjectPooler.GetObjectFromPool("Crystal");
            crystal.gameObject.SetActive(true);
            crystal.transform.position = position;
        }

        private void SetNextPlatformPos()
        {
            if (Random.Range(0f, 1f) < opositeDirectionSpawnChance)
            {
                spawnRight = !spawnRight;
            }
            if (spawnRight)
            {
                var pos = platformToSpawnPos;
                pos.z += step;
                platformToSpawnPos = pos;
            }
            else
            {
                var pos = platformToSpawnPos;
                pos.x += step;
                platformToSpawnPos = pos;
            }
        }

        public void Initialize(int platformsOnLevel, float opositeDirectionSpawnChance, float crystalSpawnChance)
        {
            this.platformsOnLevel = platformsOnLevel;
            this.opositeDirectionSpawnChance = opositeDirectionSpawnChance;
            this.crystalSpawnChance = crystalSpawnChance;
        }

        public void AddPlatform()
        {
            if (platformCounter == platformsOnLevel)
            {
                if (finishAdded)
                {
                    return;
                }
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

            SetNextPlatformPos();
            platformCounter++;
        }
    }
}