using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    Text text;
    DataDefine.ICONS icon;

    void Start()
    {
        text = gameObject.transform.Find("NUM").GetComponent<Text>();
    }

    void Update()
    {
        
    }

    public void SetImage(Sprite sprite)
    {
        transform.Find("IMAGE").GetComponent<Image>().sprite = sprite;
    }

    public void SetNumText(int num)
    {
        transform.Find("NUM").GetComponent<Text>().text = num.ToString();
    }

    public void SetPriceText(string type, int price)
    {
        if (type == "PRICE_BUY")
            transform.Find("PRICE_BUY").GetComponent<Text>().text = price.ToString();
        else if(type == "PRICE_SELL")
            transform.Find("PRICE_SELL").GetComponent<Text>().text = price.ToString();
    }

    public void SetIcon(DataDefine.ICONS icon)
    {
        this.icon = icon;
    }

    public void RenewCountText(DataDefine.ICONS icon, int num)
    {
        if (this.icon == icon)
            text.text = num.ToString();
    }
}
