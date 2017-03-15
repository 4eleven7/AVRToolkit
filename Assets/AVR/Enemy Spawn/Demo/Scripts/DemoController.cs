using UnityEngine;
using AVRToolkit.EnemySpawner;

public class DemoController : MonoBehaviour
{
	private EnemySpawner _spawner;

	private bool _isSpawning = false;

	void Awake()
	{
		_spawner = GetComponent<EnemySpawner>();
	}

	void Update()
	{
		if (Input.GetKey("r"))
		{
			if (_isSpawning) return;
			_isSpawning = true;

			_spawner.Spawn(SpawnType.Random);
		}
		else if (Input.GetKey("c"))
		{
			if (_isSpawning) return;
			_isSpawning = true;
			
			_spawner.Spawn(SpawnType.ClosestToPlayer);
		}
		else if (Input.GetKey("f"))
		{
			if (_isSpawning) return;
			_isSpawning = true;

			_spawner.Spawn(SpawnType.FurthestFromPlayer);
		}
		else
		{
			_isSpawning = false;
		}
	}
}
