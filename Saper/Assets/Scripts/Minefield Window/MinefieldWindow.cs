using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinefieldWindow : MonoBehaviour
{
    [SerializeField] private Button _exitButton, _restartButton;
    [SerializeField] private StartWindow _startWindow;
    [SerializeField] private Minefield _minefield;

    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private Button _cellTemplate;

    private void OnEnable()
    {
        _minefield.CreateNewMinefieldCells(8, 8, 8, _cellTemplate);

        _minefield.OnGameOver += StartGameOver;

        _exitButton.onClick.AddListener(RemoveToStartWindow);
    }

    private void OnDisable()
    {
        _minefield.OnGameOver -= StartGameOver;

        _exitButton.onClick.AddListener(RemoveToStartWindow);
        _restartButton.onClick.RemoveAllListeners();
    }

    private void RemoveToStartWindow()
    {
        gameObject.SetActive(false);
        _gameOverPanel.SetActive(false);
        _startWindow.gameObject.SetActive(true);
    }

    private void StartGameOver()
    {
        StartCoroutine(OpenGameOverWindow());
        _restartButton.onClick.AddListener(RemoveToStartWindow);
    }

    private IEnumerator OpenGameOverWindow()
    {
        yield return new WaitForSeconds(2f);
        _gameOverPanel.SetActive(true);
    }
}
