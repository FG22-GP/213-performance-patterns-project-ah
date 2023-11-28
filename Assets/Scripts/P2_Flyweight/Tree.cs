using UnityEngine;

public class Tree : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private TreeSpawner _spawner;
    // private TreeSeasonColors _treeColors;
    private int _tick;
    private int _index;
    
    void Start()
    {
        this._spriteRenderer = GetComponent<SpriteRenderer>();
        // LoadColorInfos();
        UpdateSeason();
    }
    
    void FixedUpdate()
    {
        UpdateSeason();
    }

    /// <summary>
    /// In the Tree Colors, the Artist assigned very specific values for the seasoning tree.
    /// Each tree needs to access their colors depending on how old they are.
    /// Unfortunately, this solution uses up a lot of Memory :(
    /// </summary>

    // void LoadColorInfos()
    // {
    //     var fileContents = Resources.Load<TextAsset>("treeColors").text;
    //     this._treeColors = JsonUtility.FromJson<TreeSeasonColors>(fileContents);
    // }

    void UpdateSeason()
    {
        _index = _spawner.GetColors().MoveNext(_index);
        this._spriteRenderer.color = _spawner.GetColors().CurrentColor(_index);
    }

    public void SetSpawner(TreeSpawner spawner) => _spawner = spawner;
}
