using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 2f;
    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;
    private Animator _anim;

    [SerializeField]
    private float _fireRate = 3.0f;
    private float _canFire = -1f;

    private float timeLeft;
    private Transform transformPlayer;


    void Start()
    {

        _player = GameObject.Find("Player").GetComponent<Player>();

        transformPlayer = _player.transform;

        // transformPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
    }


    void Update()
    {
        CalulateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemylaser();
            }
        }
    }
    public void CalulateMovement()
    {
        if (transformPlayer != null)
        {

            if (transform.position.y <= -5.0f || transform.position == null)
            {
                float randomX = Random.Range(-8f, 8f);
                transform.position = new Vector3(randomX, 7, 0);
            }

       
        if (transform.position.y <= -2f)
        {
                transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

            }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transformPlayer.position, _enemySpeed * Time.deltaTime);

        }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(this.gameObject, 1f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1f);
        }
    }
}






