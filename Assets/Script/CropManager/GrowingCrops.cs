using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingCrops : MonoBehaviour
{
    DataDefine.GROWING_STATE state;
    DataDefine.CROPS crop;
    float remainTime;
    float[] range = { 3.0f, 5.0f };

    CropManager.cropsStateChangedEventHandler cropsStateChangedEventHandler;

    void Awake()
    {
        state = DataDefine.GROWING_STATE.first;
        ActiveImage();

        remainTime = Random.Range(range[0], range[1]);
        cropsStateChangedEventHandler?.Invoke(transform.position.x, transform.position.y, this.state);
        // Growing();
    }

    void Update()
    {
        if(state < DataDefine.GROWING_STATE.finish)
        {
            remainTime -= Time.deltaTime;

            if (remainTime <= 0)
                Growing();
        }
    }

    public void SetState(DataDefine.GROWING_STATE state)
    {
        this.state = state-1;
        //Debug.Log($"state: {state}, current state: {this.state}");
        Growing();
    }

    public void Watering(float term)
    {
        remainTime -= term;
    }

    void ActiveImage()
    {
        for(int i=0; i<this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).name==((int)this.state+1).ToString())
            {
                this.transform.GetChild(i).gameObject.SetActive(true);

                //Debug.Log($"ActiveImage: {i}, name: {this.transform.GetChild(i).name}, state: {this.state}");
            }
            else
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void Growing()
    {
        var name = this.gameObject.name;
        var name_words = name.Split('_');
        //int statenum = int.Parse(name_words[name_words.Length - 1]);

        name_words[name_words.Length-1] = ((int)(++this.state)).ToString();
        this.gameObject.name = string.Join('_', name_words);
        //Debug.Log($"words: {name_words[name_words.Length - 1]}, new name: {this.gameObject.name}");

        ActiveImage();

        remainTime = Random.Range(range[0], range[1]);

        cropsStateChangedEventHandler?.Invoke(transform.position.x, transform.position.y, this.state);
    }

    public void SetCropType(DataDefine.CROPS crop)
    {
        this.crop = crop;
    }

    public void AddCropsStateChangedEventHandler(CropManager.cropsStateChangedEventHandler callback)
    {
        this.cropsStateChangedEventHandler += callback;
    }
}
