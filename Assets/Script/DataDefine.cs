using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDefine
{
    public enum ITEMS
    {
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
        GOLD=-1,
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

}
