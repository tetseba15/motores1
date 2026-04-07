using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private enum AIState { Patrol, Chase }

    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Space(20)]
    [SerializeField] private Transform[] _patrolWaypoints;

    [Header("Vision Settings")]
    [SerializeField, Range(1f, 50f)] private float _viewRadious = 15f;
    [SerializeField, Range(1f, 360f)] private float _viewAngle = 90f;

    [Space(10)]
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    private NavMeshAgent _agent;
    private AIState _currentState;
    private int _currentWaypointIndex;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = AIState.Patrol;
    }




    void Start()
    {
        if (_patrolWaypoints.Length > 0)
        {
            _agent.SetDestination(_patrolWaypoints[0].position);
        }
    }

    void Update()
    {
        CheckSensors();

        switch (_currentState)
        {
            case AIState.Patrol:
                HandlePatrol();
                break;

            case AIState.Chase:
                HandleChase();
                break;
        }
    }
    private void CheckSensors()
    {
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        if (distanceToPlayer <= _viewRadious)
        {

            // Check if player is inside angle vision
            if (Vector3.Angle(transform.forward, directionToPlayer) < _viewAngle / 2f)
            {
                // Check if a wall or obstacle is blocking vision
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, _obstacleMask))
                {
                    ChangeState(AIState.Chase);
                    return;
                }

            }

        }

        //Loosing target
        if (_currentState == AIState.Chase && distanceToPlayer > _viewRadious * 1.5f)
        {
            ChangeState(AIState.Patrol);
        }

    }

    private void ChangeState(AIState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;

        if (_currentState == AIState.Patrol)
        {
            _agent.SetDestination(_patrolWaypoints[_currentWaypointIndex].position);
        }
    }

    private void HandleChase()
    {
        _agent.SetDestination(_playerTransform.position);
    }

    private void HandlePatrol()
    {

        if (_patrolWaypoints.Length == 0) return;

        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _patrolWaypoints.Length;
            _agent.SetDestination(_patrolWaypoints[_currentWaypointIndex].position);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewRadious);

        Vector3 viewAngleLeft = DirFromAngle(transform.eulerAngles.y, -_viewAngle / 2f);
        Vector3 viewAngleRight = DirFromAngle(transform.eulerAngles.y, _viewAngle / 2f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleLeft * _viewAngle);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleRight * _viewAngle);

        if(_playerTransform != null && _currentState == AIState.Chase)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _playerTransform.position);
        }
    }


    private Vector3 DirFromAngle(float eulerY, float angleInDegrees)
    {

        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }
}
