using UnityEngine;

public class TurnBaseController : MonoBehaviour
{
    public static TurnBaseController Instance;
    public TurnState turnState;
    
    private void Awake()
    {
        Instance = this;
    }


    public void ChangeTurn()
    {
        var changeTurn = turnState == TurnState.PlayerYellow ? TurnState.PlayerRed : TurnState.PlayerYellow;
        turnState = changeTurn;
    }
}
