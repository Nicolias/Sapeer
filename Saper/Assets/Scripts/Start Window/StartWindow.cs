using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private MinefieldWindow _minefieldWindow;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveAllListeners();
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        _minefieldWindow.gameObject.SetActive(true);
    }
}
