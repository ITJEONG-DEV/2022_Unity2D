using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDefine
{
    public enum ITEMS
    {
        NONE = -1,
        GOLD = 0,
        CORN,
        CORN_SEED,
        TURNIP,
        TURNIP_SEED,
        CARROT,
        CARROT_SEED,
        STRAWBERRY,
        STRAWBERRY_SEED,
        SICKLE,
        WATERING_CAN
    }
    public enum ICONS
    {
        GOLD = -1,
        DEFAULT = 0,
        CORN,
        CORN_SEED,
        TURNIP,
        TURNIP_SEED,
        CARROT,
        CARROT_SEED,
        STRAWBERRY,
        STRAWBERRY_SEED,
        SICKLE,
        WATERING_CAN
    }

    public enum CROPS
    {
        DEFAULT = 0,
        CORN,
        TURNIP,
        CARROT,
        STRAWBERRY
    }

    public enum GROWING_STATE
    {
        none = -1,
        first,
        second,
        third,
        finish,
        gather
    }

    public Dictionary<ITEMS, (int, int)> PRICE = new Dictionary<ITEMS, (int, int)>();
    public Dictionary<ITEMS, Sprite> ICON = new Dictionary<ITEMS, Sprite>();

    public DataDefine()
    {
        PRICE.Add(ITEMS.CORN, (5, 4));
        PRICE.Add(ITEMS.TURNIP, (5, 4));
        PRICE.Add(ITEMS.CARROT, (5, 4));
        PRICE.Add(ITEMS.STRAWBERRY, (5, 4));

        PRICE.Add(ITEMS.CORN_SEED, (3, 2));
        PRICE.Add(ITEMS.TURNIP_SEED, (3, 2));
        PRICE.Add(ITEMS.CARROT_SEED, (3, 2));
        PRICE.Add(ITEMS.STRAWBERRY_SEED, (3, 2));

        var spriteList = Resources.LoadAll<Sprite>("sprites");

        for (int i = 0; i < spriteList.Length; i++)
        {
            var item = GetItem(spriteList[i].name);

            ICON.Add(item, spriteList[i]);
        }
    }

    public static ITEMS GetItemInfo(CROPS crops)
    {
        switch(crops)
        {
            case CROPS.CORN:
                return ITEMS.CORN;
            case CROPS.TURNIP:
                return ITEMS.TURNIP;
            case CROPS.CARROT:
                return ITEMS.CARROT;
            case CROPS.STRAWBERRY:
                return ITEMS.STRAWBERRY;
        }

        return ITEMS.GOLD;
    }

    public static ITEMS GetSeedInfo(CROPS crops)
    {
        switch (crops)
        {
            case CROPS.CORN:
                return ITEMS.CORN_SEED;
            case CROPS.TURNIP:
                return ITEMS.TURNIP_SEED;
            case CROPS.CARROT:
                return ITEMS.CARROT_SEED;
            case CROPS.STRAWBERRY:
                return ITEMS.STRAWBERRY_SEED;
        }

        return ITEMS.GOLD;

    }

    public static ITEMS GetItem(string name)
    {
        foreach (ITEMS item in new ITEMS[] {
            ITEMS.GOLD, ITEMS.CORN, ITEMS.CORN_SEED, ITEMS.TURNIP, ITEMS.TURNIP_SEED,
            ITEMS.CARROT, ITEMS.CARROT_SEED, ITEMS.STRAWBERRY, ITEMS.STRAWBERRY_SEED,
            ITEMS.SICKLE, ITEMS.WATERING_CAN })
            if (item.ToString() == name)
                return item;

        return ITEMS.NONE;
    }

    public static ICONS GetIconInfo(ITEMS item)
    {
        switch (item)
        {
            case ITEMS.CORN:
                return ICONS.CORN;
            case ITEMS.CORN_SEED:
                return ICONS.CORN_SEED;
            case ITEMS.TURNIP:
                return ICONS.TURNIP;
            case ITEMS.TURNIP_SEED:
                return ICONS.TURNIP_SEED;
            case ITEMS.CARROT:
                return ICONS.CARROT;
            case ITEMS.CARROT_SEED:
                return ICONS.CARROT_SEED;
            case ITEMS.STRAWBERRY:
               return ICONS.STRAWBERRY;
            case ITEMS.STRAWBERRY_SEED:
                return ICONS.STRAWBERRY_SEED;
            case ITEMS.SICKLE:
                return ICONS.SICKLE;
            case ITEMS.WATERING_CAN:
                return ICONS.WATERING_CAN;
        }

        return ICONS.DEFAULT;
    }

    public (int, int) GetPrice(ITEMS item)
    {
        if(PRICE.ContainsKey(item))
            return PRICE[item];

        return (-1, -1);
    }

    public Sprite GetSprite(ITEMS item)
    {
        if (ICON.ContainsKey(item))
            return ICON[item];

        return null;
    }
}
