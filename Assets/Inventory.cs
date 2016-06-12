using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    private ItemDatabase database;

    public int slotsX;
    public int slotsY;
    public List<Item> slots = new List<Item>();

    public bool showInventory;

    private bool showToolTip;
    private string toolTip;

    private bool draggingItem;
    private Item draggedItem;

    private int prevIndex;


    public GUISkin skin;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());

        }

        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();

        //starting items in inventory//
        //inventory[0] = database.items[0];
        // this is how you add items to the list. this will be done within an objects pick up function?
        //AddItem(2);

        inventory[0] = database.items[0];
        inventory[1] = database.items[1];
        inventory[2] = database.items[1];
        inventory[3] = database.items[2];
        inventory[4] = database.items[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
    }

    void OnGUI()
    {
        toolTip = "";
        GUI.skin = skin;
        if (showInventory)
        {
            Debug.Log(skin.GetStyle("Slot"));
            DrawInventory();
            if (showToolTip)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 20, Event.current.mousePosition.y, 200, 200), toolTip, skin.GetStyle("ToolTip"));
            }
        }
        if (draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
        }

    }

    void DrawInventory()
    {

        Event e = Event.current;
        int index = 0;
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {

                Rect slotRect = new Rect(x * 48, y * 48, 40, 40);
                GUI.Box(slotRect, "", skin.GetStyle("Slot"));

                slots[index] = inventory[index];

                if (slots[index].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[index].itemIcon);

                    if (slotRect.Contains(e.mousePosition))
                    {

                        if (e.isMouse && e.type == EventType.mouseDown && e.button == 1)
                        {
                            if (slots[index].itemType == Item.ItemType.Consumable)
                            {
                                UseConsumable(slots[index], index, true);
                                ////////////////////////////////////////////////////
                            }
                        }
                        toolTip = CreateToolTip(slots[index]);
                        showToolTip = true;

                        if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = index;
                            draggedItem = slots[index];
                            inventory[index] = new Item();

                        }
                        if (e.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[prevIndex] = inventory[index];
                            inventory[index] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }


                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.mouseUp && draggingItem)
                        {
                            inventory[index] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }
                if (toolTip == "")
                {
                    showToolTip = false;
                }
                index++;
            }

        }
    }

    private void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch (item.itemID)
        {
            case 2:
                // player skill that the consumable is used for(ID), amount its being increased by(int), time its increased for
                break;
            default:
                break;
        }

        if (deleteItem)
        {
            inventory[slot] = new Item();
        }
    }

    string CreateToolTip(Item item)
    {
        toolTip = "<color=#ffffff>" + item.itemName + "</color>\n\n" + item.itemDescription;

        return toolTip;
    }

    // Adding an Item to the inventory //
    public void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == null)
            {
                for (int j = 0; j < database.items.Count; j++)
                {
                    if (database.items[j].itemID == id)
                    {
                        inventory[i] = database.items[j];
                    }
                }
                break;
            }
        }
    }

    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    // Checking the Inventory for a specific item //
    bool InventoryContains(int id)
    {

        foreach (Item item in inventory)
        {
            if (item.itemID == id)
            {
                return true;
            }
        }
        return false;
    }
}