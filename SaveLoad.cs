using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class SaveLoad : MonoBehaviour
{
    public GameObject chest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void save()
    {
        QuickSaveWriter.Create("Chest")
                       .Write("opened", true)
                       .Commit();
    }
}
