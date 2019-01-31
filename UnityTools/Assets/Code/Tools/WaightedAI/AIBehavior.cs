using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    public class AIBehavior : MonoBehaviour {

        public static List<AIBehavior> _aIReverences = new List<AIBehavior>();

        [SerializeField] IWaightedAIDesition[] _behaviors;

        int _currentBehavior = -1;
        
        public void Reevaluate() {
            _currentBehavior = -1;
            float currentWaight = float.MinValue;
            float tmp;
            for (int i = 0; i < _behaviors.Length; i++) {
                tmp = _behaviors[i].Evaluate();
                if (tmp > currentWaight) {
                    _currentBehavior = i;
                    currentWaight = tmp;
                }
            }
        }

        void Update() {
            if(_currentBehavior < 0 || _currentBehavior >= _behaviors.Length)
                return;

            _behaviors[_currentBehavior].Execute();
        }
    }
}