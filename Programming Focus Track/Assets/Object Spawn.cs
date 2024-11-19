using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField] GameObject SpawnObject;
    [SerializeField] float AmountToSpawn;
    [SerializeField] float RadiusToSpawnInto;
    [SerializeField] float MinDisToEachObj;
    [SerializeField] float MaxAttemptsToSpawn;
    [SerializeField, Range(0f, 1f)] float RandomSize = 0.1f;
    private List<GameObject> spawnList = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < AmountToSpawn; i++)
        {
            SpawnObj(SpawnObject);
        }
    }

    private void SpawnObj(GameObject obj)
    {
        Vector3 spawnPos = position();
        for (int i = 0; i <= MaxAttemptsToSpawn; i++)
        {

            if (i >= MaxAttemptsToSpawn)
            {
                Debug.Log("Falied to spawn all");
                AmountToSpawn = 0;
                return;
            }

            if (CheckDistance(spawnPos))
            {
                break;
            }
            else
            {
                spawnPos = position();
            }

            
        }
        

        GameObject spawn = GameObject.Instantiate(obj, spawnPos, Quaternion.identity);
        spawn.transform.localScale = spawn.transform.localScale * (1 + Random.Range(-RandomSize, RandomSize));
        spawnList.Add(spawn);
    }

    private Vector3 position()
    {
        Vector3 pos = new Vector3(transform.position.x + Random.Range(-RadiusToSpawnInto, RadiusToSpawnInto), transform.position.y, transform.position.z + Random.Range(-RadiusToSpawnInto, RadiusToSpawnInto));

        pos = new Vector3(pos.x * Mathf.Abs(pos.normalized.x), pos.y, pos.z * Mathf.Abs(pos.normalized.z));

        return pos;
    }

    private bool CheckDistance(Vector3 pos)
    {
        bool bol = true;
        foreach (GameObject gameObject in spawnList)
        {
            if (Vector3.Distance(pos, gameObject.transform.position) < MinDisToEachObj)
            {
                bol = false; break;
            }
        }
        return bol;
    }
}
