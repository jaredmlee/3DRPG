using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyItems : MonoBehaviour
{
    InventorySlot[] slots;

    public GameObject itemsParent;
    public Text coinNum;
    public ChangeCoins changeCoins;
    public Text numCoins;
    public Transform player;
    public Text Name;

    public GameObject healthPotion;
    public GameObject cookedMeat;
    public GameObject longAxe;
    public GameObject longSword;
    public GameObject basicHammer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        coinNum.text = numCoins.text;
    }
    public void buyItem()
    {
        int coins = int.Parse(numCoins.text);
        if (Name.text == "Health Potion" && coins >= 40)
        {
            changeCoins.changeNum(-40);
            Instantiate(healthPotion, player.position, player.rotation);
        }
        else if (Name.text == "Cooked Meat" && coins >= 10)
        {
            changeCoins.changeNum(-10);
            Instantiate(cookedMeat, player.position, player.rotation);
        }
        else if (Name.text == "Long Axe" && coins >= 120)
        {
            changeCoins.changeNum(-120);
            Instantiate(longAxe, player.position, player.rotation);
        }
        else if (Name.text == "Long Sword" && coins >= 110)
        {
            changeCoins.changeNum(-110);
            Instantiate(longSword, player.position, player.rotation);
        }
        else if (Name.text == "Basic Hammer" && coins >= 120)
        {
            changeCoins.changeNum(-120);
            Instantiate(basicHammer, player.position, player.rotation);
        }
        else if (Name.text == "Apprentice" && coins >= 2)
        {
            bool hasRawMeat = false;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].itemName == "Raw Meat")
                {
                    slots[i].decrementItem();
                    hasRawMeat = true;
                }
            }
            if (hasRawMeat)
            {
                changeCoins.changeNum(-2);
                Instantiate(cookedMeat, player.position, player.rotation);
            }
        }
    }
}
