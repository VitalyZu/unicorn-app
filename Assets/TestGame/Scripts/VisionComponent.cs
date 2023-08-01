using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisionComponent : MonoBehaviour
{
    [SerializeField] private OnEnterVisionEvent _onEnter;
    [SerializeField] private string _tag;
    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.gameObject.CompareTag(_tag))
        {
            _onEnter?.Invoke(collision.gameObject);
        }
    }
}

[Serializable]
public class OnEnterVisionEvent : UnityEvent<GameObject>
{ }
