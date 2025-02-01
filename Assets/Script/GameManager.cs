using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //player1인 상태에서 버튼을 클릭하면
    //player1이 가지고 있는 이미지가 뜬다.
    
    private EMarkType player1;
    private EMarkType player2;
    private int gameTurn;
    private Button[] button;
    private Sprite _sprite;
    [SerializeField] Image xMarkImg;
    [SerializeField] Image OMarkImg;
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
        for (int i = 0; i < button.Length; i++)
        {
            PressClickButton tempButton = button[i].GetComponent<PressClickButton>();
            if (tempButton.IsClick)
            {
                tempButton.buttonClickaction = () => PressTurnClick();
            }
        }
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

    Image PressTurnClick()
    {
        Image tempImage = null;
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

            return tempImage;
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
            
            return tempImage;
        }
    }
}
