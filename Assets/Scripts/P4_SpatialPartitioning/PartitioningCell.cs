using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitioningCell
{
    public List<MonoBehaviour> Contents;

    public PartitioningCell()
    {
        Contents = new List<MonoBehaviour>();
    }

    public List<T> GetContents<T>() where T : MonoBehaviour
    {
        List<T> contents = new List<T>();
        foreach (var item in Contents)
        {
            if (item is T)
                contents.Add((T)item);
        }
        return contents;
    }
}
