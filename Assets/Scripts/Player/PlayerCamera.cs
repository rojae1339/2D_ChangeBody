using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCam;
    
    

    private void LateUpdate()
    {
        var playerTransform = gameObject.transform;
        
        _mainCam.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 2, -1);
    }
}