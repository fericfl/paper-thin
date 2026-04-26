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
    Animator animator;
    private string currentAnimState;
    [SerializeField] private EnemyState currentState = EnemyState.ChasingMonster;
    private Coroutine activeReactionRoutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = monsterMoveSpeed;
        animator = GetComponent<Animator>();
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
        UpdateAnimation();
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
        currentState = EnemyState.FrozenKitty;
        agent.isStopped = true;
        AudioManager.Instance.PlaySFX("cat_long_meow");
        yield return new WaitForSeconds(freezeDuration);

        currentState = EnemyState.FleeingKitty;
        agent.isStopped = false;
        agent.speed = kittyMoveSpeed;
        yield return new WaitForSeconds(fleeDuration);

        currentState = EnemyState.ChasingKitty;
        yield return new WaitForSeconds(kittyChaseDuration);

        currentState = EnemyState.ChasingMonster;
        agent.speed = monsterMoveSpeed;
        AudioManager.Instance.PlaySFX("cat_transform");

        activeReactionRoutine = null;
    }

    private void UpdateAnimation()
    {
        string suffix = (currentState != EnemyState.ChasingMonster) ? "_Cat" : "";

        string baseAnim = "Idle";

        if (agent.velocity.sqrMagnitude > 0.01f)
        {
            if (Mathf.Abs(agent.velocity.x) > Mathf.Abs(agent.velocity.y))
            {
                if (agent.velocity.x > 0) baseAnim = "Run_Right";
                else baseAnim = "Run_Left";
            }
            else
            {
                if (agent.velocity.y > 0) baseAnim = "Run_Back";
                else baseAnim = "Run_Front";
            }
        }

        ChangeAnimationState(baseAnim + suffix);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentAnimState == newState) return;

        animator.Play(newState);
        currentAnimState = newState;
    }
}