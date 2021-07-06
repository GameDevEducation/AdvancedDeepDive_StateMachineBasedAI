using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected CharacterUIInterface FeedbackUI;

    public virtual void OnEnemyDetected(GameObject enemy)
    {
        Debug.Log("[" + gameObject.name + "] Detected " + enemy.name);

        Unity.VisualScripting.CustomEvent.Trigger(gameObject, "OnEnemyDetected", new object[1] {enemy});
    }

    public virtual void OnEnemyLost(GameObject enemy)
    {
        Debug.Log("[" + gameObject.name + "] Lost " + enemy.name);

        Unity.VisualScripting.CustomEvent.Trigger(gameObject, "OnEnemyLost", new object[1] {enemy});
    }

    public virtual void OnSoundHeard(Vector3 location)
    {
        Debug.Log("[" + gameObject.name + "] heard something at " + location);
        
        Unity.VisualScripting.CustomEvent.Trigger(gameObject, "OnSoundHeard", new object[1] {location});
    }
}
