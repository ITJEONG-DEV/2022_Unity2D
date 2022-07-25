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

        // 마우스 커서 모양 변경을 위한 텍스쳐 설정
        SetTexturesForCursor();


    }

    void Update()
    {
        // ESC를 누르는 경우, default 상태로 변경
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

    // 아이콘 클릭 시 호출됨
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
    
    // 농지를 클릭한 경우 작물 생성
    public void OnClickFarmGround(Vector3 pos)
    {
        switch (option)
        {
            case DataDefine.ICONS.CORN_SEED:
                if (dataManager.Add(DataDefine.ITEMS.CORN_SEED, -1))
                {
                    var result = cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.CORN);
                    if (!result) dataManager.Add(DataDefine.ITEMS.CORN_SEED, 1);
                }
                break;
            case DataDefine.ICONS.TURNIP_SEED:
                if (dataManager.Add(DataDefine.ITEMS.TURNIP_SEED, -1))
                {
                    var result = cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.TURNIP);
                    if (!result) dataManager.Add(DataDefine.ITEMS.TURNIP_SEED, 1);
                }
                break;
            case DataDefine.ICONS.CARROT_SEED:
                if (dataManager.Add(DataDefine.ITEMS.CARROT_SEED, -1))
                {
                    var result = cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.CARROT);
                    if (!result) dataManager.Add(DataDefine.ITEMS.CARROT_SEED, 1);
                }
                break;
            case DataDefine.ICONS.STRAWBERRY_SEED:
                if (dataManager.Add(DataDefine.ITEMS.STRAWBERRY_SEED, -1))
                {
                    var result = cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.STRAWBERRY);
                    if (!result) dataManager.Add(DataDefine.ITEMS.STRAWBERRY_SEED, 1);
                }
                break;
            case DataDefine.ICONS.SICKLE:
                if(cropManager.CheckIsInCrop(pos.x, pos.y) == 1)
                {
                    // harvest
                    DataDefine.CROPS type = cropManager.GetCropInfo(pos.x, pos.y);
                    var result = cropManager.RemoveCropInfo(pos.x, pos.y);

                    if(result)
                    {
                        DataDefine.ITEMS itemType = DataDefine.GetItemInfo(type);
                        int itemNum = Random.Range(1, 3);
                        
                        DataDefine.ITEMS seedType = DataDefine.GetSeedInfo(type);

                        int seedNum = 1;
                        if (Random.Range(0f, 1f) < 0.85f)
                            seedNum = 2;

                        if (itemType == DataDefine.ITEMS.GOLD) return;

                        dataManager.Add(itemType, itemNum);
                        dataManager.Add(seedType, seedNum);

                        Debug.Log($"Harvest:: type: {type}, itemType: {itemType}-{itemNum}, seedType: {seedType}-{seedNum}");
                    }
                }
                break;
        }
    }
}
