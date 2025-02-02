using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class PressClickButton : MonoBehaviour
{
    public Action SettingMarkValueToGrid;
    public Func<int> SettingButtonMarkNum;
    public Func<Sprite> HandlePlayerImageChange;
    public Func<Sprite> HighlightCellWithPlayerMark;
    
    private Button Button;
    private bool isClick = false;
    public int buttonRowIndex;
    public int buttonColIndex;
    public int markNum;
    void Start()
    {
        Button = GetComponent<Button>();
    }

    void Update()
    {
        DisplayHighlightSprite();
    }
    
    public void DisplayHighlightSprite()
    {
        HighlightCellWithPlayerMark = () => GameManager.Instance.PreviewDMark();

        //★ spriteState는 구조체라서 직접 수정못함
        var spriteState = Button.spriteState;
        spriteState.highlightedSprite = HighlightCellWithPlayerMark();
        Button.spriteState = spriteState;
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
            
            Debug.Log(markNum);
        }
    }
}
