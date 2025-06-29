using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    
    public Vector3 MoveDirection { get; set; } = Vector3.zero;

    private void Update()
    {
        transform.position += MoveDirection * (_moveSpeed * Time.deltaTime);
    }
}
