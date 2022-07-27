using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public GameObject content;
    public GameObject shopItemPrefab;
    public static DataDefine dataDefine;
    
    public static DataManager dataManager;
    public static MainUI mainUI;

    public GameObject gold;

    static bool isCreated = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Initialize", 0.2f);
    }

    void Initialize()
    {

        mainUI = GameObject.Find("MainUI").GetComponent<MainUI>();
        dataManager = GameObject.Find("MainUI").GetComponent<DataManager>();

        if (!isCreated)
        {
            dataDefine = new DataDefine();

            foreach (DataDefine.ITEMS item in new DataDefine.ITEMS[] {
                DataDefine.ITEMS.CORN, DataDefine.ITEMS.CORN_SEED, DataDefine.ITEMS.TURNIP, DataDefine.ITEMS.TURNIP_SEED,
                DataDefine.ITEMS.CARROT, DataDefine.ITEMS.CARROT_SEED, DataDefine.ITEMS.STRAWBERRY,
                DataDefine.ITEMS.STRAWBERRY_SEED })
            {
                AddItemList(item);
                Debug.Log(item);
            }
            isCreated = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void AddItemList(DataDefine.ITEMS item)
    {
        var obj = Instantiate(shopItemPrefab);
        obj.name = item.ToString();
        obj.transform.SetParent(content.transform);

        var shopItem = obj.GetComponent<ShopItem>();
        
        shopItem.SetIcon(DataDefine.GetIconInfo(item));
        mainUI.AddItemChangedEventHandler(shopItem.RenewCountText);

        // image
        shopItem.SetImage(dataDefine.GetSprite(item));

        // num
        shopItem.SetNumText(dataManager.Get(item));

        // price text
        var price = dataDefine.GetPrice(item);
        shopItem.SetPriceText("PRICE_BUY", price.Item1);
        shopItem.SetPriceText("PRICE_SELL", price.Item2);

        // button event
        obj.transform.Find("BUY_BUTTON").GetComponent<Button>().onClick.AddListener(delegate { OnClickBuyButton(obj); });
        obj.transform.Find("SELL_BUTTON").GetComponent<Button>().onClick.AddListener(delegate { OnClickSellButton(obj); });
    }
    
    void OnClickBuyButton(GameObject obj)
    {
        var name = obj.name;
        var item = DataDefine.GetItem(name);
        var price_buy = int.Parse(obj.transform.Find("PRICE_BUY").GetComponent<Text>().text);

        if(dataManager.Add(DataDefine.ITEMS.GOLD, -price_buy))
        {
            dataManager.Add(item, 1);
            obj.transform.Find("NUM").GetComponent<Text>().text = dataManager.Get(item).ToString();
            gold.GetComponent<Text>().text = $"{dataManager.Get(DataDefine.ITEMS.GOLD)} G";

            Debug.Log($"Buy {item}, Use {price_buy} G. current: {dataManager.Get(item)}, {dataManager.Get(DataDefine.ITEMS.GOLD)} G");
        }
    }

    void OnClickSellButton(GameObject obj)
    {
        var name = obj.name;
        var item = DataDefine.GetItem(name);
        var price_sell = int.Parse(obj.transform.Find("PRICE_SELL").GetComponent<Text>().text);

        int num = 1;
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            num = dataManager.Get(item);
        
        if (dataManager.Add(item, -num))
        {
            dataManager.Add(DataDefine.ITEMS.GOLD, price_sell);
            obj.transform.Find("NUM").GetComponent<Text>().text = dataManager.Get(item).ToString();
            gold.GetComponent<Text>().text = $"{dataManager.Get(DataDefine.ITEMS.GOLD)} G";

            //Debug.Log($"Buy {item}, Get {price_sell} G. current: {dataManager.Get(item)}, {dataManager.Get(DataDefine.ITEMS.GOLD)} G");
        }
    }
}
