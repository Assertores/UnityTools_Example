using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AsserTOOLres;

[RequireComponent(typeof(NavMeshAgent))]
public class WAI_GoToTarget : MonoBehaviour, IWaightedAIDesition {

    [Header("References")]
    [SerializeField] Transform _pathRootNode;

    [Header("Behavior")]
    [SerializeField] AnimationCurve _timeSinceLastNodeReachedCurve;
    float _timeSinceLastNodeReached = 0;
    [SerializeField] AnimationCurve _distanceToTargetCurve;
    float _distanceToTarget = 0;

    [Header("Balancing")]
    [SerializeField] float _speed;

    [Header("Debug")]
    public float DBValue;

    AIBehavior _parent;
    NavMeshAgent _nMA;
    int _currentTarget = 0;

    public void Initialice(AIBehavior aI) {
        _parent = aI;
        _nMA = GetComponent<NavMeshAgent>();
    }

    public float Evaluate() {
        float value;

        value = _timeSinceLastNodeReachedCurve.Evaluate(Time.timeSinceLevelLoad - _timeSinceLastNodeReached);
        value *= _distanceToTargetCurve.Evaluate(Vector3.Distance(_pathRootNode.GetChild(_currentTarget).position, transform.position));

        DBValue = value;
        return value;
    }

    public void StartExecution() {

    }

    public void Execute() {
        
        _distanceToTarget = Vector3.Distance(_pathRootNode.GetChild(_currentTarget).position, transform.position);
        
        if (_distanceToTarget <= _speed * Time.deltaTime) {
            _nMA.Move(_pathRootNode.GetChild(_currentTarget).position - transform.position);
            
            NextTaget();
            return;
        }

        _nMA.Move((_pathRootNode.GetChild(_currentTarget).position - transform.position).normalized * _speed * Time.deltaTime);
    }

    void NextTaget() {
        _currentTarget++;
        _currentTarget %= _pathRootNode.childCount;

        _distanceToTarget = Vector3.Distance(_pathRootNode.GetChild(_currentTarget).position, transform.position);
        _timeSinceLastNodeReached = Time.timeSinceLevelLoad;

        _parent.Reevaluate();
    }
}
