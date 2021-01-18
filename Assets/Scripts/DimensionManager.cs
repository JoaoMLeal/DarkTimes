using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DimensionManager : MonoBehaviour
{

    private static DimensionManager _instance;
    public static DimensionManager instance { get { return _instance; } }

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

    private bool isDark;

    [SerializeField]
    public GameObject lightBackground;
    [SerializeField]
    public GameObject darkBackground;

    public bool IsDark()
    {
        return isDark;
    }

    // Start is called before the first frame update
    void Start()
    {
        isDark = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwitchLayers();
            if (!isDark)
                isDark = true;
        }
    }

    void SwitchLayers()
    {
        int order = lightBackground.GetComponent<TilemapRenderer>().sortingOrder;
        lightBackground.GetComponent<TilemapRenderer>().sortingOrder = darkBackground.GetComponent<TilemapRenderer>().sortingOrder;
        darkBackground.GetComponent<TilemapRenderer>().sortingOrder = order;
    }
}
