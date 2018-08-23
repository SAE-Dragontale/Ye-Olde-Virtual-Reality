using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightCoordinator : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    [SerializeField]private List<GameObject> _enemies;
    [SerializeField]private List<GameObject> _attackingEnemy;


    [SerializeField] private List<GameObject> _spawnPoints;
    [SerializeField] private GameObject _enemyPrefab;


    [Tooltip("The maximum number of enemies that can be spawned on the scene at the same time")]
    [SerializeField] private int _maxEnemies = 10;

    [Tooltip("The maximum number of enemies that can be in an attacking state at the same time")]
    [SerializeField] private int _maxAttack = 3;


    private float _spawnTimer = 0.0f;
    private float _attackTimer = 5.0f;

    private int _killCount;

    [SerializeField] private GameObject _gameOverMenu;

    [SerializeField] private gameScoreController _score = null;


	void Awake()
	{
		FindObjectOfType<TeleportVive> ().enabled = false;
	}

    // Use this for initialization
    void Start()
    {
		
        _gameOverMenu.SetActive(false);
        _enemies = new List<GameObject>();
        _attackingEnemy = new List<GameObject>();
        GetComponent<AudioSource>().Play();
    }
	
    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        _attackTimer -= Time.deltaTime;
    }

    void LateUpdate()
    {
        if (_attackTimer <= 0 && (_attackingEnemy.Count < _maxAttack + (_killCount/10)) && _enemies.Count > 0)
        {
            SignalAttack();
        }

        if (_spawnTimer <= 0.0f)
        {
            SpawnEnemy();
        }
    }


    /// <summary>
    /// Spawns an enemy between any of the list of _spawnPoints
    /// </summary>
    private void SpawnEnemy()
    {
        if ((_enemies.Count + _attackingEnemy.Count) < _maxEnemies + (_killCount/10))
        {
            // continue spawning enemies every 5-10 seconds until the maximum count has been reached

            GameObject spawned = Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform);

            spawned.GetComponent<EnemyAIManager>().CoordinatorInitialize(_target, this);
            float scaleChange = Random.Range(1.0f, 1.5f);
            spawned.transform.localScale = new Vector3(scaleChange, scaleChange, scaleChange);
            _enemies.Add(spawned);
            _spawnTimer = Random.Range(5.0f, 10.0f);
        }
    }

    public void ConnectScoreBoard(gameScoreController inScore)
    {
        _score = inScore;
    }

    /// <summary>
    /// Signals an enemy to attack
    /// </summary>
    private void SignalAttack()
    {
        int i = Random.Range(0, _enemies.Count);

        if (_enemies[i].GetComponent<EnemyAIManager>().SignalAttack())
        {
            _attackingEnemy.Add(_enemies[i]);
            _enemies.Remove(_enemies[i]);
            _enemies.TrimExcess();
        }

        _attackTimer = Random.Range(1.0f, 3.0f);
    }


    /// <summary>
    /// Called by an enemy to receive their next instruction to attack
    /// </summary>
    /// <returns>The next instruction in int value</returns>
    public int GetNextInstruction(GameObject inEnemy)
    {
        int nextInstructionIndex = Random.Range(0, 2);

        if (nextInstructionIndex == 0)
        {
            _attackingEnemy.Remove(inEnemy);
            _enemies.Add(inEnemy);
        }

        return nextInstructionIndex;
    }


    /// <summary>
    /// Called by an enemy when they are killed
    /// </summary>
    /// <param name="inEnemy">In enemey.</param>
    public void EnemyKilled(GameObject inEnemy)
    {
        _killCount++;

        if (_score != null)
        {
            _score.AddScore(1);
        }
        _enemies.Remove(inEnemy);
        _enemies.TrimExcess();

        _attackingEnemy.Remove(inEnemy);
        _attackingEnemy.TrimExcess();
    }

    public void GameOver()
    {
        foreach (GameObject enemy in _enemies)
        {
            enemy.GetComponent<EnemyAIManager>().GameOver();
        }

        foreach (GameObject enemy in _attackingEnemy)
        {
            enemy.GetComponent<EnemyAIManager>().GameOver();
        }

        _gameOverMenu.SetActive(true);
		this.enabled = false;
    }

}