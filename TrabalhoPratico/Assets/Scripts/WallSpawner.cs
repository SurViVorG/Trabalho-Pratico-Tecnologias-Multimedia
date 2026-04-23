using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [Header("Prefabs das paredes")]
    public GameObject wallNorthPrefab;
    public GameObject wallSouthPrefab;
    public GameObject wallEastPrefab;
    public GameObject wallWestPrefab;

    [Header("Timing")]
    public float spawnInterval = 3f;    // segundos entre ondas
    public float minInterval   = 1f;    // intervalo mínimo (dificuldade máxima)
    public float speedIncrease = 0.1f;  // quanto a velocidade aumenta por onda

    [Header("Velocidade inicial das paredes")]
    public float wallSpeed = 5f;

    private float timer;
    private int wave = 0;
    private float spawnDistance = 12f;

    void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnWave();
            }
        }
    }

    void SpawnWave()
    {
        wave++;
        GameManager.Instance.AddWave();

        // Aumenta dificuldade a cada 3 ondas
        if (wave % 3 == 0)
        {
            wallSpeed     += speedIncrease;
            spawnInterval  = Mathf.Max(minInterval, spawnInterval - 0.2f);
        }

        // Decide quais direções spawnar nesta onda
        // Ondas iniciais: só 1 parede; depois 2 opostas; depois todas 4
        int wallCount = wave < 3 ? 1 : (wave < 6 ? 2 : 4);
        int[] dirs = GetRandomDirections(wallCount);

        foreach (int dir in dirs)
        {
            SpawnWall(dir);
        }
    }

    void SpawnWall(int dir)
    {
        GameObject prefab;
        Vector3 pos;

        switch (dir)
        {
            case 0: // Norte
                prefab = wallNorthPrefab;
                pos    = new Vector3(0, 1, spawnDistance);
                break;
            case 1: // Sul
                prefab = wallSouthPrefab;
                pos    = new Vector3(0, 1, -spawnDistance);
                break;
            case 2: // Este
                prefab = wallEastPrefab;
                pos    = new Vector3(spawnDistance, 1, 0);
                break;
            default: // Oeste
                prefab = wallWestPrefab;
                pos    = new Vector3(-spawnDistance, 1, 0);
                break;
        }

        GameObject wall = Instantiate(prefab, pos, Quaternion.identity);
        wall.GetComponent<WallMover>().speed = wallSpeed;
    }

    int[] GetRandomDirections(int count)
    {
        int[] all = { 0, 1, 2, 3 };
        // Shuffle simples
        for (int i = 0; i < all.Length; i++)
        {
            int j   = Random.Range(i, all.Length);
            int tmp = all[i]; all[i] = all[j]; all[j] = tmp;
        }
        int[] result = new int[count];
        System.Array.Copy(all, result, count);
        return result;
    }
}