using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUI : MonoBehaviour
{
    public GameObject icons;
    public Text gold;

    DataManager dataManager;
    CropManager cropManager;

    public Sprite[] spritesforCursor;
    Texture2D[] textureListforCursor;

    DataDefine.ICONS option = DataDefine.ICONS.DEFAULT;

    void Start()
    {
        cropManager = GetComponent<CropManager>();
        dataManager = GetComponent<DataManager>();

        icons = GameObject.Find("[ICONS]");
        for (int i = 0; i < icons.transform.childCount; i++)
            icons.transform.GetChild(i).gameObject.SetActive(true);

        gold = GameObject.Find("GOLD").GetComponent<Text>();
        AddItemChangedEventHandler(RenewGoldText);

        // ���콺 Ŀ�� ��� ������ ���� �ؽ��� ����
        SetTexturesForCursor();


    }

    void Update()
    {
        // ESC�� ������ ���, default ���·� ����
        OnClickESC();
    }

    void RenewGoldText(DataDefine.ICONS icons, int numm)
    {
        if (icons == DataDefine.ICONS.GOLD)
            gold.text = numm + " G";

    }

    public void AddItemChangedEventHandler(DataManager.itemChanged handler)
    {
        dataManager.AddItemChangedEventHandler(handler);
    }

    void SetTexturesForCursor()
    {
        textureListforCursor = new Texture2D[spritesforCursor.Length];

        for(int i=0; i<spritesforCursor.Length; i++)
        {
            if (i == 0)
                textureListforCursor[i] = spritesforCursor[i].texture;
            else
            {
                int x = Mathf.FloorToInt(spritesforCursor[i].textureRect.x);
                int y = Mathf.FloorToInt(spritesforCursor[i].textureRect.y);
                int width = Mathf.FloorToInt(spritesforCursor[i].textureRect.width);
                int height = Mathf.FloorToInt(spritesforCursor[i].textureRect.height);
                Texture2D newTexture = new Texture2D(width, height);
                Color[] newColors = spritesforCursor[i].texture.GetPixels(x, y, width, height);

                newTexture.SetPixels(newColors);
                newTexture.Apply();

                textureListforCursor[i] = newTexture;
            }
        }

        ChangeCursorTexture();
    }

    void ChangeCursorTexture()
    {
        Cursor.SetCursor(textureListforCursor[(int)option], Vector2.zero, CursorMode.ForceSoftware);
    }

    // ������ Ŭ�� �� ȣ���
    public void SetSelectedOption(DataDefine.ICONS item)
    {
        this.option = item;
        ChangeCursorTexture();
    }

    public void OnClickESC()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.option = DataDefine.ICONS.DEFAULT;

            ChangeCursorTexture();
        }
    }
    
    // ������ Ŭ���� ��� �۹� ����
    public void OnClickFarmGround(Vector3 pos)
    {
        switch (option)
        {
            case DataDefine.ICONS.CORN_SEED:
                if (dataManager.Use(DataDefine.ITEMS.CORN_SEED, -1))
                {
                    var result = cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.CORN);
                    if (!result) dataManager.Use(DataDefine.ITEMS.CORN_SEED, 1);
                }
                break;
            case DataDefine.ICONS.TURNIP_SEED:
                if (dataManager.Use(DataDefine.ITEMS.TURNIP_SEED, -1))
                {
                    var result = cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.TURNIP);
                    if (!result) dataManager.Use(DataDefine.ITEMS.TURNIP_SEED, 1);
                }
                break;
            case DataDefine.ICONS.CARROT_SEED:
                if (dataManager.Use(DataDefine.ITEMS.CARROT_SEED, -1))
                {
                    var result = cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.CARROT);
                    if (!result) dataManager.Use(DataDefine.ITEMS.CARROT_SEED, 1);
                }
                break;
            case DataDefine.ICONS.STRAWBERRY_SEED:
                if (dataManager.Use(DataDefine.ITEMS.STRAWBERRY_SEED, -1))
                {
                    var result = cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.STRAWBERRY);
                    if (!result) dataManager.Use(DataDefine.ITEMS.STRAWBERRY_SEED, 1);
                }
                break;
        }
    }
}
