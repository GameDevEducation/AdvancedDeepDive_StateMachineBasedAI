using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemyDetected(GameObject enemy)
    {
        Debug.Log("[" + gameObject.name + "] Detected " + enemy.name);
    }

    public void OnEnemyLost(GameObject enemy)
    {
        Debug.Log("[" + gameObject.name + "] Lost " + enemy.name);
    }

    public void OnSoundHeard(Vector3 location)
    {
        Debug.Log("[" + gameObject.name + "] heard something at " + location);
    }
}
