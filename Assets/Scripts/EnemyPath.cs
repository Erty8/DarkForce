using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPath : MonoBehaviour
{

    EnemyCombatScript enemyCombatScript;
    
    public Transform player;
    private float dist;
    public float detectRange;

    [SerializeField]
    NavMeshAgent _agent;
    [SerializeField]
    Transform _destination;

    [SerializeField]
    bool _patrolWaiting;
    [SerializeField]
    float _totalWaitTime = 3f;
    [SerializeField]
    float _switchProbablity = .2f;
    [SerializeField]
    List<Waypoints> _patrolPoints;

    int _currentPatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _patrolForawrd;
    float _waitTimer;

    public Animator anim;
    public Enemy_AI aiscript;
    public float speed;
    public float speedVal;
    public float motionSmoothTime = .1f;



    // Start is called before the first frame update
    void Start()
    {
        enemyCombatScript = GetComponent<EnemyCombatScript>();        
        _agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aiscript = gameObject.GetComponent<Enemy_AI>();

        if (_patrolPoints != null && _patrolPoints.Count >= 2)
        {
            _currentPatrolIndex = 0;
            SetDestination();
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (aiscript.summoned)
        {

        }
        speed = new Vector3(_agent.velocity.x, 0, _agent.velocity.z).magnitude / _agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
        speedVal = speed * motionSmoothTime * Time.deltaTime;
        //SetDestination();
        dist = Vector3.Distance(player.position, transform.position);

        //Movement after death fixed
        if (enemyCombatScript.isAlive)
        {
            if (dist <= detectRange)
            {
                if (aiscript.walkbool)
                {
                    Vector3 targetVector = player.position;
                    _agent.SetDestination(targetVector);
                }

            }
            else
            {
                if (_travelling && _agent.remainingDistance <= 1f)
                {
                    _travelling = false;

                    if (_patrolWaiting)
                    {
                        _waiting = true;
                        _waitTimer = 0f;

                    }
                    else
                    {
                        ChangePatrolPoint();
                        SetDestination();
                    }
                }

                if (_waiting)
                {
                    _waitTimer += Time.deltaTime;
                    if (_waitTimer >= _totalWaitTime)
                    {
                        _waiting = false;

                        ChangePatrolPoint();
                        SetDestination();
                    }

                }
            }

        }

    }

    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _agent.SetDestination(targetVector);
            _travelling = true;

        }



        //if (_destination != null)
        //{
        //    Vector3 targetVector = _destination.transform.position;
        //    _agent.SetDestination(targetVector);
        //}
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbablity)
        {
            _patrolForawrd = !_patrolForawrd;
        }

        if (_patrolForawrd)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }

        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }
    public void speedcalc()
    { speed = new Vector3(_agent.velocity.x, 0, _agent.velocity.z).magnitude / _agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
        speedVal = speed* motionSmoothTime * Time.deltaTime;
}
}
