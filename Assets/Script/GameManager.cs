using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public enum EMarkType
    {
        defaultMark,
        xMark,
        oMark,
        max
    }
    private EMarkType player1;
    private EMarkType player2;
    private int gameTurn;
    [SerializeField] Sprite xMarkImg;
    [SerializeField] Sprite OMarkImg;
    public EMarkType[,] markValueGrid = new EMarkType[3, 3];
    public PressClickButton[] buttons;
    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    void Start()
    {
        InitializeButtonIndexSetting();
        InitializePlayerMark();
    }

    void Update()
    {
        CheckMarkLineMatch();
    }
    void InitializePlayerMark() //처음 시작할 마크가 O인지 X인지 정하기
    {
        gameTurn = 1;
        int turnMark = Random.Range((int)EMarkType.defaultMark + 1, (int)EMarkType.max);
        switch ((EMarkType)turnMark)
        {
            case EMarkType.xMark:
                player1 = EMarkType.xMark;
                player2 = EMarkType.oMark;
                break;
            case EMarkType.oMark:
                player1 = EMarkType.oMark;
                player2 = EMarkType.xMark;
                break;
        }
    }

    void InitializeButtonIndexSetting() // 버튼의 배열 위치 값을 초기화하고, 버튼을 아무것도 없는 마크 값으로 설정
    {
        int buttonIndex = 0;
        
        for (int i = 0; i < markValueGrid.GetLength(0); i++)
        {
            for (int j = 0; j < markValueGrid.GetLength(1); j++)
            {
                buttons[buttonIndex].buttonRowIndex = i;
                buttons[buttonIndex].buttonColIndex = j;
                ++buttonIndex;
            }
        }
    }

    public Sprite PreviewDMark()
    {
        Sprite tempImage = null;
        
        if (gameTurn % 2 == 0)
        {
            switch (player1)
            {
                case EMarkType.xMark:
                    tempImage = xMarkImg;
                    break;
                case EMarkType.oMark:
                    tempImage = OMarkImg;
                    break;
            }
        }
        else
        {
            switch (player2)
            {
                case EMarkType.xMark:
                    tempImage = xMarkImg;
                    break;
                case EMarkType.oMark:
                    tempImage = OMarkImg;
                    break;
            }
        }
        
        return tempImage;
    }
    
    public Sprite DisplayPlayerMark() // (UI_Button) 버튼 누를 시 버튼에 현재 순서의 플레이어의 마크 값 설정
    {
        Sprite tempImage = null;
        
        if (gameTurn % 2 == 0)
        {
            switch (player1)
            {
                case EMarkType.xMark:
                    tempImage = xMarkImg;
                    break;
                case EMarkType.oMark:
                    tempImage = OMarkImg;
                    break;
            }
        }
        else
        {
            switch (player2)
            {
                case EMarkType.xMark:
                    tempImage = xMarkImg;
                    break;
                case EMarkType.oMark:
                    tempImage = OMarkImg;
                    break;
            }
        }
        
        ++gameTurn;
        return tempImage;
    }

    public int SettingMarkValue() // (Value_Button) 버튼 누를 시 버튼에 플레이어의 마크 값 설정
    {
        int markValue = 0;
        if ((gameTurn + 1) % 2 == 0)
        {
            switch (player1)
            {
                case EMarkType.xMark:
                    markValue =  (int)EMarkType.xMark;
                    break;
                case EMarkType.oMark:
                    markValue =  (int)EMarkType.oMark;
                    break;
            }
        }
        else
        {
            switch (player2)
            {
                case EMarkType.xMark:
                    markValue =  (int)EMarkType.xMark;
                    break;
                case EMarkType.oMark:
                    markValue =  (int)EMarkType.oMark;
                    break;
            }
        }
        return markValue;
    }

    public void SaveMarkValuetoGrid(int rowIndex, int colIndex, int markNum) // (Grid) 버튼 누를 시 그리드에 마크 값 설정, Grid는 동일한 마크가 3개 이상 있는지 체크하기 위해 필요
    {
        markValueGrid[rowIndex, colIndex] = (EMarkType)markNum;
    }
    public void CheckMarkLineMatch()
    {
        EMarkType tempType = EMarkType.defaultMark;
        int sameValueCount = 0;
        
        for (int i = 0; i < markValueGrid.GetLength(1); i++)
        {
            for (int j = 0; j < markValueGrid.GetLength(0); j++)
            {
                if (j == 0)
                {
                    tempType = markValueGrid[i, j];
                    continue;
                }

                if (tempType == markValueGrid[i, j])
                {
                    ++sameValueCount;
                }

                tempType = markValueGrid[i, j];
            }
            
            if (sameValueCount == 2 && tempType != EMarkType.defaultMark)
            {
                if (tempType == player1)
                {
                    Debug.Log($"{player1}가 승리!");
                }
                else
                {
                    Debug.Log($"{player2}가 승리!");
                }
            }
            
            tempType = EMarkType.defaultMark;
            sameValueCount = 0;
        }
        
        for (int i = 0; i < markValueGrid.GetLength(0); i++)
        {
            for (int j = 0; j < markValueGrid.GetLength(1); j++)
            {
                if (j == 0)
                {
                    tempType = markValueGrid[j, i];
                    continue;
                }
        
                if (tempType == markValueGrid[j, i])
                {
                    ++sameValueCount;
                }
        
                tempType = markValueGrid[j, i];
            }
            
            if (sameValueCount == 2 && tempType != EMarkType.defaultMark)
            {
                if (tempType == player1)
                {
                    Debug.Log($"{player1}가 승리!");
                }
                else
                {
                    Debug.Log($"{player2}가 승리!");
                }
            }
            
            tempType = EMarkType.defaultMark;
            sameValueCount = 0;
        }
        
        tempType = markValueGrid[0, 0];
        
        if (tempType != EMarkType.defaultMark && markValueGrid[0, 0] == markValueGrid[1, 1] && markValueGrid[1, 1] == markValueGrid[2, 2])
        {
            if (tempType == player1)
            {
                Debug.Log($"{player1}가 승리!");
            }
            else
            {
                Debug.Log($"{player2}가 승리!");
            }
        }
        
        tempType = markValueGrid[0, 2];
        
        if (tempType != EMarkType.defaultMark && markValueGrid[0, 2] == markValueGrid[1, 1] && markValueGrid[1, 1] == markValueGrid[2, 0])
        {
            if (tempType == player1)
            {
                Debug.Log($"{player1}가 승리!");
            }
            else
            {
                Debug.Log($"{player2}가 승리!");
            }
        }
    }
    
}
