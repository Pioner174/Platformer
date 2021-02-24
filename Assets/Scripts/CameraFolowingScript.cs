using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowingScript : MonoBehaviour
{
    [SerializeField] private Transform camT;

    void LateUpdate() {
        camT.position = new Vector3(transform.position.x, transform.position.y, camT.position.z);
    }
}
