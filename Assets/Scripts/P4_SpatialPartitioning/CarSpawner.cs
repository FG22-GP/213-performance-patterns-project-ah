using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    public Car CarPrefab;
    private float _currentCooldown;
    
    const float _totalCooldown = 0.2f;

    public PartitioningGrid Grid;

    private void Start()
    {
        Grid = new PartitioningGrid(60, 60, new Vector2(-60f, 60f), new Vector2(60f, -60f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this._currentCooldown -= Time.deltaTime;
        if (this._currentCooldown <= 0f)
        {
            this._currentCooldown += _totalCooldown;
            SpawnCar();
        }
    }

    void SpawnCar()
    {
        var randomPositionX = Random.Range(-60f, 60f);
        var randomPositionY = Random.Range(-60f, 60f);
        Car SpawnedCar = Instantiate(this.CarPrefab, new Vector2(randomPositionX, randomPositionY), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Vector2Int GridIndices = Grid.GetCellCoordinateAtWorldLocation(randomPositionX, randomPositionY);
        Grid.Cells[GridIndices.x, GridIndices.y].Contents.Add(SpawnedCar);
    }
}
