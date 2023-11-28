using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class PartitioningGrid
{
    public PartitioningCell[,] Cells;
    private Vector2 _upperLeft, _lowerRight;

    public PartitioningGrid(int cellWidth, int cellHeight, Vector2 UpperLeft, Vector2 LowerRight)
    {
        Cells = new PartitioningCell[cellWidth, cellHeight];
        for (int i = 0; i < cellWidth; i++)
        {
            for (int j = 0; j < cellHeight; j++)
            {
                Cells[i, j] = new PartitioningCell();
            }
        }

        _upperLeft = UpperLeft;
        _lowerRight = LowerRight;
    }

    public PartitioningCell GetCellAt(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Cells.GetLength(0) || y >= Cells.GetLength(1))
            return null;
        return Cells[x, y];
    }

    //Returns {-1, -1} if location is out of bounds.
    public Vector2Int GetCellIndexAtWorldLocation(float x, float y)
    {
        if (x < _upperLeft.x || x > _lowerRight.x || y < _lowerRight.y || y > _upperLeft.y)
            return new Vector2Int(-1, -1);

        float xAlpha = Mathf.InverseLerp(_upperLeft.x, _lowerRight.x, x);
        float yAlpha = Mathf.InverseLerp(_lowerRight.y, _upperLeft.y, y);
        int xIndex = (int)(xAlpha * Cells.GetLength(0));
        int yIndex = (int)(yAlpha * Cells.GetLength(1));
        return new Vector2Int(xIndex, yIndex);
    }

    public List<T> GetObjectsInRadius<T>(int xIndex, int yIndex, int radius) where T : MonoBehaviour
    {
        if (xIndex < 0 || yIndex < 0 || xIndex > Cells.GetLength(0) || yIndex > Cells.GetLength(1))
            return new List<T>();

        List<T> objects = new List<T>();

        PartitioningCell currentCell;

        for (int i = xIndex - radius; i <= xIndex + radius; i++)
        {
            currentCell = GetCellAt(i, yIndex - radius);
            if (currentCell != null)
            {
                foreach (T content in currentCell.GetContents<T>())
                    objects.Add(content);
            }

            currentCell = GetCellAt(i, yIndex + radius);
            if (currentCell != null)
            {
                foreach (T content in currentCell.GetContents<T>())
                    objects.Add(content);
            }
        }

        for (int j = yIndex - radius + 1; j < yIndex + radius; j++)
        {
            currentCell = GetCellAt(xIndex - radius, j);
            if (currentCell != null)
            {
                foreach (T content in currentCell.GetContents<T>())
                    objects.Add(content);
            }

            currentCell = GetCellAt(xIndex + radius, j);
            if (currentCell != null)
            {
                foreach (T content in currentCell.GetContents<T>())
                    objects.Add(content);
            }
        }

        return objects;
    }

    public List<T> GetObjectsInNearestRadius<T>(int xIndex, int yIndex, int depth) where T : MonoBehaviour
    {
        List<T> objects;

        for (int i = 0; i < Cells.Length * 2; i++)
        {
            objects = GetObjectsInRadius<T>(xIndex, yIndex, i);
            if (objects.Count > 0)
            {
                for (int j = 1; j <= depth; j++)
                {
                    List<T> additionalObjects = GetObjectsInRadius<T>(xIndex, yIndex, i + j);
                    objects.AddRange(additionalObjects);
                }
                return objects;
            }
        }

        return new List<T>();
    }
}
