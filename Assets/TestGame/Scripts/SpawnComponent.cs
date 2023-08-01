using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform _position;
    [SerializeField] private GameObject _prefab;

    public void Spawn()
    {
        Instantiate(_prefab, _position.position, Quaternion.identity);
    }
}
