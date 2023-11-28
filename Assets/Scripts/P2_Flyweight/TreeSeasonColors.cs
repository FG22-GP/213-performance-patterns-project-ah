using System;
using UnityEngine;

[Serializable]
public class TreeSeasonColors
{
    [SerializeField] private ColorInfo[] colors;
    
    /// <summary>
    /// This returns the current color. The value changes every time
    /// `MoveNext` is invoked.
    /// </summary>
    public Color CurrentColor(int index)
    {

            var colorInfo = colors[index];
            return new Color(colorInfo.r / 255.0f, colorInfo.g / 255.0f, colorInfo.b / 255.0f, 1f);
    }

    /// <summary>
    /// This makes the Tree move on to the next color
    /// </summary>
    public int MoveNext(int index)
    {
        index += 10;
        index %= colors.Length;
        return index;
    }

    private int _index;
}