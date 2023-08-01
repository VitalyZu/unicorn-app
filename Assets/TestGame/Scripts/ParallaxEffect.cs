using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float _effectValue;
    [SerializeField] private Transform _followTarget;

    private Vector3 _prevPos;

    private void Start()
    {
        _prevPos = _followTarget.position;
    }

    private void LateUpdate()
    {
        var delta = _followTarget.position - _prevPos;
        _prevPos = _followTarget.position;

        transform.position = new Vector3(transform.position.x + delta.x * _effectValue, transform.position.y, transform.position.z);
    }
}
