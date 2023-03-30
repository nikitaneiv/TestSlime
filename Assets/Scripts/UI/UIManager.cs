using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;

    private GameObject _currentScreen;

    private void Start()
    {
        _currentScreen = _gameScreen;
    }

    public void GameScreen()
    {
        _currentScreen.SetActive(true);
        _currentScreen = _gameScreen;

    }
    public void LoseScreen()
    {
        _currentScreen.SetActive(false);
        _loseScreen.SetActive(true);
        _currentScreen = _loseScreen;
    }
    public void WinScreen()
    {
        _currentScreen.SetActive(false);
        _winScreen.SetActive(true);
        _currentScreen = _winScreen;
    }
}
