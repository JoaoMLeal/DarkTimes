using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public AudioSource DoorOpenSound;

    [SerializeField]
    public GameObject door1;

    [SerializeField]
    public GameObject door2;

    [SerializeField]
    private Sprite openDoor;
    [SerializeField]
    private Sprite closedDoor;

    private bool onTrigger;

    // Start is called before the first frame update
    void Start()
    {
        onTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrigger && Input.GetKeyDown(KeyCode.E))
        {
            DoorOpenSound.Play();
            if (door1.CompareTag("DoorClosed"))
                OpenDoor(door1);
            else
                CloseDoor(door1);

            if (door2.CompareTag("DoorClosed"))
                OpenDoor(door2);
            else
                CloseDoor(door2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            onTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            onTrigger = false;
    }

    private void CloseDoor(GameObject door)
    {
        door.tag = "DoorClosed";
        door.GetComponent<SpriteRenderer>().sprite = closedDoor;
        door.GetComponent<Collider2D>().enabled = true;
    }

    private void OpenDoor(GameObject door)
    {
        door.tag = "DoorOpen";
        door.GetComponent<SpriteRenderer>().sprite = openDoor;
        door.GetComponent<Collider2D>().enabled = false;
    }
}
