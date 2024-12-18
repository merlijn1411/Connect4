using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    private Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RetryGame);
        
        gameObject.SetActive(false);
    }
   
    private static void RetryGame()
    {
        SceneManager.LoadScene("Connect4");
    }
}
