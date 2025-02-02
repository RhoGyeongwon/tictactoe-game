using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum EMarkType
    {
        noMark,
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
        InitializePlayerMark();
    }

    void InitializePlayerMark() //처음 시작할 마크가 O인지 X인지 정하기
    {
        gameTurn = 0;
        int turnMark = Random.Range((int)EMarkType.noMark + 1, (int)EMarkType.max);
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
