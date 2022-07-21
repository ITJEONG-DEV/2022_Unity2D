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

}
