using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUI : MonoBehaviour
{
    public Sprite[] sprites;
    Texture2D[] textureList;
    Texture2D cursorImg;
    string option = "DEFAULT";

    void Start()
    {
        SetTextures();
        ChangeCursorTexture();
    }

    void Update()
    {
        OnClickESC();
    }

    void ChangeCursor()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }

    void SetTextures()
    {
        textureList = new Texture2D[sprites.Length];

        for(int i=0; i<sprites.Length; i++)
        {
            if (i == 0)
                textureList[i] = sprites[i].texture;
            else
            {
                int x = Mathf.FloorToInt(sprites[i].textureRect.x);
                int y = Mathf.FloorToInt(sprites[i].textureRect.y);
                int width = Mathf.FloorToInt(sprites[i].textureRect.width);
                int height = Mathf.FloorToInt(sprites[i].textureRect.height);
                Texture2D newTexture = new Texture2D(width, height);
                Color[] newColors = sprites[i].texture.GetPixels(x, y, width, height);

                newTexture.SetPixels(newColors);
                newTexture.Apply();

                textureList[i] = newTexture;
            }
        }


    }

    void ChangeCursorTexture()
    {
        for (int i = 0; i < 11; i++)
            if (this.option == ((DataDefine.ICONS)i).ToString())
                cursorImg = textureList[i];

        ChangeCursor();
    }

    public void SetSelectedOption(string name)
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
}
