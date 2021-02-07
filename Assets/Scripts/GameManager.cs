using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    private float bolderSpeed;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bolderSpeed = GetBolderControllers()[0].Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BolderController[] GetBolderControllers()
    {
        GameObject[] bolders = GameObject.FindGameObjectsWithTag("Bolder");
        BolderController[] controllers = new BolderController[bolders.Length];
        for (int i = 0; i < controllers.Length; i++)
            controllers[i] = bolders[i].GetComponent<BolderController>();
        return controllers;
    }

    public float BolderSpeed()
    {
        return bolderSpeed;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void Victory()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}
