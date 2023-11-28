using P1_Pooling;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Enemy EnemyPrefab;
    private const float _totalCooldown = 2f;
    private float _currentCooldown;
    
    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(new []{EnemyPrefab}, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this._currentCooldown -= Time.deltaTime;
        if (this._currentCooldown <= 0f)
        {
            this._currentCooldown += _totalCooldown;
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        var maxAmount = Mathf.CeilToInt(Time.timeSinceLevelLoad / 7);
        int amount = Random.Range(maxAmount, maxAmount + 3);
        for (var i = 0; i < amount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        var randomPositionX = Random.Range(-6f, 6f);
        var randomPositionY = Random.Range(-6f, 6f);
        //Instantiate(this.EnemyPrefab, new Vector2(randomPositionX, randomPositionY), Quaternion.identity);
        var enemy = _enemyPool.Get();
        enemy.transform.position = new Vector2(randomPositionX, randomPositionY);
    }
}
