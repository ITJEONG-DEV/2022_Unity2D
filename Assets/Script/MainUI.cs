using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUI : MonoBehaviour
{
    public GameObject icons;

    DataManager dataManager;
    CropManager cropManager;

    public Sprite[] spritesforCursor;
    Texture2D[] textureListforCursor;


    string option = "DEFAULT";

    void Start()
    {
        dataManager = GetComponent<DataManager>();
        cropManager = GetComponent<CropManager>();

        icons = GameObject.Find("[ICONS]");

        // ���콺 Ŀ�� ��� ������ ���� �ؽ��� ����
        SetTexturesForCursor();

    }

    void Update()
    {
        // ESC�� ������ ���, default ���·� ����
        OnClickESC();
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
        for (int i = 0; i < 11; i++)
            if (this.option == ((DataDefine.ICONS)i).ToString())
                Cursor.SetCursor(textureListforCursor[i], Vector2.zero, CursorMode.ForceSoftware);
    }

    // ������ Ŭ�� �� ȣ���
    void SetSelectedOption(string name)
    {
        this.option = name.ToUpper();
        ChangeCursorTexture();
    }

    public void OnClickESC()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.option = "DEFAULT";

            ChangeCursorTexture();
        }
    }
    
    // ������ Ŭ���� ��� �۹� ����
    public void OnClickFarmGround(Vector3 pos)
    {
        Debug.Log(this.option);
        switch(option)
        {
            case "CORN_SEED":
                cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.CORN);
                break;
            case "TURNIP_SEED":
                cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.TURNIP);
                break;
            case "CARROT_SEED":
                cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.CARROT);
                break;
            case "STRAWBERRY_SEED":
                cropManager.CreateCrops(pos.x, pos.y, DataDefine.CROPS.STRAWBERRY);
                break;
        }
    }
}
