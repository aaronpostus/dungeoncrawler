using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPrefabRegistry", menuName = "Game/Enemy Prefab Registry")]
public class EnemyPrefabRegistry : ScriptableObject
{
    [System.Serializable]
    public class EnemyPrefab
    {
        public string type;
        public GameObject prefab;
    }

    public EnemyPrefab[] enemyPrefabs;

    public GameObject GetPrefabByType(string type)
    {
        foreach (var enemyPrefab in enemyPrefabs)
        {
            if (enemyPrefab.type == type)
            {
                return enemyPrefab.prefab;
            }
        }

        Debug.LogWarning($"EnemyPrefabRegistry: No prefab found for type {type}.");
        return null;
    }
}

