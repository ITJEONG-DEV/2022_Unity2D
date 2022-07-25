using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public GameObject ShowPanel;

    bool active = false;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    void OnClickButton()
    {
        active= !active;
        ShowPanel.SetActive(active);
    }
}
