using UnityEngine;
using System.Collections;
[System.Serializable]
public class Item
{

    public string itemName;
    public int itemID;
    public string itemDescription;
    public Texture2D itemIcon;
    public int itemPower;
    public int itemSpeed;
    public ItemType itemType;

    public enum ItemType
    {

        Weapon,
        Armour,
        Consumable,
        Resource,
        Quest,

    }

    public Item(string name, int id, string description, int power, int speed, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDescription = description;
        itemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
        itemPower = power;
        itemSpeed = speed;
        itemType = type;
    }

    public Item() { }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
