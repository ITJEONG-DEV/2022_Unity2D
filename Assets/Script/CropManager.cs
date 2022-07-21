using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public GameObject crops;
    public GameObject[] cropPrefabList;

    Dictionary<(float, float), DataDefine.CROPS> currentCropList;
    Dictionary<(float, float), DataDefine.GROWING_STATE> currentCropStateList;

    public delegate bool cropsStateChangedEventHandler(float x, float y, DataDefine.GROWING_STATE state);
    public delegate bool cropsRemovalEventHandler(float x, float y, DataDefine.CROPS crops);

    void Start()
    {
        crops = GameObject.Find("[CROPS]");

        currentCropList = new Dictionary<(float, float), DataDefine.CROPS>();
        currentCropStateList = new Dictionary<(float, float), DataDefine.GROWING_STATE>();
    }

    // crop state가 변경된 경우
    bool ChangeCropState(float x, float y, DataDefine.GROWING_STATE state)
    {
        if (currentCropList.ContainsKey((x, y)))
        {
            //Debug.Log($"ChangeCropState:{state}, ({x}, {y})");
            currentCropStateList[(x, y)] = state;
            return true;
        }

        return false;
    }

    // crop을 수확한 경우
    public bool RemoveCropInfo(float x, float y, DataDefine.CROPS type)
    {
        Debug.Log($"RemoveCropInfo:{type}, ({x}, {y})");

        return currentCropList.Remove((x, y));
    }

    // crop을 추가한 경우
    public bool AddCropInfo(float x, float y, DataDefine.CROPS type)
    {
        if (currentCropList.ContainsKey((x, y)))
            return false;

        //Debug.Log($"AddCropInfo:{type}, ({x}, {y})");

        currentCropList[(x, y)] = type;
        currentCropStateList[(x, y)] = DataDefine.GROWING_STATE.none;
        return true;
    }

    // 불러오기 용
    public void SetCrops(float x, float y, DataDefine.CROPS type, DataDefine.GROWING_STATE state)
    {
        if (!AddCropInfo(x, y, type))
            return;

        //Debug.Log($"CreateCrops:{type}, ({x}, {y})");
        var obj = Instantiate(cropPrefabList[(int)type - 1], new Vector3(x, y, 0), Quaternion.identity);
        obj.name = $"{x}_{y}_{type}_{(int)state}";
        obj.transform.SetParent(crops.transform);

        // 작물 타입 설정, 콜백함수 등록
        var growingCrops = obj.GetComponent<GrowingCrops>();
        growingCrops.SetCropType(type);
        growingCrops.SetState(state);
        growingCrops.AddCropsStateChangedEventHandler(ChangeCropState);
        growingCrops.AddCropsRemovalEventHandler(RemoveCropInfo);
    }

    // 생성
    public bool CreateCrops(float x, float y, DataDefine.CROPS type)
    {
        if (!AddCropInfo(x, y, type))
            return false;

        //Debug.Log($"CreateCrops:{type}, ({x}, {y})");
        var obj = Instantiate(cropPrefabList[(int)type - 1], new Vector3(x, y, 0), Quaternion.identity);
        obj.name = $"{x}_{y}_{type}_0";
        obj.transform.SetParent(crops.transform);
        
        // 작물 타입 설정, 콜백함수 등록
        var growingCrops = obj.GetComponent<GrowingCrops>();
        growingCrops.SetCropType(type);
        growingCrops.AddCropsStateChangedEventHandler(ChangeCropState);
        growingCrops.AddCropsRemovalEventHandler(RemoveCropInfo);

        return true;
    }
}
