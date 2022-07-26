using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static Data data;
    static Items items;
    static string path;

    static CropManager cropManager;

    public delegate void itemChanged(DataDefine.ICONS icon, int num);

    itemChanged itemChangedEventHandler;

    private void Start()
    {
        cropManager = GetComponent<CropManager>();

        data = new Data();
        items = data.items;

        path = Application.dataPath + "/data.json";

        Debug.Log($"path: {path}");

        Invoke("Load", 0.1f);
    }

    void Load()
    {
        try
        {
            string str = File.ReadAllText(path);
            JsonUtility.FromJson<Data>(str);
        }catch(Exception e)
        {
            Debug.Log(e.Message);
        }

        Debug.Log("Load()");
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.GOLD, items.gold);
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.CORN, items.corn);
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.CORN_SEED, items.corn_seed);
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.TURNIP, items.turnip);
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.TURNIP_SEED, items.turnip_seed);
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.CARROT, items.carrot);
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.CARROT_SEED, items.carrot_seed);
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.STRAWBERRY, items.strawberry);
        itemChangedEventHandler?.Invoke(DataDefine.ICONS.STRAWBERRY_SEED, items.strawberry_seed);
    }

    public void Save()
    {
        data.time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        // 저장 시간
        var timeData = JsonUtility.ToJson(data);

        // 아이템 정보 + 골드
        var itemData = JsonUtility.ToJson(items);

        // 농작물 정보
        var currentCropsList = cropManager.GetCurrentCropsList();
        var currentCropsStateList = cropManager.GetCurrentCropsStateList();
        
        foreach (var key in currentCropsStateList.Keys)
            if (!currentCropsList.ContainsKey(key))
                currentCropsStateList.Remove(key);

        var currentCropData = JsonUtility.ToJson(currentCropsList);
        var currentCropState = JsonUtility.ToJson(currentCropsStateList);

        var str = $"{timeData}, {itemData}, {currentCropData}, {currentCropState}";

        File.WriteAllText(path, str);
    }

    public int Get(DataDefine.ITEMS item)
    {
        switch(item)
        {
            case DataDefine.ITEMS.GOLD:
                return items.gold;
            case DataDefine.ITEMS.CORN:
                return items.corn;
            case DataDefine.ITEMS.CORN_SEED:
                return items.corn_seed;
            case DataDefine.ITEMS.TURNIP:
                return items.turnip;
            case DataDefine.ITEMS.TURNIP_SEED:
                return items.turnip_seed;
            case DataDefine.ITEMS.CARROT:
                return items.carrot;
            case DataDefine.ITEMS.CARROT_SEED:
                return items.carrot_seed;
            case DataDefine.ITEMS.STRAWBERRY:
                return items.strawberry;
            case DataDefine.ITEMS.STRAWBERRY_SEED:
                return items.strawberry_seed;
            default:
                return -1;
        }
    }

    public bool Add(DataDefine.ITEMS item, int num)
    {
        switch (item)
        {
            case DataDefine.ITEMS.GOLD:
                if (items.gold + num >= 0)
                {
                    items.gold += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.GOLD, items.gold);
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.CORN:
                if (items.corn + num >= 0)
                {
                    items.corn += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.CORN, items.corn);
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.CORN_SEED:
                if (items.corn_seed + num >= 0)
                {
                    items.corn_seed += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.CORN_SEED, items.corn_seed);
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.TURNIP:
                if (items.turnip + num >= 0)
                {
                    items.turnip += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.TURNIP, items.turnip);
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.TURNIP_SEED:
                if (items.turnip_seed + num >= 0)
                {
                    items.turnip_seed += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.TURNIP_SEED, items.turnip_seed);
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.CARROT:
                if (items.carrot + num >= 0)
                {
                    items.carrot += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.CARROT, items.carrot);
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.CARROT_SEED:
                if (items.carrot_seed + num >= 0)
                {
                    items.carrot_seed += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.CARROT_SEED, items.carrot_seed);
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.STRAWBERRY:
                if (items.strawberry + num >= 0)
                {
                    items.strawberry += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.STRAWBERRY, items.strawberry);
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.STRAWBERRY_SEED:
                if (items.strawberry_seed + num >= 0)
                {
                    items.strawberry_seed += num;
                    itemChangedEventHandler?.Invoke(DataDefine.ICONS.STRAWBERRY_SEED, items.strawberry_seed);
                    return true;
                }
                else return false;
            default:
                return false;
        }


    }

    public void AddItemChangedEventHandler(itemChanged handler)
    {
        itemChangedEventHandler += handler;
    }
}

public class Data
{
    public Items items;
    public string time;

    public Data()
    {
        items = new Items();

        items.corn_seed = 5;
        items.turnip_seed = 5;
        items.carrot_seed = 5;
        items.strawberry_seed = 5;

        time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }
}

public class Items
{
    public int gold;
    public int corn;
    public int turnip;
    public int carrot;
    public int strawberry;
    public int corn_seed;
    public int turnip_seed;
    public int carrot_seed;
    public int strawberry_seed;
}
