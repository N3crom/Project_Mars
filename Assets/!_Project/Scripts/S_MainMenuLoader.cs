using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenuLoader : MonoBehaviour
{
    public RSE_LoadMainMenu _rseLoadMainMenu;
    public RSE_LoadNextLevel _rseLoadNextLevel;

    public string _nextLevelName;

    private void OnEnable()
    {
        _rseLoadMainMenu.action += LoadMainMenu;
        _rseLoadNextLevel.action += LoadNextLevel;
    }

    private void OnDisable()
    {
        _rseLoadMainMenu.action -= LoadMainMenu;
        _rseLoadNextLevel.action -= LoadNextLevel;
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Scene_MainMenu");
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextLevelName);
    }
}