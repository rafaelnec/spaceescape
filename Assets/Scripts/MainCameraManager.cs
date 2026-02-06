using Unity.Cinemachine;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EnableCutCamera()
    {
        Camera cam = Camera.main;
        CinemachineBrain cameraBrain = cam.GetComponent<CinemachineBrain>();
        if (cameraBrain != null) cameraBrain.DefaultBlend.Time = 0f;
    }
}
