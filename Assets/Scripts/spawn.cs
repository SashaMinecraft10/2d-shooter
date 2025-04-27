using UnityEngine;

public class spawn : MonoBehaviour
{
    [SerializeField]Transform[] spawnpoints;
    [SerializeField] GameObject enemyPref;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("EnemySpawn", 10, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EnemySpawn()
    {
        int randomIndex = Random.Range(0,spawnpoints.Length);
        Instantiate(enemyPref, spawnpoints[randomIndex].position, Quaternion.identity);
    }
}
