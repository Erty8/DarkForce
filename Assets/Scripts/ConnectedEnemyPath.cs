using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Assets.Scripts
{

    public class ConnectedEnemyPath : MonoBehaviour
    {

        [SerializeField]
        bool _patrolWaiting;
        [SerializeField]
        float _totalWaitTime = 3f;
        [SerializeField]
        float _switchProbability = .2f;


        NavMeshAgent _agent;
        ConnectedWaypoints _currentWaypoint;
        ConnectedWaypoints _previousWaypoint;

        bool _travelling;
        bool _waiting;
        float _waitTİmer;
        int _waypointsVisited;


        // Start is called before the first frame update
        void Start()
        {
            _agent = this.GetComponent<NavMeshAgent>();

            if(_agent == null)
            {
                Debug.LogError("Assign NavMeshAgent.");
            }
            else
            {
                if(_currentWaypoint == null)
                {
                    GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                    if(allWaypoints.Length > 0)
                    {
                        while(_currentWaypoint == null)
                        {
                            int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                            ConnectedWaypoints startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoints>();

                            if(startingWaypoint != null)
                            {
                                _currentWaypoint = startingWaypoint;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("No waypoints in scene.");
                    }
                }
            }

            SetDestination();
        }

        // Update is called once per frame
        public void Update()
        {
            if(_travelling && _agent.remainingDistance <= 1f)
            {
                _travelling = false;
                _waypointsVisited++;

                if (_patrolWaiting)
                {
                    _waiting = true;
                    _waitTİmer = 0f;
                }
                else
                {
                    SetDestination();
                }


                if (_waiting)
                {
                    _waitTİmer += Time.deltaTime;
                    if(_waitTİmer >= _totalWaitTime)
                    {
                        _waiting = false;
                        SetDestination();
                    }

                }

            }
        }

        private void SetDestination()
        {
            if(_waypointsVisited > 0)
            {
                ConnectedWaypoints nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
                _previousWaypoint = _currentWaypoint;
                _currentWaypoint = nextWaypoint;
            }

            Vector3 targetVector = _currentWaypoint.transform.position;
            _agent.SetDestination(targetVector);
            _travelling = true;
        }
    }
}

