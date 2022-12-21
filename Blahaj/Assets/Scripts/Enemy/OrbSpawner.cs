using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbSpawner : MonoBehaviour
{

    [SerializeField] private GameObject RedOrb;
    [SerializeField] private GameObject YellowOrb;
    [SerializeField] private GameObject PurpleOrb;
    [SerializeField] private int numOrbsDropped;

    public void SpawnOrbs()
    {
        for (int i = 0; i < numOrbsDropped; i++)
            {
                int randomNumber = UnityEngine.Random.Range(0, 3);
                
                Vector3 spawnPosition = new Vector3(transform.position.x + UnityEngine.Random.Range(-2.0f, 2.0f), 
                    transform.position.y + UnityEngine.Random.Range(-2.0f, 2.0f), 
                    transform.position.z + UnityEngine.Random.Range(-2.0f, 2.0f));
                while (DetectCollideWall(spawnPosition))
                {
                    spawnPosition = new Vector3(transform.position.x + UnityEngine.Random.Range(-2.0f, 2.0f), 
                    transform.position.y + UnityEngine.Random.Range(-2.0f, 2.0f), 
                    transform.position.z + UnityEngine.Random.Range(-2.0f, 2.0f));
                }   
                switch (randomNumber)
                {
                    case (0):
                        Instantiate(RedOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
                        break;
                    case (1):
                        Instantiate(YellowOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
                        break;
                    case (2):
                        Instantiate(PurpleOrb, spawnPosition, new Quaternion(0f, 0f, 0f, 0f));
                        break;
                }
        }
    }

    private bool DetectCollideWall(Vector3 pos) 
    {        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, 0f);
        foreach (Collider2D col in hitColliders) 
        {
            if (col.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }
}