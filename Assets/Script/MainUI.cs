using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUI : MonoBehaviour
{
    string option;
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SetSelectedOption(string name)
    {
        this.option = name;
    }
}
