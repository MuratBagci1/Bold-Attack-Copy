using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private PoolManager poolManager;
    [SerializeField] private List<GameObject> enemyPrefabList;
    [SerializeField] private float spawnRadius;

    public static EnemySpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ActionManager.OnGameStateChanged += HandleGameState;
        poolManager = PoolManager.Instance;

        foreach (GameObject enemyPrefab in enemyPrefabList)
        {
            poolManager.CreatePool<Enemy>(enemyPrefab.GetComponent<Enemy>(), 10);
        }
    }

    private void HandleGameState(GameManager.GameState state, WaveSO waveSO)
    {
        if(state == GameManager.GameState.GamePlaying && waveSO != null)
        {
            Coroutine enemySpawnCoroutine = StartCoroutine(SpawnEnemy(waveSO));
        }
    }

    private IEnumerator SpawnEnemy(WaveSO wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            int enemyPrefabIndex = UnityEngine.Random.Range(0, enemyPrefabList.Count);

            Enemy spawnedEnemy = poolManager.Get<Enemy>(enemyPrefabList[enemyPrefabIndex].GetComponent<Enemy>());

            yield return new WaitForSeconds(wave.spawnCooldown);
        }
    }

    public Vector3 GiveSpawnPosition()
    {
        Vector2 randomDir = UnityEngine.Random.insideUnitCircle.normalized;

        Vector3 offset = new Vector3(randomDir.x, randomDir.y, 0) * spawnRadius;

        return Player.instance.transform.position + offset;
    }
}