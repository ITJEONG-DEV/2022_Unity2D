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
    public string name;
    public int count;

    Text countText;

    public MainUI mainUI;

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
        mainUI.SetSelectedOption(name);
    }

    void RenewCountText()
    {
        if (countText == null || count == -1)
            return;

        countText.text = count.ToString();
    }

    void Start()
    {
        mainUI = GameObject.Find("MainUI").GetComponent<MainUI>();

        try
        {
            countText = this.transform.GetChild(0).GetComponent<Text>();
            RenewCountText();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        this.GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
