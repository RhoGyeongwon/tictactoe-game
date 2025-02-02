using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class PressClickButton : MonoBehaviour
{
    public Action SettingMarkValueToGrid;
    public Func<int> SettingButtonMarkNum;
    public Func<Sprite> HandlePlayerImageChange;
    private Button Button;
    private bool isClick = false;
    public int buttonRowIndex;
    public int buttonColIndex;
    public int markNum;
    void Start()
    {
        Button = GetComponent<Button>();
    }

    public void ButtonUp()
    {
        if (!isClick)
        {
            HandlePlayerImageChange = () => GameManager.Instance.DisplayPlayerMark();
            Button.image.sprite = HandlePlayerImageChange?.Invoke();
            
            SettingButtonMarkNum = () => GameManager.Instance.SettingMarkValue();
            markNum = SettingButtonMarkNum();

            SettingMarkValueToGrid = () => GameManager.Instance.SaveMarkValuetoGrid(buttonRowIndex, buttonColIndex, markNum);
            SettingMarkValueToGrid?.Invoke();
            
            isClick = true;
            Button.enabled = false;
        }
    }
}
