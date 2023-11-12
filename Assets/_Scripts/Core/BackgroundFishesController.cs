using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random; // Make sure to include this for UniTask

public class BackgroundFishesController : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private float _SpawnTopBorder;
    [SerializeField] private float _spawnFromX;
    [SerializeField] private float _sinMoveMult;
    [SerializeField] private Vector2 _zSpawnRange;
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private Vector2 _spawnNewDelayRange;
    
    private List<Transform> _fishesTransforms = new List<Transform>();
    private List<float> _sinOffsets = new List<float>();
    private List<float> _fishesSpeed = new List<float>();

    private void Start()
    {
        bool firstSkiped = false;
        
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (firstSkiped == false)
            {
                firstSkiped = true;
                continue;
            }
            
            InitNewFish(child);
        }
        
        SpawnFishesPeriodically().Forget();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        
        for (int i = _fishesTransforms.Count - 1; i >= 0; i--)
        {
            Transform fishTransform = _fishesTransforms[i];
            Vector3 fishPos = fishTransform.position;

            float fishSpeed = _fishesSpeed[i] * deltaTime;
            float sinOffset = _sinOffsets[i];
            
            fishPos = Vector3.Lerp(fishPos, fishPos + fishTransform.forward * fishSpeed, deltaTime * 6);
            fishPos.y = Mathf.Lerp(fishPos.y, fishPos.y + Mathf.Sin((Time.time + sinOffset) * fishSpeed) * _sinMoveMult, deltaTime * 4f);

            fishTransform.position = fishPos;

            if (fishPos.x >= -_spawnFromX)
            {
                // Assuming there's a method to deactivate or destroy the fish
                DeactivateFish(fishTransform.gameObject);
                _fishesTransforms.RemoveAt(i);
                _fishesSpeed.RemoveAt(i);
            }
        }
    }

    private async UniTaskVoid SpawnFishesPeriodically()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(_spawnNewDelayRange.x, _spawnNewDelayRange.y);
            await UniTask.Delay(TimeSpan.FromSeconds(delay));

            SpawnFish();
        }
    }

    private void SpawnFish()
    {
        // Assuming a method to spawn or activate a fish GameObject
        GameObject fish = ActivateFish();
        fish.transform.position = new Vector3(_spawnFromX, Random.Range(0.5f, _SpawnTopBorder), UnityEngine.Random.Range(_zSpawnRange.x, _zSpawnRange.y));
        
        InitNewFish(fish.transform);
    }

    private GameObject ActivateFish()
    {
        return Instantiate(fishPrefab);
    }

    private void InitNewFish(Transform newFish)
    {
        float yRot = Random.Range(75, 115);
        Vector3 rot = newFish.transform.rotation.eulerAngles;
        rot.y = yRot;
        newFish.transform.rotation = Quaternion.Euler(rot);

        _fishesTransforms.Add(newFish);
        _sinOffsets.Add(Random.Range(-100f, 100f));
        _fishesSpeed.Add(UnityEngine.Random.Range(_speedRange.x, _speedRange.y));
    }
    
    private void DeactivateFish(GameObject fish)
    {
        // Implement your fish deactivation/destruction logic here
        // This is a placeholder
        Destroy(fish);
    }
}