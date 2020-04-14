using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
    float size = 40;

    public bool spawnEnemies = true;

    public Transform enemy;

    private int wave = 0;

    public float rate = 3;
    public int count = 1;

    public int enemiesAlive;

    float timeBetweenWaves = 5;

    bool startNewWave = true;

    public Transform portal;

    public bool portalIsActive = true;

    GameManager manager;

    private void Start() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update() {

        if (timeBetweenWaves <= 0){
            StartCoroutine("SpawnEnemy");
            timeBetweenWaves = 7f;
            startNewWave = false;
            portalIsActive = false;
            
        }

        if(enemiesAlive <= 0){
            if(startNewWave)
                timeBetweenWaves -= Time.deltaTime;
                
            Debug.Log("Wave: " + wave);
            if(!portalIsActive){
                Instantiate(portal, new Vector3(20, 3, 38), Quaternion.identity);
                portalIsActive = true;
            }
        }

        
    }

    IEnumerator SpawnEnemy(){

        enemiesAlive = count;
        manager.wave += 1;
        for(int i = 0; i < count; i++){
            Transform spawnEnemy = Instantiate(enemy, new Vector3(Random.Range(0,size), 0f, Random.Range(0, size)), Quaternion.identity);

            yield return new WaitForSeconds(3);
            spawnEnemy.GetComponentInChildren<Animator>().enabled = false;
            yield return new WaitForSeconds(2f / rate); 
        }
        
        
    }

    
}
