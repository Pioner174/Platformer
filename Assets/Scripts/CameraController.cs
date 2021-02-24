using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerController.transform.position.x, playerController.transform.position.y, transform.position.z);
    }
}
 