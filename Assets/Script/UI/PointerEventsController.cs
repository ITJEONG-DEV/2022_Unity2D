using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerEventsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite baseSprite;
    public Sprite overSprite;
    Text countText;

    public MainUI mainUI;

    DataDefine.ICONS icon;


    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = overSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = baseSprite;
    }

    void OnClickButton()
    {
        mainUI.SetSelectedOption(icon);
    }

    void RenewCountText(DataDefine.ICONS icon, int num)
    {
        Debug.Log($"RenewCountText({icon}, {num})");
        if (this.icon == icon)
            countText.text = num.ToString();
    }

    void Awake()
    {
        mainUI = GameObject.Find("MainUI").GetComponent<MainUI>();

        mainUI.AddItemChangedEventHandler(RenewCountText);

        try
        {
            countText = this.transform.GetChild(0).GetComponent<Text>();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }

        SetItemType();

        this.GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    void SetItemType()
    {
        switch(this.gameObject.name)
        {
            case "CORN":
                icon = DataDefine.ICONS.CORN;
                break;
            case "TURNIP":
                icon = DataDefine.ICONS.TURNIP;
                break;
            case "CARROT":
                icon = DataDefine.ICONS.CARROT;
                break;
            case "STRAWBERRY":
                icon = DataDefine.ICONS.STRAWBERRY;
                break;
            case "CORN_SEED":
                icon = DataDefine.ICONS.CORN_SEED;
                break;
            case "TURNIP_SEED":
                icon = DataDefine.ICONS.TURNIP_SEED;
                break;
            case "CARROT_SEED":
                icon = DataDefine.ICONS.CARROT_SEED;
                break;
            case "STRAWBERRY_SEED":
                icon = DataDefine.ICONS.STRAWBERRY_SEED;
                break;
            case "SICKLE":
                icon = DataDefine.ICONS.SICKLE;
                break;
            case "WATERING_CAN":
                icon = DataDefine.ICONS.WATERING_CAN;
                break;
        }
    }
}
