using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerComponent _playerComponent;

    private void Start()
    {
        _playerComponent = GetComponent<PlayerComponent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerComponent.Shoot();
        }
    }
}
