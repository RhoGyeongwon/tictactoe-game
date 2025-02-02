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
    public EMarkType[,] Grid = new EMarkType[3, 3];
    public PressClickButton[] buttons;
    
    public int testbuttonRowIndex;
    public int testbuttonColIndex;
    
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

    void InitializePlayerMark() //처음 시작할 마크가 O인지 X인지 정하기
    {
        gameTurn = 0;
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

    void InitializeButtonIndexSetting() // 버튼의 배열 위치 값을 초기화하고, 버튼을 defaultMark로 설정
    {
        int buttonIndex = 0;
        
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                buttons[buttonIndex].buttonRowIndex = i;
                buttons[buttonIndex].buttonColIndex = j;
                ++buttonIndex;
            }
        }
    }
    
    public Sprite DisplayPlayerMark() //버튼 누를 시 player의 마크가 표시 된다.
    {
        Sprite tempImage = null;
        ++gameTurn;
        
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
}
