using System.Collections;
using System.Collections.Generic;
using CitrioN.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WearhouseState : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isOpen = false;
    public TMP_Dropdown tmpDropdown;
    public GameObject[] wearhouse;
    public RectTransform showContent ;

    public Sprite yes;
    public Sprite no;

    private int wearhouseIndex = 0;
    private bool[,] storeState;
    void Start()
    {
        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        storeState = wearhouse[wearhouseIndex].GetComponent<Wearhouse>().isStore;
        UpdateInfo();
    }
    void OnDropdownValueChanged(int index)
    {
        wearhouseIndex = index;
        storeState = wearhouse[wearhouseIndex].GetComponent<Wearhouse>().isStore;
        UpdateInfo();
    }
    void Update()
    {
        UpdateInfo();
    }
    public void UpdateInfo()
    {
        int index = 0;
        for(int i = 0;i<9;i++)
        {
            for(int j = 0;j<20;j++)
            {
                bool isStore = storeState[j,i];
                if(isStore)
                    showContent.GetChild(index).GetComponent<Image>().sprite = yes;
                else
                    showContent.GetChild(index).GetComponent<Image>().sprite = no;
                index++;
            }
        }
    }

    public void SwitchState()
    {
        isOpen = !isOpen;
        gameObject.SetActive(isOpen);
    }
}
