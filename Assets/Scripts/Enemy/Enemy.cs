using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    ChasingMonster,
    FrozenKitty,
    FleeingKitty,
    ChasingKitty
}

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float fleeDistance = 5f;

    [Header("Speeds")]
    [SerializeField] float monsterMoveSpeed = 0.75f;
    [SerializeField] float kittyMoveSpeed = 0.45f;

    [Header("Sequence Timers")]
    [SerializeField] float freezeDuration = 1f;
    [SerializeField] float fleeDuration = 10f;
    [SerializeField] float kittyChaseDuration = 3f;

    NavMeshAgent agent;
    [SerializeField] private EnemyState currentState = EnemyState.ChasingMonster;
    private Coroutine activeReactionRoutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = monsterMoveSpeed;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.ChasingMonster:
            case EnemyState.ChasingKitty:
                agent.isStopped = false;
                agent.SetDestination(target.position);
                break;

            case EnemyState.FrozenKitty:
                break;

            case EnemyState.FleeingKitty:
                agent.isStopped = false;
                Vector3 directionAway = transform.position - target.position;
                Vector3 fleeTarget = transform.position + directionAway.normalized * fleeDistance;
                agent.SetDestination(fleeTarget);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flashlight") && (currentState == EnemyState.ChasingMonster || currentState == EnemyState.ChasingKitty))
        {
            if (activeReactionRoutine != null)
            {
                StopCoroutine(activeReactionRoutine);
            }
            activeReactionRoutine = StartCoroutine(LightReactionSequence());
        }
    }

    private IEnumerator LightReactionSequence()
    {
        //Debug.Log("I am here");
        currentState = EnemyState.FrozenKitty;
        agent.isStopped = true;

        // TODO: Tell Animator to play Kitty Transform/Shocked animation!

        yield return new WaitForSeconds(freezeDuration);

        currentState = EnemyState.FleeingKitty;
        agent.isStopped = false;
        agent.speed = kittyMoveSpeed;

        // TODO: Tell Animator to play Kitty Running animation!

        yield return new WaitForSeconds(fleeDuration);

        currentState = EnemyState.ChasingKitty;

        yield return new WaitForSeconds(kittyChaseDuration);

        currentState = EnemyState.ChasingMonster;
        agent.speed = monsterMoveSpeed;

        // TODO: Tell Animator to play Monster Transform animation!
        activeReactionRoutine = null;
    }
}