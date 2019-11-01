﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<pool> pools;

    public GameObject Tree;
    public GameObject Rock;
    public GameObject FoodParent;
    public GameObject RockParent;
    public float x;
    public float z;
    [Range(0, 100)]
    public int numTrees;
    [Range(0, 100)]
    public int numRocks;
    public float SpawnOffset;



    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
			{
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = transform;
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    private void Update()
    {
        SpawnFromPool("Rock", Vector3.zero, Quaternion.identity);
    }

    void spawn()
    {
        
        //It works, That's all that matters
        //Don't spend too long trying to comprehend it
        for (int i = 0; i < numTrees; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * x / 2;
            randomDirection += new Vector3(0, 1, 0);
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, x / 2, 1);
            //GameObject TempTree = Instantiate(Tree, new Vector3(navHit.position.x, SpawnOffset, navHit.position.z), Quaternion.Euler(0f, Random.Range(0f, 359f), 0f));
            GameObject TempTree = SpawnFromPool("Tree", new Vector3(navHit.position.x, SpawnOffset, navHit.position.z), Quaternion.Euler(0f, Random.Range(0f, 359f), 0f));
            //TempTree.transform.parent = FoodParent.transform;
            //TempTree.transform.localScale = Vector3.one * Random.Range(0.75f, 2f);
            //TempTree.name = "Tree" + (i + 1);
        }
        for (int i = 0; i < numRocks; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * x / 2;
            randomDirection += new Vector3(0, 1, 0);
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, x / 2, 1);
            //GameObject TempRock = Instantiate(Rock, new Vector3(navHit.position.x, SpawnOffset, navHit.position.z), Quaternion.Euler(Random.Range(0f, 359f), Random.Range(0f, 359f), Random.Range(0f, 359f)));
            GameObject TempRock = SpawnFromPool("Rock", new Vector3(navHit.position.x, SpawnOffset, navHit.position.z), Quaternion.Euler(Random.Range(0f, 359f), Random.Range(0f, 359f), Random.Range(0f, 359f)));
            TempRock.transform.parent = RockParent.transform;
            TempRock.name = "Rock" + (i + 1);

        }
    }


}