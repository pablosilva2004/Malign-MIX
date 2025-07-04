using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCapsuleSpawner : MonoBehaviour
{
    [SerializeField] GameObject capsulePrefab;
    [SerializeField] List<Transform> spawnPoints;

    GameObject currentCapsule;

    void Start()
    {
        SpawnCapsuleAtRandomPoint();
    }

    void SpawnCapsuleAtRandomPoint()
    {
        if (spawnPoints.Count == 0 || capsulePrefab == null) return;

        int index = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[index];

        currentCapsule = Instantiate(capsulePrefab, spawnPoint.position, Quaternion.identity);
        
        CapsuleTarget capsuleTarget = currentCapsule.GetComponent<CapsuleTarget>();
        capsuleTarget.OnCapsuleDestroyed += OnCapsuleDestroyed;
    }

    void OnCapsuleDestroyed(CapsuleTarget destroyedCapsule)
    {
        StartCoroutine(RespawnCapsuleAfterDelay(3f));
    }

    IEnumerator RespawnCapsuleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnCapsuleAtRandomPoint();
    }
}

