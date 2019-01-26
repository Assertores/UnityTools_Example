using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    public class TrackingShot : MonoBehaviour {

        [SerializeField] Transform _rootNode;

        ICameraNode[] _nodes;
        int _currentNode = 0;
        public bool _hasFinished { get; private set; } = true;

        void Start() {
            if (!_rootNode) {
                _rootNode = transform;
            }

            List<ICameraNode> tempList = new List<ICameraNode>();
            ICameraNode temp;
            for (int i = 0; i < _rootNode.childCount; i++) {
                temp = _rootNode.GetChild(i).GetComponent<ICameraNode>();
                if (temp != null) {
                    tempList.Add(temp);
                }
            }
            _nodes = tempList.ToArray();
        }
        
        void Update() {
            if (!_hasFinished) {
                if(_currentNode >= _nodes.Length) {
                    _hasFinished = true;//make callback instadt?
                    return;
                }
                if (_nodes[_currentNode].NextTick()) {
                    _currentNode++;
                }
            }
        }

        public void StartTrackingShot() {
            _currentNode = 0;
            _hasFinished = false;
        } 
    }
}