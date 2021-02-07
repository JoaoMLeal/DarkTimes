using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;
    public Button InfoButton;

    public string GameSceneName;
    public string AboutSceneName;
    
    void Start()
    {
        PlayButton.onClick.AddListener(PlayGame);
        ExitButton.onClick.AddListener(ExitGame);
        InfoButton.onClick.AddListener(AboutGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void AboutGame()
    {
        SceneManager.LoadScene(AboutSceneName);
    }
}
