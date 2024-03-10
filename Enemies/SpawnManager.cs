
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int spawnRange = 9;
    
    public int enemyCount = 1;
    public GameObject[] enemyPrefab;
    public GameObject powerUpPrefabs;
    public bool go = true;
    
    private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(powerUpPrefabs, GenerateSpanwPosition(), powerUpPrefabs.transform.rotation);
        SpawnEnemyWave(Parameters.enemyWaves);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (Parameters.level == 1)
        {
            if (enemyCount < 4)
            {
                Parameters.enemyWaves++;
                SpawnEnemyWave(Parameters.enemyWaves);
            }
        }

        if (Parameters.level == 2 && !Parameters.tregua)
        {
           goToHim();
        }

        if (Parameters.level == 3 && !Parameters.tregua)
        {
            if (enemyCount < 5)
            {
                goToHim();
            }
        }



    }


    Vector3 GenerateSpanwPosition()
    {
        if (Parameters.level == 1)
        {
            float startX = Random.Range(-spawnRange, spawnRange);
            float startZ = Random.Range(-spawnRange, spawnRange);
            spawnPos = new Vector3(startZ, 0, startX);
        }
        else if (Parameters.level == 2)
        {
            float startX = Random.Range(216, 281);
            float startZ = Random.Range(-48.2f, 20.8f);
            spawnPos = new Vector3(startX, 80f, startZ);
           
        }
        else if (Parameters.level == 3)
        {
            float startX = Random.Range(-200, -150);
            float startZ = Random.Range(0,138);
            spawnPos = new Vector3(startX, 84.52f, startZ);
            
        }
            return spawnPos;
    }


    void SpawnEnemyWave(int wave)
    {
        Debug.Log("Oleada: " + wave);
        Debug.Log("Nivel: " + Parameters.level);
        Debug.Log("Tregua: " + Parameters.tregua);

        int numEnemies = wave * wave; // Calcula el cuadrado del número de oleadas

        //if (!Parameters.GAMEOVER)
        //{
            if(Parameters.level == 1 && !Parameters.tregua)
            {
                for (int i = 0; i < numEnemies; i++)
                {
                    int posArray = Random.Range(0, 2);
                    Vector3 posSpawn = new Vector3(28.58f, 0, -0.5f);
                    GameObject newEnemy = Instantiate(enemyPrefab[posArray], GenerateSpanwPosition(), transform.rotation);

                    // Destruir el enemigo después de un cierto tiempo (por ejemplo, 10 segundos)
                    Destroy(newEnemy, 7f);
                };

            }else if(Parameters.level == 2 && !Parameters.tregua)
            {
            Debug.Log("Dentro de nivel 2");
                for (int i = 0; i < numEnemies; i++)
                {
                    int posArray = Random.Range(0, enemyPrefab.Length);
                    Vector3 posSpawn = new Vector3(158f, 38, 6f);
                    GameObject newEnemy = Instantiate(enemyPrefab[3], GenerateSpanwPosition(), transform.rotation);

                    // Destruir el enemigo después de un cierto tiempo (por ejemplo, 10 segundos)
                    //Destroy(newEnemy, 9f);
                };


            }
            else if (Parameters.level == 3 && !Parameters.tregua)
            {
                for (int i = 0; i < numEnemies; i++)
                {
                    int posArray = Random.Range(0, enemyPrefab.Length);
                    Vector3 posSpawn = new Vector3(-208f, 38, 6f);
                    GameObject newEnemy = Instantiate(enemyPrefab[4], GenerateSpanwPosition(), transform.rotation);

                    // Destruir el enemigo después de un cierto tiempo (por ejemplo, 10 segundos)
                    //Destroy(newEnemy, 9f);
                }
            }  
        //}
    }


    private void goToHim()
    {
        if (go)
        {
            go = false;
            Debug.Log("Go to him");
            Parameters.enemyWaves++;
            SpawnEnemyWave(Parameters.enemyWaves);
            StartCoroutine(tem());
        }
    }

    IEnumerator tem()
    {
        Debug.Log("Esperando");
        yield return new WaitForSeconds(7f);
        go = true;
    }



}
