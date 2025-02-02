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
        DisplayHighlightSprite(); //마우스에 버튼 올려두면 버튼 위에 마크를 미리 볼 수 있다. (실시간으로 언제나 돌아야한다.)
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
        if (!isClick) // 버튼에 클릭을 했다면 (버튼 클릭 시 아래 함수들이 호출을 한다.)
        {
            // 게임 매니저에서 함수를 불러오는 이유는 게임 매니저가 x,o 마크를 결정하는 권한을 갖고 있기 때문
            // 버튼 클릭 순간 필요한 함수들을 게임 매니저로부터 담고, 함수를 곧바로 실행한다.
            
            HandlePlayerImageChange = () => GameManager.Instance.DisplayPlayerMark(); // 버튼 클릭 시 플레이어 마크를 UI에 표시한다
            Button.image.sprite = HandlePlayerImageChange?.Invoke();
            
            SettingButtonMarkNum = () => GameManager.Instance.SettingMarkValue(); // 버튼 클릭 시 버튼에 X,O마크 값을 담는다.
            markNum = SettingButtonMarkNum();

            SettingMarkValueToGrid = () => GameManager.Instance.SaveMarkValuetoGrid(buttonRowIndex, buttonColIndex, markNum);// 2차원 배열 그리드 값에도 버튼의 X,O 마크 값을 담는다.
            SettingMarkValueToGrid?.Invoke(); //그리드 값으로 마크가 3개가 있는지 확인!
            
            isClick = true; //더 이상 클릭 못하게 해제
            Button.enabled = false; //버튼 이벤트도 해제

            ++GameManager.Instance.gameTurn; //상대 편으로 턴이 바뀐다.
        }
    }
}
