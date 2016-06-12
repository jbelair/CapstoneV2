using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    // Use this for initialization
    void Awake()
    {
        //this is how to create new items.
        // Name, ID (Go in order that makes sense, all metals together, weapons together etc), Description, 
        // Random values (Ignore these for now), and type of item that it is. 
        items.Add(new Item("Copper", 0, "A Chunk of Copper", 0, 0, Item.ItemType.Resource));
        items.Add(new Item("Iron", 1, "A Chunk of Iron", 0, 0, Item.ItemType.Resource));
        items.Add(new Item("Tin", 2, "A Chunk of Tin", 0, 0, Item.ItemType.Consumable));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
