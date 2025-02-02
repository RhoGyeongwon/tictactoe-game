using System;
using UnityEngine;
using UnityEngine.UI;

public class PressClickButton : MonoBehaviour
{
    private GameManager.EMarkType markType;
    public Func<Sprite> HandlePlayerImageChange;
    private Button Button;
    private bool isClick = false;
    public int buttonRowIndex;
    public int buttonColIndex;
    
    void Start()
    {
        Button = GetComponent<Button>();
        markType = GameManager.EMarkType.defaultMark;
    }

    public void ButtonUp()
    {
        if (!isClick)
        {
            HandlePlayerImageChange = () => GameManager.Instance.DisplayPlayerMark();
            Button.image.sprite = HandlePlayerImageChange?.Invoke();
            isClick = true;
            Button.enabled = false;
        }
    }
}
