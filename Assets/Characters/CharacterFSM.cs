using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFSM : CharacterBase
{
    public enum EState
    {
        Idle,
        Patrolling,
        HeardSomething,
        SawSomething
    }
    
    [SerializeField] EState CurrentState;

    [SerializeField] List<Transform> PatrolPoints;
    [SerializeField] float PatrolPointReachedThreshold = 0.5f;

    [SerializeField] float MovementSpeed = 2f;

    [SerializeField] float IdleMinTime = 5f;
    [SerializeField] float IdleMaxTime = 10f;

    [SerializeField] float ListenTime = 5f;
    
    float IdleTimeRemaining;
    float ListenTimeRemaining;

    int CurrentPatrolPoint;

    Vector3 LastHeardLocation;
    Transform LastSeenEnemy;

    // Start is called before the first frame update
    void Start()
    {
        SwitchToState(CurrentState);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    public override void OnEnemyDetected(GameObject enemy)
    {
        base.OnEnemyDetected(enemy);

        LastSeenEnemy = enemy.transform;
        SwitchToState(EState.SawSomething);
    }

    public override void OnEnemyLost(GameObject enemy)
    {
        base.OnEnemyLost(enemy);

        LastSeenEnemy = null;

        if (CurrentState == EState.SawSomething)
            SwitchToState(EState.Patrolling);
    }

    public override void OnSoundHeard(Vector3 location)
    {
        base.OnSoundHeard(location);

        LastHeardLocation = location;
        SwitchToState(EState.HeardSomething);
    }

    void SwitchToState(EState newState)
    {
        // initialise based on the state
        if (newState == EState.Idle)
        {
            IdleTimeRemaining = Random.Range(IdleMinTime, IdleMaxTime);
        }
        else if (newState == EState.Patrolling)
        {
            CurrentPatrolPoint = Random.Range(0, PatrolPoints.Count);
        }
        else if (newState == EState.HeardSomething)
        {
            // look at the sound source for a set time
            transform.rotation = Quaternion.LookRotation(LastHeardLocation - transform.position, Vector3.up);
            ListenTimeRemaining = ListenTime;
        }
        else if (newState == EState.SawSomething)
        {
            // look at the enemy
            transform.LookAt(LastSeenEnemy, Vector3.up);
        }

        // update the state and debug display
        CurrentState = newState;
        FeedbackUI.SetFeedbackText(CurrentState.ToString());
    }

    void UpdateState()
    {
        if (CurrentState == EState.Idle)
        {
            UpdateState_Idle();
        }
        else if (CurrentState == EState.Patrolling)
        {
            UpdateState_Patrolling();
        }
        else if (CurrentState == EState.HeardSomething)
        {
            UpdateState_HeardSomething();
        }
        else if (CurrentState == EState.SawSomething)
        {
            UpdateState_SawSomething();
        }
    }

    void UpdateState_Idle()
    {
        // update idle time remaining
        IdleTimeRemaining -= Time.deltaTime;

        // idle time completed?
        if (IdleTimeRemaining <= 0)
            SwitchToState(EState.Patrolling);
    }

    void UpdateState_Patrolling()
    {
        Vector3 vectorToPatrolPoint = PatrolPoints[CurrentPatrolPoint].position - transform.position;

        // reached patrol point?
        if (vectorToPatrolPoint.magnitude <= PatrolPointReachedThreshold)
        {
            // advance to the next point
            CurrentPatrolPoint = (CurrentPatrolPoint + 1) % PatrolPoints.Count;
        }

        // move towards the point
        transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].position, MovementSpeed * Time.deltaTime);

        // face the patrol point
        transform.LookAt(PatrolPoints[CurrentPatrolPoint], Vector3.up);
    }

    void UpdateState_HeardSomething()
    {
        ListenTimeRemaining -= Time.deltaTime;

        // nothing heard - patrol
        if (ListenTimeRemaining <= 0)
            SwitchToState(EState.Patrolling);
    }

    void UpdateState_SawSomething()
    {
        transform.LookAt(LastSeenEnemy, Vector3.up);        
    }
}
