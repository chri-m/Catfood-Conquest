using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountScript1 : MonoBehaviour
{
    public int coinCount;
    public Text coinCounttext;

    // Start is called before the first frame update
    void Start()
    {
        LoadCat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCat()
    {
        CatDataScript data = SaveScript.LoadCat();
        if (data != null)
        {
            coinCount = data.coinCount;
        }
        else
        {
            coinCount = 0;
        }
        coinCounttext.text = coinCount.ToString();

    }
}
