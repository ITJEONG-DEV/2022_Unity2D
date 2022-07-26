using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public GameObject crops;
    public GameObject[] cropPrefabList;

    Dictionary<(float, float), DataDefine.CROPS> currentCropsList;
    Dictionary<(float, float), DataDefine.GROWING_STATE> currentCropsStateList;
    Dictionary<(float, float), GameObject> currentCropsObject;

    public delegate bool cropsStateChangedEventHandler(float x, float y, DataDefine.GROWING_STATE state);
    public delegate bool cropsRemovalEventHandler(float x, float y);

    void Start()
    {
        crops = GameObject.Find("[CROPS]");

        currentCropsList = new Dictionary<(float, float), DataDefine.CROPS>();
        currentCropsStateList = new Dictionary<(float, float), DataDefine.GROWING_STATE>();
        currentCropsObject = new Dictionary<(float, float), GameObject>();
    }

    // crop state�� ����� ���
    bool ChangeCropState(float x, float y, DataDefine.GROWING_STATE state)
    {
        if (currentCropsList.ContainsKey((x, y)))
        {
            //Debug.Log($"ChangeCropState:{state}, ({x}, {y})");
            currentCropsStateList[(x, y)] = state;
            return true;
        }

        return false;
    }

    public Dictionary<(float, float), DataDefine.CROPS> GetCurrentCropsList()
    {
        return currentCropsList;
    }

    public Dictionary<(float, float), DataDefine.GROWING_STATE> GetCurrentCropsStateList()
    {
        return currentCropsStateList;
    }

    // crop�� ��Ȯ�� ���
    public bool RemoveCropInfo(float x, float y)
    {
        if (!currentCropsList.ContainsKey((x, y)))
            return false;

        var type = currentCropsList[(x, y)];
        //Debug.Log($"RemoveCropInfo:{type}, ({x}, {y})");

        var result = currentCropsList.Remove((x, y));

        if (result)
        {
            currentCropsStateList.Remove((x, y));

            var obj = currentCropsObject[(x, y)];
            currentCropsObject.Remove((x, y));
            Destroy(obj);
        }

        return result;
    }

    // crop�� �߰��� ���
    public bool AddCropInfo(float x, float y, DataDefine.CROPS type)
    {
        if (currentCropsList.ContainsKey((x, y)))
            return false;

        //Debug.Log($"AddCropInfo:{type}, ({x}, {y})");

        currentCropsList[(x, y)] = type;
        currentCropsStateList[(x, y)] = DataDefine.GROWING_STATE.none;
        return true;
    }

    public int CheckIsInCrop(float x, float y)
    {
        if (!currentCropsList.ContainsKey((x, y)))
            return -1;

        if (currentCropsStateList[(x, y)] == DataDefine.GROWING_STATE.finish)
            return 1;

        return 0;
    }

    public DataDefine.CROPS GetCropInfo(float x, float y)
    {
        if (!currentCropsList.ContainsKey((x, y)))
            return DataDefine.CROPS.DEFAULT;
        
        return currentCropsList[(x, y)];
    }

    public DataDefine.GROWING_STATE GetCropState(float x, float y)
    {
        if (!currentCropsList.ContainsKey((x, y)))
            return DataDefine.GROWING_STATE.none;

        return currentCropsStateList[(x, y)];
    }

    // �ҷ����� ��
    public void SetCrops(float x, float y, DataDefine.CROPS type, DataDefine.GROWING_STATE state)
    {
        if (!AddCropInfo(x, y, type))
            return;

        //Debug.Log($"CreateCrops:{type}, ({x}, {y})");
        var obj = Instantiate(cropPrefabList[(int)type - 1], new Vector3(x, y, 0), Quaternion.identity);
        obj.name = $"{x}_{y}_{type}_{(int)state}";
        obj.transform.SetParent(crops.transform);

        // �۹� Ÿ�� ����, �ݹ��Լ� ���
        var growingCrops = obj.GetComponent<GrowingCrops>();
        growingCrops.SetCropType(type);
        growingCrops.SetState(state);
        growingCrops.AddCropsStateChangedEventHandler(ChangeCropState);
    }

    // ����
    public bool CreateCrops(float x, float y, DataDefine.CROPS type)
    {
        if (!AddCropInfo(x, y, type))
            return false;

        //Debug.Log($"CreateCrops:{type}, ({x}, {y})");
        var obj = Instantiate(cropPrefabList[(int)type - 1], new Vector3(x, y, 0), Quaternion.identity);
        obj.name = $"{x}_{y}_{type}_0";
        obj.transform.SetParent(crops.transform);

        currentCropsObject[(x, y)] = obj;
        
        // �۹� Ÿ�� ����, �ݹ��Լ� ���
        var growingCrops = obj.GetComponent<GrowingCrops>();
        growingCrops.SetCropType(type);
        growingCrops.AddCropsStateChangedEventHandler(ChangeCropState);

        return true;
    }

    public bool Watering(float x, float y)
    {
        if (!currentCropsList.ContainsKey((x, y)))
            return false;

        float term = Random.Range(1.0f, 3.0f)*1000;
        currentCropsObject[(x, y)].GetComponent<GrowingCrops>().Watering(term);
        //Debug.Log($"Watering::({x}, {y}) > {term}s");

        return true;
    }
}
