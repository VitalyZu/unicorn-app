using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation;
    [SerializeField] private BoxCollider2D _vision;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _enemyMask;
    [Space]
    [SerializeField] private GameObject _prefabFire;
    [SerializeField] private SpawnComponent _fireSpawn;
    [SerializeField] private AppManager _manager;
    [Space]
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _shootClip;

    private Camera _camera;
    private bool _isWalking;
    private bool _isShooting;

    private void Start()
    {
        _camera = Camera.main;

        SetVision();

        _animation.AnimationState.SetAnimation(0, "idle", true);
        _animation.AnimationState.AddAnimation(0, "walk", true, 2f);

        _animation.AnimationState.Start += OnStartAnimationEvent;
    }

    private void Update()
    {
        if (_isWalking)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }   
    }

    public void SetAnimationByName(string name)
    {
        if (_animation.AnimationName != name)
            _animation.AnimationState.SetAnimation(0, name, true);
    }

    public void Shoot()
    {
        if (_isShooting) return;

        _isShooting = true;

        _audio.PlayOneShot(_shootClip);
        var shoot = _animation.AnimationState.SetAnimation(0, "shoot", false);
        shoot.Event += OnShootEvent;
        _animation.AnimationState.AddAnimation(0, "walk", true, 0);   
    }

    public void Die()
    {
        if (_animation.AnimationName == "loose") return;

        _animation.AnimationState.SetAnimation(0, "loose", false);
        StartCoroutine(StartLose());
    }

    private void OnShootEvent(TrackEntry trackEntry, Spine.Event e)
    {
        _fireSpawn.Spawn();

        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        Instantiate(_prefabFire, mousePos, Quaternion.identity);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 10f, _enemyMask);
        if (hit.collider != null)
        {
            EnemyComponent enemy = hit.collider.gameObject.GetComponent<EnemyComponent>();
            if (enemy != null) enemy.Die();
        }
    }

    private void OnStartAnimationEvent(TrackEntry track)
    {
        if (track.Animation.Name == "walk")
        {
            _isWalking = true;
            _isShooting = false;
        }
        else _isWalking = false;
    }

    private void SetVision()
    {
        float aspect = (float)Screen.width / Screen.height;
        float worldHeight = _camera.orthographicSize * 2;
        float worldWidth = worldHeight * aspect;

        _vision.size = new Vector2(worldWidth, 1f);
    }

    public void StartEnemies(GameObject go)
    {
        EnemyComponent enemyComponent = go.GetComponent<EnemyComponent>();
        if (enemyComponent != null) enemyComponent.SetWalk();
    }

    private IEnumerator StartLose()
    {
        yield return new WaitForSeconds(1f);
        _manager.ShowPanel(PanelType.Lose);
    }

    private void OnDestroy()
    {
        _animation.AnimationState.Start -= OnStartAnimationEvent;
    }
}
