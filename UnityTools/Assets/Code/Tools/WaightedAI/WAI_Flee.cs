using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AsserTOOLres;

public class WAI_Flee : MonoBehaviour, IWaightedAIDesition{

    [Header("References")]
    [SerializeField] Transform _fleeFrom;

    [Header("Behavior")]
    [SerializeField] AnimationCurve _fleeDistanceCurve;

    [Header("Balancing")]
    [SerializeField] float _speed;

    [Header("Debug")]
    public float DBValue;

    AIBehavior _parent;

    public float Evaluate() {
        float value;

        value = _fleeDistanceCurve.Evaluate(Vector3.Distance(transform.position, _fleeFrom.position));

        DBValue = value;
        return value;
    }

    public void Execute() {
        transform.Translate((transform.position - _fleeFrom.position).normalized * _speed * Time.deltaTime);
    }

    public void Initialice(AIBehavior aI) {
        _parent = aI;
    }

    public void StartExecution() {
    }

    void FixedUpdate() {
        if(_fleeDistanceCurve.Evaluate(Vector3.Distance(transform.position, _fleeFrom.position)) > 0.8f) {
            _parent.Reevaluate();
        }
    }
}
