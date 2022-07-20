using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDefine
{
    public enum CROPS
    {
        CORN = 0,
        BEET,
        TURNIP,
        CARROT,
        STRAWBERRY,
        PEANUT,
        APPLE_RED,
        APPLE_GREEN
    };

    public Dictionary<int, int> MouseOver = new Dictionary<int, int>();

}
