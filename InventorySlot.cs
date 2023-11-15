using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject itemDescription;
    public GameObject numSlot;
    public string itemName;
    public bool isFull = false;
    public Text numItem;
    public Image icon;
    public Text itemNameText;
    public Text itemDescriptionText;
    Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addItem(Item newItem)
    {
        numSlot.SetActive(true);
        item = newItem;
        itemName = item.itemName;
        icon.enabled = true;
        icon.sprite = item.icon;
        isFull = true;
    }

    

    public void incrementItem()
    {
        int number;
        bool success = int.TryParse(numItem.text, out number);
        if (success)
        {
            int newNum = number + 1;
            numItem.text = newNum.ToString();
        }
    }

    public void decrementItem()
    {
        int number;
        bool success = int.TryParse(numItem.text, out number);
        if (success)
        {
            int newNum = number - 1;
            numItem.text = newNum.ToString();
        }
        if (numItem.text == "0")
        {
            numItem.text = "1";
            numSlot.SetActive(false);
            itemDescriptionText.text = "";
            itemNameText.text = "";
            itemDescription.SetActive(false);
            itemName = "";
            isFull = false;
            icon.sprite = null;
            icon.enabled = false;
            item = null;
        }
    }

    public void selectItem()
    {
        if (itemName != "")
        {
            itemDescription.SetActive(true);
            itemNameText.text = this.itemName;
            itemDescriptionText.text = item.itemDescription;
        }
        else
        {
            itemDescription.SetActive(false);
        }
    }
}
