using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCoins : MonoBehaviour
{
    public Text coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeNum(int num)
    {
        int number;
        bool success = int.TryParse(coins.text, out number);
        if (success)
        {
            int newNum = number + num;
            coins.text = newNum.ToString();
        }
    }
}
