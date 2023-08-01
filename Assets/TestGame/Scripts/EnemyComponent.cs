using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation;
    [SerializeField] private float _maxSpeed;
    [Space]
    [SerializeField] private SpawnComponent _explosionSpawn;

    private float _speed;
    private bool _isWalking;

    private void Start()
    {
        _speed = Random.Range(2f, _maxSpeed);
        _animation.AnimationState.Start += OnStartAnimation;
    }

    private void Update()
    {
        if (_isWalking)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }

    public void SetAnimationByName(string name)
    {
        if(_animation.AnimationName != name)
        _animation.AnimationState.SetAnimation(0, name, true);
    }

    public void SetWalk()
    {
        if (_isWalking) return;

        _animation.AnimationState.SetAnimation(0, "angry", true);
        _animation.AnimationState.AddAnimation(0, "run", true, 1f);
    }

    public void Hit(GameObject go)
    {
        PlayerComponent player = go.GetComponent<PlayerComponent>();

        if (player != null)
        {
            player.Die();
            _animation.AnimationState.SetAnimation(0, "win", true);
        }
    }

    public void Die()
    {
        _explosionSpawn.Spawn();
        Destroy(gameObject);
    }

    private void OnStartAnimation(TrackEntry track)
    {
        if (track.Animation.Name == "run")
        {
            _isWalking = true;
        }
        else _isWalking = false;
    }

    private void OnDestroy()
    {
        _animation.AnimationState.Start -= OnStartAnimation;
    }
}
