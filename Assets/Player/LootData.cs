using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootData : MonoBehaviour
{
    [SerializeField]
    float lootSizeDivisor = 15f;

    [SerializeField]
    float defaultSize = 0.5f;

    public string playerName;

    private int lootSize = 1;
    public int LootSize
    {
        get { return lootSize; }
        set
        {
            lootSize = value;
            float scale = defaultSize + lootSize / lootSizeDivisor;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
