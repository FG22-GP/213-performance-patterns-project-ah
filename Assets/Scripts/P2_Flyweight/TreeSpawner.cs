using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

public class TreeSpawner : MonoBehaviour
{
    public Tree TreePrefab;
    private TreeSeasonColors _treeColors;
    private float _currentCooldown;
    
    const float _totalCooldown = 0.2f;

    private void Start()
    {
        LoadColorInfos();
    }

    void FixedUpdate()
    {
        this._currentCooldown -= Time.deltaTime;
        if (this._currentCooldown <= 0f)
        {
            this._currentCooldown += _totalCooldown;
            SpawnTree();
        }
    }

    void SpawnTree()
    {
        var randomPositionX = Random.Range(-6f, 6f);
        var randomPositionY = Random.Range(-6f, 6f);
        Tree tree = Instantiate(this.TreePrefab, new Vector2(randomPositionX, randomPositionY), Quaternion.identity);
        tree.SetSpawner(this);
    }
    
    void LoadColorInfos()
    {
        var fileContents = Resources.Load<TextAsset>("treeColors").text;
        this._treeColors = JsonUtility.FromJson<TreeSeasonColors>(fileContents);
    }
    
    public TreeSeasonColors GetColors() => _treeColors;
}
