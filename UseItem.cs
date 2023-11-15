using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    InventorySlot[] slots;

    public Text itemName;
    public PlayerScript player;
    public GameObject itemsParent;
    public GameObject parentOfWeapon;

    public GameObject starterSword;
    public GameObject longAxe;
    public GameObject longSword;
    public GameObject RuneSword;
    public GameObject basicHammer;
    public GameObject lantern;
    public GameObject boneSword;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void useItem()
    {
       
        if (itemName.text == "Health Potion")
        {
           
            player.currentHealth += 200;
            if (player.currentHealth > player.maxHealth)
            {
                player.currentHealth = player.maxHealth;
            }
        }
        else  if (itemName.text == "Raw Meat" || itemName.text == "Bear Claw" || itemName.text == "Gem")
        {
            return;
        }
        else if (itemName.text == "Cooked Meat")
        {
            player.currentHunger += 25;
            if (player.currentHunger > player.maxHunger)
            {
                player.currentHunger = player.maxHunger;
            }
        }
        else if (itemName.text == "Lantern")
        {
            Destroy(parentOfWeapon.transform.GetChild(0).gameObject);
            GameObject newChild = Instantiate(lantern, parentOfWeapon.transform);
            newChild.transform.localPosition = new Vector3(-0.08f, 0f, 0f);
            newChild.transform.localRotation = Quaternion.Euler(100f, 70f, 0f);
            newChild.transform.localScale = Vector3.one;
            player.hitBox = newChild.transform.Find("Hitbox").gameObject;
            player.dc.hitBox = player.hitBox;
            return;
        }
        else if (itemName.text == "Long Axe")
        {
            Destroy(parentOfWeapon.transform.GetChild(0).gameObject);
            GameObject newChild = Instantiate(longAxe, parentOfWeapon.transform);
            newChild.transform.localPosition = new Vector3(-0.08f, 0f, 0f);
            newChild.transform.localRotation = Quaternion.Euler(100f, 0f, 0f);
            newChild.transform.localScale = Vector3.one;
            player.hitBox = newChild.transform.Find("Hitbox").gameObject;
            player.dc.hitBox = player.hitBox;
            return;
        }
        else if (itemName.text == "Rune Sword")
        {
            Destroy(parentOfWeapon.transform.GetChild(0).gameObject);
            GameObject newChild = Instantiate(RuneSword, parentOfWeapon.transform);
            newChild.transform.localPosition = new Vector3(-0.08f, 0f, 0f);
            newChild.transform.localRotation = Quaternion.Euler(100f, 0f, 0f);
            newChild.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            player.hitBox = newChild.transform.Find("Hitbox").gameObject;
            player.dc.hitBox = player.hitBox;
            return;
        }
        else if (itemName.text == "Starter Sword")
        {
            Destroy(parentOfWeapon.transform.GetChild(0).gameObject);
            GameObject newChild = Instantiate(starterSword, parentOfWeapon.transform);
            newChild.transform.localPosition = Vector3.zero;
            newChild.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            newChild.transform.localScale = Vector3.one;
            player.hitBox = newChild.transform.Find("Hitbox").gameObject;
            player.dc.hitBox = player.hitBox;
            return;
        }
        else if (itemName.text == "Long Sword")
        {
            Destroy(parentOfWeapon.transform.GetChild(0).gameObject);
            GameObject newChild = Instantiate(longSword, parentOfWeapon.transform);
            newChild.transform.localPosition = new Vector3(-0.08f, 0f, 0f);
            newChild.transform.localRotation = Quaternion.Euler(100f, 0f, 0f);
            newChild.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            player.hitBox = newChild.transform.Find("Hitbox").gameObject;
            player.dc.hitBox = player.hitBox;
            return;
        }
        else if (itemName.text == "Basic Hammer")
        {
            Destroy(parentOfWeapon.transform.GetChild(0).gameObject);
            GameObject newChild = Instantiate(basicHammer, parentOfWeapon.transform);
            newChild.transform.localPosition = new Vector3(-0.08f, 0.05f, -0.1f);
            newChild.transform.localRotation = Quaternion.Euler(100f, 0f, 0f);
            newChild.transform.localScale = Vector3.one;
            player.hitBox = newChild.transform.Find("Hitbox").gameObject;
            player.dc.hitBox = player.hitBox;
            return;
        }
        else if (itemName.text == "Bone Sword")
        {
            Destroy(parentOfWeapon.transform.GetChild(0).gameObject);
            GameObject newChild = Instantiate(boneSword, parentOfWeapon.transform);
            newChild.transform.localPosition = new Vector3(-0.08f, 0f, 0f);
            newChild.transform.localRotation = Quaternion.Euler(100f, 0f, 0f);
            newChild.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            player.hitBox = newChild.transform.Find("Hitbox").gameObject;
            player.dc.hitBox = player.hitBox;
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemName == itemName.text)
            {
                slots[i].decrementItem();
            }
        }
    }
}
