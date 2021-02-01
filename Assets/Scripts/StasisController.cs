using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisController : MonoBehaviour
{

    private bool active = false;

    [SerializeField]
    private float speed;

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
            foreach (BolderController controller in GameManager.instance.GetBolderControllers())
                controller.Speed = GameManager.instance.BolderSpeed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DarkPlayer"))
        {
            BolderController[] controllers = GameManager.instance.GetBolderControllers();
            foreach (BolderController controller in controllers)
                controller.Speed = speed;
            active = true;
        }
    }
}
