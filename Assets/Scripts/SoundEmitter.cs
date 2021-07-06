using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector3> OnSoundHeard = new UnityEvent<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEmitSound()
    {
        OnSoundHeard.Invoke(transform.position);
    }
}
