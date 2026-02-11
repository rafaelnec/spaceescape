using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public GameObject doorPrefab;
    private Animator animator;
    private bool doorLock = true;
    private bool isDoorOpen = false;


    void Start ()
    {
        animator = doorPrefab.GetComponent<Animator>();
    }

    public void UnlockDoor()
    {
        doorLock = false;
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (!doorLock)
        {   
            animator.SetBool("Door", true);
            isDoorOpen = true;
        }
    }

    public void CloseDoor()
    {
        animator.SetBool("Door", false);
        isDoorOpen = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        OpenDoor();
    }

    public void OnTriggerStay(Collider other)
    {
        if(!isDoorOpen)
        {
            OpenDoor();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        CloseDoor();
    }

}
