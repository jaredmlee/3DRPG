using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeAmbienceBackUp2 : MonoBehaviour
{
    public EnterCave enterCave;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !enterCave.inCave)
        {
            enterCave.change();
        }
    }
}
