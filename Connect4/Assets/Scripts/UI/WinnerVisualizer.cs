using TMPro;
using UnityEngine;

public class WinnerVisualizer : MonoBehaviour
{
    private TextMeshProUGUI _winnerTex;
    private TurnBaseController _turnController => TurnBaseController.Instance;

    private void Start()
    {
        _winnerTex = GetComponent<TextMeshProUGUI>();
    }

    public void ShowWinner()
    {
        var winner = _turnController.turnState.ToString();
        _winnerTex.text = $"Winner: {winner}";
    }
}
