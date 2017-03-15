/*
 * AVRToolkit
 * EnemySpawner
 *
 * Easy spawning of enemies around your player, either
 * at a random (predefined) location, or at a point
 * furthest/closest your player.
 *
 * Version:
 *   1.0
 *
 * Authors:
 *   Daniel Love <AVRToolkit@daniellove.net>
 */

using UnityEngine;

namespace AVRToolkit.EnemySpawner
{
	public class EnemySpawner : MonoBehaviour
	{
		private GameObject[] _locations;

		[SerializeField]
		private GameObject _enemyPrefab;

		[SerializeField]
		private string _playerTag;

		void Awake()
		{
			_locations = GameObject.FindGameObjectsWithTag("Enemy Spawn Location");
		}

		public void Spawn(SpawnType type)
		{
			GameObject location = null;

			switch (type)
			{
				case SpawnType.Random:
					location = _locations[Random.Range(0, _locations.Length)];
					break;

				case SpawnType.ClosestToPlayer:
					location = FindSpawnLocation(SearchDistance.Closest);
					break;

				case SpawnType.FurthestFromPlayer:
					location = FindSpawnLocation(SearchDistance.Furthest);
					break;
			}

			if (location != null) {
				SpawnAt(location);
			}
		}

		void SpawnAt(GameObject locationObject)
		{
			if (_enemyPrefab == null) {
				Debug.Log("AVR.EnemySpawner.enemyPrefab = null. Can't spawn enemy.");
				return;
			}

			EnemySpawnLocation location = locationObject.GetComponent<EnemySpawnLocation>();
			Point point = location.SpawnPoint();
			Instantiate(_enemyPrefab, point.position,point.rotation);
		}

		enum SearchDistance
		{
			Closest,
			Furthest
		}

		GameObject FindSpawnLocation(SearchDistance search)
		{
			if (_playerTag == null) {
				Debug.Log("AVR.EnemySpawner.playerTag = null. Can't spawn closest to player, if we can't find player via tag.");
				return null;
			}

			bool isClosest = (search == SearchDistance.Closest);

			GameObject player = GameObject.FindGameObjectsWithTag(_playerTag)[0];
			Vector3 playersPosition = player.transform.position;
			
			float distance = isClosest ? Mathf.Infinity : 0;

			GameObject bestLocation = null;
			foreach (GameObject location in _locations)
			{
				float currentDistance = Vector3.Distance(playersPosition, location.transform.position);
				
				bool isMatch = (currentDistance < distance);
				if (isMatch == isClosest)
				{
					bestLocation = location;
					distance = currentDistance;
				}
			}
			
			if (bestLocation == null) {
 				Debug.Log("AVR.EnemySpawner.FindFurthest = null.");
			}

			return bestLocation;
		}
	}

	public enum SpawnType
	{
		Random,
		ClosestToPlayer,
		FurthestFromPlayer
	}
}
