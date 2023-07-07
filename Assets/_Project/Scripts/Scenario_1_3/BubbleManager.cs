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
    private Transform[] spawnPositions;

    private Transform player;

    private Vector3 spawnPos;

    //Current Bubble Round
    int bubbleRound = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        SpawnBubbles(bubbleRound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBubbles(int round)
    {
        foreach(Sprite sprite in spriteContainer[round ].sprites)
        {
            //Get a random prefab
            GameObject bubbleToSpawn = BubblePrefabs[Random.Range(0, BubblePrefabs.Length)];

            //Get a random position
            FindSpawnPoint();

            //Spawn Bubble and make it child object of this object
            var bubble = GameObject.Instantiate(bubbleToSpawn, spawnPos, Quaternion.identity);
            bubble.transform.parent = gameObject.transform;

            //Add Sprite to Bubble
            bubble.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        }
    }

    public void NextRound()
    {
        //Delete all bubbles
        bubbleRound++;
        foreach (Transform child in transform) Destroy(child.gameObject);

        //Setting spawn positions to not occupied
        foreach (Transform spawnPos in spawnPositions) spawnPos.gameObject.GetComponent<SpawnPoint>().occupied = false;

        //Check if it was the forelast round
        if (bubbleRound == spriteContainer.Length - 1)
            FindObjectOfType<SceneFlow_Scenario_1_3>().SetLastRoundReached();
        //Check if it was the last round
        else if (bubbleRound == spriteContainer.Length)
        {
            FindObjectOfType<SceneFlow_Scenario_1_3>().SetAllBubbleRoundsDone();
            return;
        }

        //Spawn new bubbles
        SpawnBubbles(bubbleRound);
    }

    //Make Player able to pop all bubbles instead of just one
    public void AbleToPopAll()
    {
        foreach (Transform child in transform)
            child.GetComponent<BubbleBehavior_1_3>().popAll = true;
    }

    void FindSpawnPoint()
    {
        Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
        if (spawnPosition.gameObject.GetComponent<SpawnPoint>().occupied == true)
        {
            FindSpawnPoint();
        }
        else
        {
            spawnPos = spawnPosition.position;
            spawnPosition.gameObject.GetComponent<SpawnPoint>().occupied = true;
            return;
        }
    }
}
