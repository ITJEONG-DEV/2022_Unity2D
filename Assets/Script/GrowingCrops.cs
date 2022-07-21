using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingCrops : MonoBehaviour
{
    GameObject[] childs;

    DataDefine.GROWING_STATE state;
    DataDefine.CROPS crop;
    float remainTime;
    float[] range = { 3.0f, 5.0f };

    CropManager.cropsStateChangedEventHandler cropsStateChangedEventHandler;
    CropManager.cropsRemovalEventHandler cropsRemovalEventHandler;

    void Start()
    {
        state = DataDefine.GROWING_STATE.none;
        Growing();
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
        Growing();
    }

    void ActiveImage()
    {
        for(int i=0; i<this.transform.childCount; i++)
        {
            if(i==(int)state)
                this.transform.GetChild(i).gameObject.SetActive(true);
            else
                this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Growing()
    {
        var name = this.gameObject.name;
        var name_words = name.Split('_');
        name_words[name_words.Length-1] = ((int)++state).ToString();
        this.gameObject.name = string.Join('_', name_words);
        
        ActiveImage();

        remainTime = Random.Range(range[0], range[1]);

        cropsStateChangedEventHandler?.Invoke(transform.position.x, transform.position.y, state);
    }

    void Harvest()
    {
        cropsRemovalEventHandler?.Invoke(transform.position.x, transform.position.y, crop);
    }

    public void SetCropType(DataDefine.CROPS crop)
    {
        this.crop = crop;
    }

    public void AddCropsStateChangedEventHandler(CropManager.cropsStateChangedEventHandler callback)
    {
        this.cropsStateChangedEventHandler += callback;
    }

    public void AddCropsRemovalEventHandler(CropManager.cropsRemovalEventHandler callback)
    {
        this.cropsRemovalEventHandler += callback;
    }
}
