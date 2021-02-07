using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DimensionManager : MonoBehaviour
{

    private static DimensionManager _instance;
    public static DimensionManager instance { get { return _instance; } }

    private bool isDark;

    [SerializeField]
    public GameObject player;
    [SerializeField]
    public GameObject darkPlayerPrefab;
    private GameObject darkPlayer;

    [SerializeField]
    public GameObject lightBackground;
    [SerializeField]
    public GameObject darkBackground;


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
        if (Input.GetMouseButtonDown(0) && !isDark)
        {
            isDark = true;
            player.GetComponent<LineRenderer>().enabled = false;
            SwitchLayers();
            TeleportPlayer();
            Camera.main.orthographicSize = 30;
        }
        else if (darkPlayer != null && darkPlayer.GetComponent<DarkSelfController>().finished)
        {
            player.GetComponent<LineRenderer>().enabled = true;
            isDark = false;
            Destroy(darkPlayer);
            SwitchLayers();
            Camera.main.orthographicSize = 20;
        }
    }

    void SwitchLayers()
    {
        int order = lightBackground.GetComponent<TilemapRenderer>().sortingOrder;
        lightBackground.GetComponent<TilemapRenderer>().sortingOrder = darkBackground.GetComponent<TilemapRenderer>().sortingOrder;
        darkBackground.GetComponent<TilemapRenderer>().sortingOrder = order;
    }

    void TeleportPlayer()
    {
        LineRenderer lineRenderer = player.GetComponent<LineRenderer>();
        player.transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        CreateDarkSelf(points);
        
    }

    void CreateDarkSelf(Vector3[] points)
    {
        darkPlayer = Instantiate(darkPlayerPrefab, player.transform.position, Quaternion.identity);
        darkPlayer.GetComponent<DarkSelfController>().points = points;
    }
}
