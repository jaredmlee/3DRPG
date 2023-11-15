using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class Chest : MonoBehaviour
{
    public bool triggered = false;
    public bool opened = false;
    public string uniqueName;
    public Animator animator;
    public GameObject loot1;
    public GameObject loot2;
    public GameObject loot3;
    public GameObject player;
    public QuickSaveWriter quick;
    // Start is called before the first frame update
    void Start()
    {
        quick = QuickSaveWriter.Create(uniqueName);
        player = GameObject.FindGameObjectWithTag("Player");
        //uncomment to make chests save their state
        //loadChest();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered && Input.GetKeyDown(KeyCode.E) && !opened)
        {

            opened = true;
            animator.SetBool("isOpened", true);
            saveChest();
            Instantiate(loot1, player.transform.position, player.transform.rotation);
            if (loot2 != null)
            {
                StartCoroutine(giveloot2());
            }
            if (loot3 != null)
            {
                StartCoroutine(giveloot3());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            triggered = false;
        }
    }
    private IEnumerator giveloot2()
    {
        yield return  new WaitForSeconds(2f);
        Instantiate(loot2, player.transform.position, player.transform.rotation);
    }
    private IEnumerator giveloot3()
    {
        yield return new WaitForSeconds(4f);
        Instantiate(loot3, player.transform.position, player.transform.rotation);
    }

    private void saveChest()
    {
        quick.Write("opened", true)
             .Commit();

    }
    private void loadChest()
    {
        QuickSaveReader qr = QuickSaveReader.Create(uniqueName);
        if (qr.Exists("opened"))
        {
            qr.Read<bool>("opened", (r) => { opened = r; });
            Vector3 newRotation = new Vector3(70f, 0f, 0f);
            animator.SetBool("isOpened", true);
        }
    }
}
