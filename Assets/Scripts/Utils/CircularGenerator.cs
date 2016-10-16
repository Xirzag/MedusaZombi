using UnityEngine;
using System.Collections;

public class CircularGenerator : MonoBehaviour {

    public float spawnRate;

    [System.Serializable]
    public struct PrefabRate
    {
        public GameObject prefab;
        public float rate;
    }

    [SerializeField]
    private PrefabRate[] generatedPrefabs;
    private float totalRate = 0;

    public float spawnRadius;
    public KeyCode switchKey;

    private bool stopSpawning = false;

    void SpawnPrefab()
    {
        float random = Random.Range(0, totalRate);
        Vector3 spawnPoint;
        do
        {
            spawnPoint = transform.position + Random.insideUnitSphere * spawnRadius;
        } while (!TerrainScript.isInsidePlane(spawnPoint));

        spawnPoint.y = 0.5f;


        foreach (PrefabRate generatedPrefab in generatedPrefabs)
        {
            random -= generatedPrefab.rate;
            if(random <= 0)
                GameObject.Instantiate(generatedPrefab.prefab, spawnPoint, Quaternion.identity);
        }
    }

    private IEnumerator Generator()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnPrefab();
            yield return new WaitWhile(() => stopSpawning );
        }
    }

    void Start()
    {
        foreach (PrefabRate prefab in generatedPrefabs)
            totalRate += prefab.rate;
        
        StartCoroutine(Generator());
    }

    void Update()
    {
        if (Input.GetKey(switchKey))
            stopSpawning = !stopSpawning;
    }

}
