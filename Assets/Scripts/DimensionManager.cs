using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DimensionManager : MonoBehaviour
{
    private bool isDark;

    [SerializeField]
    public GameObject lightBackground;
    [SerializeField]
    public GameObject darkBackground;


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
        }
    }

    void SwitchLayers()
    {
        int order = lightBackground.GetComponent<TilemapRenderer>().sortingOrder;
        lightBackground.GetComponent<TilemapRenderer>().sortingOrder = darkBackground.GetComponent<TilemapRenderer>().sortingOrder;
        darkBackground.GetComponent<TilemapRenderer>().sortingOrder = order;
    }
}
