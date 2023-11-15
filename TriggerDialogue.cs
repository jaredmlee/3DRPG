using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerDialogue : MonoBehaviour
{
    public CinemachineFreeLook freeLook;
    public GameObject buyButton;
    public DialogueManager manager;
    public DialogueTrigger trigger;
    public bool oneTimeEvent;
    public bool pauseGame;
    public bool endOnExit;
    public bool isShopItem;
    public bool stopCameraMovement;

    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        freeLook = GameObject.FindGameObjectWithTag("FreeLook").GetComponent<CinemachineFreeLook>();
        manager = FindAnyObjectByType<DialogueManager>();
        buyButton = GameObject.FindGameObjectWithTag("BuyButton");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isShopItem && manager.endedDialogue)
        {
            buyButton.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stopCameraMovement)
        {
            freeLook.gameObject.SetActive(false);
        }
        if (isShopItem)
        {
            buyButton.SetActive(true);
        }
        else if (buyButton != null)
        {
            buyButton.SetActive(false);
        }
        if (pauseGame)
        {
            Time.timeScale = 0f;
        }
        if (oneTimeEvent && triggered)
        {
            return;
        }
        if (other.gameObject.name == "Player")
        {
            trigger.TriggerDialogue();
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && endOnExit)
        {
            manager.animator.SetBool("isOpen", false);
            manager.endedDialogue = true;
        }
        freeLook.gameObject.SetActive(true);
    }
}
