using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GridClickHandler : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private int columnIndex;

    private Button _button;
    private TurnBaseController _turnController => TurnBaseController.Instance;
    private int _column => GridGenerator.Instance.width;
    private int _row => GridGenerator.Instance.height;
    

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
        _button.onClick.AddListener(_turnController.ChangeTurn);
    }

    
    /// <summary>
    /// Places a coin in the grid. 
    /// The `columnIndex` indicates which column corresponds to the pressed button.   
    /// </summary>
    private void OnButtonClick()
    {
        var row = FindLowestFreeRowInColumn(columnIndex);
    
        if (row != -1)
        {
            var cell = gridParent.GetChild(row * _column + columnIndex);
            if (cell.childCount == 0)
            {
                var spawnPosition = new Vector3(cell.position.x,cell.position.y + 8,0);
                var instanceCoinIndex = CoinPrefabIndex.Instance;
                var coinIndex = Mathf.Abs(_column + _row) % (int)_turnController.turnState;
                
                var newCoin = Instantiate(instanceCoinIndex.coinPrefabIndex[coinIndex], spawnPosition, Quaternion.identity, cell);

                StartCoroutine(FallToPosition(newCoin.transform, cell.position, 0.5f)); 
                
                StartCoroutine(AddAsChildAfterFall(newCoin, cell,row,coinIndex));
                
            }
        }
        else
        {
            Debug.Log("This column is full");
        }
    }
    
    /// <summary>
    /// It will find the first/lowest in the row of the column.
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    private int FindLowestFreeRowInColumn(int col)
    {
        for (var row = 0; row < _row; row++)
        {
            if (gridParent.GetChild(row * _column + col).childCount == 0)
            {
                return row; 
            }
        }
        return -1; 
    }
    
    /// <summary>
    /// Moved the coin of the startPosition to the targetPosition with a fall Effect.
    /// </summary>
    /// <param name="coin"></param>
    /// <param name="targetPosition"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator FallToPosition(Transform coin, Vector3 targetPosition, float duration)
    {
        var elapsedTime = 0f;
        var startingPos = coin.position;

        while (elapsedTime < duration)
        {
            coin.position = Vector3.Lerp(startingPos, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        coin.position = targetPosition; 
    }

    
    /// <summary>
    /// Voegt de coin als child toe aan de cel na het val-effect, en meldt de plaatsing aan de game logica.
    /// Adds the coin to the child of a cell after the fall effect, and report de placing to the game logic. 
    /// </summary>
    /// <param name="coin"></param>
    /// <param name="cell"></param>
    /// <returns></returns>
    private IEnumerator AddAsChildAfterFall(GameObject coin, Transform cell,int row, int coinIndex)
    {
        yield return new WaitForSeconds(0.5f);
        coin.transform.SetParent(cell);
        GameLogic.Instance.OnCoinPlaced(new Vector2Int(columnIndex, row), coinIndex);
        
    }

    
}