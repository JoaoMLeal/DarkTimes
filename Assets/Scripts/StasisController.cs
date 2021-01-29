using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisController : MonoBehaviour
{

    private bool active = false;
    private float oldSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (active && !DimensionManager.instance.IsDark())
        {
            active = false;
            foreach (BolderController controller in GetBolderControllers())
                controller.Speed = oldSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DarkPlayer"))
        {
            BolderController[] controllers = GetBolderControllers();
            oldSpeed = controllers[0].Speed;
            foreach (BolderController controller in controllers)
                controller.Speed = 2f;
            active = true;
        }
    }

    private BolderController[] GetBolderControllers()
    {
        GameObject[] bolders = GameObject.FindGameObjectsWithTag("Bolder");
        BolderController[] controllers = new BolderController[bolders.Length];
        for (int i = 0; i < controllers.Length; i++)
            controllers[i] = bolders[i].GetComponent<BolderController>();
        return controllers;
    }
}
