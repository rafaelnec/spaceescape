using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class Scene01 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerBody;

    public void OnLoadScene()
    {
        player.GetComponent<PlayerMovementController>().enabled = false;
    }

    public void OnUnloadScene()
    {
        player.GetComponent<PlayerMovementController>().enabled = true;

        Transform transformPLayer = player.GetComponent<Transform>();
        transformPLayer.position = Vector3.zero;
        transformPLayer.rotation = Quaternion.identity;
        
        Transform transform = playerBody.GetComponent<Transform>();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}

