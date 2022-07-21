using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    Data data;
    Items items;
    string path;

    private void Start()
    {
        Data data = new Data();
        items = data.items;

        path = Application.dataPath + "/data.json";

        Debug.Log($"path: {path}");
    }

    void Load()
    {
        string str = File.ReadAllText(path);
        JsonUtility.FromJson<Data>(str);
    }

    void Save()
    {
        data.time = System.DateTime.Now.ToString();
        File.WriteAllText(path, JsonUtility.ToJson(data));
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

    public bool Use(DataDefine.ITEMS item, int num)
    {
        switch (item)
        {
            case DataDefine.ITEMS.GOLD:
                if (items.gold + num > 0)
                {
                    items.gold += num;
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.CORN:
                if (items.corn + num > 0)
                {
                    items.corn += num;
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.CORN_SEED:
                if (items.corn_seed + num > 0)
                {
                    items.corn_seed += num;
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.TURNIP:
                if (items.turnip + num > 0)
                {
                    items.turnip += num;
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.TURNIP_SEED:
                if (items.turnip_seed + num > 0)
                {
                    items.turnip_seed += num;
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.CARROT:
                if (items.carrot + num > 0)
                {
                    items.carrot += num;
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.CARROT_SEED:
                if (items.carrot_seed + num > 0)
                {
                    items.carrot_seed += num;
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.STRAWBERRY:
                if (items.strawberry + num > 0)
                {
                    items.strawberry += num;
                    return true;
                }
                else return false;
            case DataDefine.ITEMS.STRAWBERRY_SEED:
                if (items.strawberry_seed + num > 0)
                {
                    items.strawberry_seed += num;
                    return true;
                }
                else return false;
            default:
                return false;
        }
    }
}

public class Data
{
    public Items items;
    public string time;

    public Data()
    {
        items = new Items();
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
