using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;

    private TurnBaseController _turnController => TurnBaseController.Instance;
    private int _width => GridGenerator.Instance.width;
    private int _height => GridGenerator.Instance.height;
    private Transform _gridParent => GridGenerator.Instance.transform;

    [SerializeField] private UnityEvent onFirstFourOnARow;
    
    private void Awake()
    {
        Instance = this;
    }
    

    /// <summary>
    /// Method to check if there are four on a row next to each other.
    /// </summary>
    /// <param name="lastPlacedPosition"></param>
    /// <param name="coinIndex"></param>
    private void CheckForWin(Vector2Int lastPlacedPosition, int coinIndex)
    {
        if (CheckWin(lastPlacedPosition, coinIndex))
            onFirstFourOnARow.Invoke();
    }


    /// <summary>
    /// It checks in every direction if there are four on a row. 
    /// </summary>
    /// <param name="lastPlacedPosition"></param>
    /// <param name="coinIndex"></param>
    /// <returns></returns>
    private bool CheckWin(Vector2Int lastPlacedPosition, int coinIndex)
    {
        return CheckDirection(lastPlacedPosition, Vector2Int.right, coinIndex) ||  
               CheckDirection(lastPlacedPosition, Vector2Int.up, coinIndex) ||     
               CheckDirection(lastPlacedPosition, new Vector2Int(1, 1), coinIndex) || 
               CheckDirection(lastPlacedPosition, new Vector2Int(-1, 1), coinIndex);  
    }


    /// <summary>
    /// It checks in every direction if there are four of the same coin next to each other.
    /// It resets if there is a different coin or there are no coins next to it.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="direction"></param>
    /// <param name="coinIndex"></param>
    /// <returns></returns>
    private bool CheckDirection(Vector2Int pos, Vector2Int direction, int coinIndex)
    {
        var count = 0;
        var lastCoinColor = CoinPrefabIndex.Instance.coinPrefabIndex[coinIndex].GetComponent<SpriteRenderer>().color;

        for (var i = -3; i <= 3; i++)
        {
            var checkPos = pos + direction * i;
            if (checkPos.x >= 0 && checkPos.x < _width && checkPos.y >= 0 && checkPos.y < _height)
            {
                var cell = _gridParent.GetChild(checkPos.y * _width + checkPos.x);
                if (cell.childCount > 0 && cell.GetChild(0).GetComponent<SpriteRenderer>().color == lastCoinColor)
                {
                    count++;
                    if (count == 4) 
                        return true;
                }
                else
                    count = 0; 
            }
        }
        return false;
    }
    
    /// <summary>
    /// Each time a coin is placed, it checks to see if four are connected.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="coinIndex"></param>
    public void OnCoinPlaced(Vector2Int position, int coinIndex)
    {
        CheckForWin(position, coinIndex);
    }
}