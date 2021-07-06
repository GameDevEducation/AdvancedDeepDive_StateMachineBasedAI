using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class DetectableEnemy : MonoBehaviour
{
    [SerializeField] UnityEvent<GameObject> OnEnemyDetected = new UnityEvent<GameObject>();
    [SerializeField] UnityEvent<GameObject> OnEnemyLost = new UnityEvent<GameObject>();

    MeshRenderer LinkedMR;

    // Start is called before the first frame update
    void Start()
    {
        // start hidden
        LinkedMR = GetComponent<MeshRenderer>();
        LinkedMR.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMakeVisible()
    {
        LinkedMR.enabled = true;
        OnEnemyDetected.Invoke(gameObject);
    }

    public void OnMakeHidden()
    {
        LinkedMR.enabled = false;
        OnEnemyLost.Invoke(gameObject);
    }
}
