using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenDead : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyObj()
    {
        StartCoroutine(destroyobj());
    }
    private IEnumerator destroyobj()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
