using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    InventorySlot[] slots;

    public ChangeCoins changeCoins;
    public GameObject itemsParent;
    public GameObject inventoryMenu;
    public GameObject youGotMessage;
    public Text newItem;
    public static bool inventoryOnScreen = false;
    // Start is called before the first frame update
    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (inventoryOnScreen)
            {
                resume();
            }
            else
            {
                openInventory();
            }
        }
    }
    public void add(Item item)
    {
        if (item.itemName == "coins")
        {
            StartCoroutine(showMessage(item));
            changeCoins.changeNum(item.num);
            return;
        }
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log(slots[i].GetComponent<InventorySlot>().itemName);
            if (!slots[i].GetComponent<InventorySlot>().isFull)
            {
                StartCoroutine(showMessage(item));
                slots[i].GetComponent<InventorySlot>().addItem(item);
                break;
            }
            else if (slots[i].GetComponent<InventorySlot>().itemName == item.itemName)
            {
                StartCoroutine(showMessage(item));
                slots[i].GetComponent<InventorySlot>().incrementItem();
                break;
            }
        }
    }

    public void resume()
    {
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        inventoryOnScreen = false;
    }
    public void openInventory()
    {
        inventoryMenu.SetActive(true);
        youGotMessage.SetActive(false);
        Debug.Log("why you no work");
        Time.timeScale = 0f;
        inventoryOnScreen = true;
    }

    private IEnumerator showMessage(Item item)
    {
        youGotMessage.SetActive(true);
        newItem.text = item.num.ToString() + " " + item.itemName;
        yield return new WaitForSeconds(1.5f);
        youGotMessage.SetActive(false);
    }

   
}
