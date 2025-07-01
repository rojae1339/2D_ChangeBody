using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerController _playerController;
    
    void Awake()
    {
        _playerMove = gameObject.AddComponent<PlayerMove>();
        _playerController = gameObject.AddComponent<PlayerController>();
        _playerController.Init(_playerMove);
    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        _playerController.OnMove(callback);
    }
}
