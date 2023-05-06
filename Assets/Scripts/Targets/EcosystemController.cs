using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcosystemController : MonoBehaviour
{
    [SerializeField] private GameObject[] targetPrefabs;
    [SerializeField] private GameObject targetsParent;
    [SerializeField] private float spawnInterval = 10f;

    private void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    private IEnumerator SpawnTargets()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int targetIndex = Random.Range(0, targetPrefabs.Length);
            GameObject targetPrefab = targetPrefabs[targetIndex];
            Vector3 spawnPosition = GetRandomSpawnPosition();

            GameObject newTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
            newTarget.transform.SetParent(targetsParent.transform);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Adjust these values according to your scene bounds
        float x = Random.Range(-61, -40);
        float y = Random.Range(1, 1);
        float z = Random.Range(10, 14);

        return new Vector3(x, y, z);
    }
}
