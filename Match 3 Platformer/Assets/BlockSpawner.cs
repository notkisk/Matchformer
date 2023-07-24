using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockSpawner : MonoBehaviour
{
    public GameObject []birds;
    public Transform[] slots;

    public Transform[] startingSlots;


    public Vector2 offset;

    public float spawnRate = 1f;
    float timeBtwSpawn;

    public GameObject []block;
    public int maxBirds;

    public GameObject magentaBlock_Tutorial, blueBlock_Tutorial;
    private void Awake()
    {
        //foreach (var slot in startingSlots)
        //{
        //    int randromSlot = Random.Range(0, block.Length);
        //    Instantiate(block[randromSlot], slot.position, Quaternion.identity);
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawn = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {

        var birds = FindObjectsOfType<Bird>(false);
        if (FindObjectOfType<DeathCheck>().hasLost == false)
        {
            if (birds.Length< maxBirds)
            {
                if (timeBtwSpawn <= 0f)
                {
                    timeBtwSpawn = spawnRate;
                    SpawnBlock();
                }
                else
                {
                    timeBtwSpawn -= Time.deltaTime;
                }
            }
          
        }


        if (FindObjectOfType<GameManager>().score<300)
        {
            spawnRate = 3f;
        }
        else if(FindObjectOfType<GameManager>().score>300 && FindObjectOfType<GameManager>().score<500)
        {
            spawnRate = 2.55f;
        }
        else if (FindObjectOfType<GameManager>().score>500&&FindObjectOfType<GameManager>().score<800)
        {
            spawnRate = 2f;
        }
        else if (FindObjectOfType<GameManager>().score>800 && FindObjectOfType<GameManager>().score<1200)
        {
            spawnRate = 1.5f;

        }
        else if (FindObjectOfType<GameManager>().score>1200)
        {
            spawnRate = 1.15f;
        }

    }

    void SpawnBlock()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2) return;
        int randSlot = UnityEngine.Random.Range(0, slots.Length);
        int randomBird;
        if (FindObjectOfType<GameManager>().score < 400) randomBird = UnityEngine.Random.Range(0, birds.Length - 1);
        else randomBird = UnityEngine.Random.Range(0, birds.Length);

        Transform slot = slots[randSlot];
        GameObject _birdToSpawn = birds[randomBird];

        if (slot.GetComponent<Slot>().isOccupied==false)
        {
            GameObject _bird = Instantiate(_birdToSpawn, slot.position + (Vector3)offset, Quaternion.identity);
            _bird.GetComponent<Bird>().Init(slot);
        }
        else
        {
            foreach (var s in slots)
            {
                if (s.GetComponent<Slot>().isOccupied == false)
                {
                    GameObject _bird = Instantiate(_birdToSpawn, s.position + (Vector3)offset, Quaternion.identity);
                    _bird.GetComponent<Bird>().Init(s);
                    break;
                }
            }
        }
     
    }

    public void SpawnBlueBlock()
    {
        int randSlot = UnityEngine.Random.Range(0, slots.Length);
        //int randomBird = UnityEngine.Random.Range(0, birds.Length);
        Transform slot = slots[randSlot];
        GameObject _birdToSpawn = blueBlock_Tutorial;

        if (slot.GetComponent<Slot>().isOccupied == false)
        {
            GameObject _bird = Instantiate(_birdToSpawn, slot.position + (Vector3)offset, Quaternion.identity);
            _bird.GetComponent<Bird>().Init(slot);
        }
    }

    public void SpawnMagentaBlock()
    {
        int randSlot = UnityEngine.Random.Range(0, slots.Length);
        //int randomBird = UnityEngine.Random.Range(0, birds.Length);
        Transform slot = slots[randSlot];
        GameObject _birdToSpawn = magentaBlock_Tutorial;

        if (slot.GetComponent<Slot>().isOccupied == false)
        {
            GameObject _bird = Instantiate(_birdToSpawn, slot.position + (Vector3)offset, Quaternion.identity);
            _bird.GetComponent<Bird>().Init(slot);
        }
    }

    void RandomiseArray(Transform[]myArray )
    {
        
        System.Random random = new System.Random();
        random.Next(myArray.Length);
        myArray = myArray.OrderBy(x => random.Next()).ToArray();
    }
}
