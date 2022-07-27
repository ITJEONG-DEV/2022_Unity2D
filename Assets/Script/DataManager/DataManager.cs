using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target) { this.target = target; }
}

[Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;

    [SerializeField]
    List<TValue> values;

    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }
 
    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for(var i=0; i<count; ++i)
            target.Add(keys[i], values[i]);
    }
}


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
        // save time
        var startTime = data.time;
        data.time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        // play time
        var differ = Convert.ToDateTime(data.time) - Convert.ToDateTime(startTime);
        var playTime = TimeSpan.Parse(data.playTime);
        playTime += differ;

        data.playTime = playTime.ToString();

        //Debug.Log($"playTime: {playTime}, data.playTime: {data.playTime}");

        // 저장 시간
        var timeData = JsonUtility.ToJson(data);

        // 아이템 정보 + 골드
        var itemData = JsonUtility.ToJson(items);

        // 농작물 정보
        var currentCropsList = cropManager.GetCurrentCropsList();
        var currentCropsStateList = cropManager.GetCurrentCropsStateList();
        
        //foreach (var key in currentCropsStateList.Keys)
        //    if (!currentCropsList.ContainsKey(key))
        //        currentCropsStateList.Remove(key);
        //var currentCropData = JsonUtility.ToJson(new Serialization<(float, float), DataDefine.CROPS>(currentCropsList));
        //var currentCropState = JsonUtility.ToJson(new Serialization<(float, float), DataDefine.GROWING_STATE>(currentCropsStateList));

        List<string>[] currentCrops = new List<string>[currentCropsList.Count];
        int i = 0;
        foreach(var key in currentCropsList.Keys)
        {
            currentCrops[i] = new List<string>();
            string x = key.Item1.ToString();
            string y = key.Item2.ToString();
            string crops = ((int)currentCropsList[key]).ToString();
            string state = ((int)currentCropsStateList[key]).ToString();

            currentCrops[i].Add(x);
            currentCrops[i].Add(y);
            currentCrops[i].Add(crops);
            currentCrops[i].Add(state);

            i++;
        }

        var currentCropsData = "{";
        for(i=0; i<currentCrops.Length; i++)
        {
            currentCropsData += JsonUtility.ToJson(new Serialization<string>(currentCrops[i]));
            if (i == currentCrops.Length - 1)
                currentCropsData += "}";
            else
                currentCropsData += ", ";
        }

        var str = $"{timeData}, {itemData}, {currentCropsData}";
        Debug.Log(str);

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
    public string playTime;

    public Data()
    {
        items = new Items();

        items.corn_seed = 5;
        items.turnip_seed = 5;
        items.carrot_seed = 5;
        items.strawberry_seed = 5;

        time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        playTime = TimeSpan.Zero.ToString();
        Debug.Log($"time: {time}");
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
