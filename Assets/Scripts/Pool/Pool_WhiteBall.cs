using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_WhiteBall : MonoBehaviour
{
    //Variables 
    float cueOffsetX = -3.4f;
    internal bool cueSpawned = false;

    //Components
    public GameObject cue;
    Pool_Ball ballScript;

    //Start
    void Start()
    {
        ballScript = GetComponent<Pool_Ball>();

        SpawnCue();
    }

    //Update
    void Update()
    {
        //respawn cue
        if (!ballScript.rolling && !cueSpawned)
        {
            SpawnCue();
        }
    }

    //Spawn Cue
    void SpawnCue()
    {
        Vector3 cueSpawnPosition = new Vector3(transform.position.x + cueOffsetX, transform.position.y, transform.position.z);
        GameObject inst = Instantiate(cue, cueSpawnPosition, transform.rotation);
        inst.GetComponent<Pool_Cue>().whiteBall = gameObject;

        cueSpawned = true;
    }
}
