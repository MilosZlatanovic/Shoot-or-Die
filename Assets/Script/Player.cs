using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 12f)]
    private float _speed = 4f;
    private float _speedMultiplier = 2;
    [SerializeField]
    [Range(0, 3f)]
    private float _fireRate = 0.2f;
    [SerializeField]
    private int _lives = 3;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldsActive = false;

    [SerializeField]
    private int _score;
    public int _bestScore;

    private UIManager _uIManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;


    void Start()
    {
        // transform.position = new Vector3();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        if (_uIManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        _audioSource.clip = _laserSoundClip;

        // Svae and Load data
        _bestScore = PlayerPrefs.GetInt("HigtScore", 0);
        _uIManager._bestScore.text = "Best Score: " + _bestScore;
    }

    void Update()
    {
        MovingPlayer();
        ScreenBounds();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();

        }
    }

    void MovingPlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 playerDirection = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(playerDirection * _speed * Time.deltaTime);
    }

    void ScreenBounds()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5.6f), 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.1f, 9.1f), transform.position.y, 0);
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;



        _uIManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlyerDeath();
            Destroy(this.gameObject);
        }
        EngineDamage(_lives);
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerRoutine());
    }

    IEnumerator TripleShotPowerRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uIManager.AddScore(_score, _bestScore);
        if (_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("HigtScore", _bestScore);
            _uIManager.AddScore(_score, _bestScore);
        }
    }

    void EngineDamage(int live)
    {
        if (live == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (live == 1)
        {
            _rightEngine.SetActive(true);
        }
    }
}



