using System;
using UnityEngine;
using UnityEngine.UI;

public class PressClickButton : MonoBehaviour
{
    public Func<int> SettingButtonMarkNum;
    public Func<Sprite> HandlePlayerImageChange;
    private Button Button;
    private bool isClick = false;
    public int buttonRowIndex;
    public int buttonColIndex;
    private int markNum;
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
            
            isClick = true;
            Button.enabled = false;
        }
    }
}
