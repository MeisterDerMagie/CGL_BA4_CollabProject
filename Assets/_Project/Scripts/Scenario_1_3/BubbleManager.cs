using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    //Struct with sprites for a round of bubble
    [System.Serializable]
    public struct SpriteContainer
    {
        public Sprite[] sprites;
    }

    [SerializeField]
    SpriteContainer[] spriteContainer;

    //All Bubble Prefabs
    [SerializeField]
    private GameObject[] BubblePrefabs;

    [SerializeField]
    private float minX, maxX, minY, maxY, minZ, maxZ;

    //Current Bubble Round
    int bubbleRound = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBubbles(bubbleRound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBubbles(int round)
    {
        foreach(Sprite sprite in spriteContainer[round - 1].sprites)
        {
            //Get a random prefab
            GameObject bubbleToSpawn = BubblePrefabs[Random.Range(0, BubblePrefabs.Length)];

            //Get a random position
            float xSpawnPos = Random.Range(minX, maxX);
            float ySpawnPos = Random.Range(minY, maxY);
            float zSpawnPos = Random.Range(minZ, maxZ);
            Vector3 spawnPos = new Vector3(xSpawnPos, ySpawnPos, zSpawnPos);

            //Spawn Bubble and make it child object of this object
            var bubble = GameObject.Instantiate(bubbleToSpawn, spawnPos, Quaternion.identity);
            bubble.transform.parent = gameObject.transform;

            //Add Sprite to Bubble
            bubble.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        }
    }

    public void NextRound()
    {
        bubbleRound++;
        foreach (Transform child in transform) Destroy(child.gameObject);
        SpawnBubbles(bubbleRound);
    }
}
