using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawningManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> lootSpots = new List<Transform>();

    [SerializeField]
    private GameObject lootPrefab;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            lootSpots.Add(transform.GetChild(i));
        }

        foreach(Transform lootSpot in lootSpots)
        {
            GameObject loot = Instantiate(lootPrefab, lootSpot);
            loot.GetComponent<LootData>().playerName = "Map";
            loot.GetComponent<LootData>().LootSize = 1;
        }
    }
}
