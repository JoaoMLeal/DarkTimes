using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStopController : MonoBehaviour
{

    [SerializeField]
    private float stopTime;

    private float timeLeft;
    private float oldSpeed;
    private GameObject darkPlayer;

    // Start is called before the first frame update
    void Start()
    {
        oldSpeed = 0f;
        darkPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft != 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                foreach (BolderController controller in GameManager.instance.GetBolderControllers())
                    controller.Speed = GameManager.instance.BolderSpeed();

                darkPlayer.GetComponent<DarkSelfController>().speed = oldSpeed;
                timeLeft = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DarkPlayer"))
        {
            timeLeft = stopTime;
            BolderController[] controllers = GameManager.instance.GetBolderControllers();
            foreach (BolderController controller in controllers)
                controller.Speed = 0f;

            darkPlayer = collision.gameObject;
            oldSpeed = darkPlayer.GetComponent<DarkSelfController>().speed;
            darkPlayer.GetComponent<DarkSelfController>().speed = 0f;  
        }
    }
}
