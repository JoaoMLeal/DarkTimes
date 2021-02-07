using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AboutMenu : MonoBehaviour
{
    public string BackSceneName;

    public Button BackButton;

    void Start()
    {
        BackButton.onClick.AddListener(BackMenu);
    }
    
    private void BackMenu()
    {
        SceneManager.LoadScene(BackSceneName);
    }
}
