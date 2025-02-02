using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private EMarkType player1;
    private EMarkType player2;
    private int gameTurn;
    [SerializeField] Button[] button;
    [SerializeField] Sprite xMarkImg;
    [SerializeField] Sprite OMarkImg;

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
    
    public enum EMarkType
    {
        xMark,
        oMark,
        max
    }
    
    void Start()
    {
        InitializePlayerMark();
    }

    void Update()
    {
    }

    void InitializePlayerMark()
    {
        gameTurn = 0;
        int temp = Random.Range(0, (int)EMarkType.max);
        switch ((EMarkType)temp)
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

        //그때마다 이벤트를 할당해주면 된다.
    }

    public Sprite PressTurnClick()
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
