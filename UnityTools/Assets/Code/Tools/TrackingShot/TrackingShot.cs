using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    public class TrackingShot : MonoBehaviour {

        static Transform _rootNode;
        static ICameraNode[] _nodes;

        static int _currentNode = 0;
        public static bool _hasFinished { get; private set; } = true;

        //public static TrackingShot _singelton;
        //void Awake() {
        //    if (!_singelton) {
        //        _singelton = this;
        //    } else {
        //        Destroy(this);
        //    }
        //}

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

        public static void StartTrackingShot(Transform rootTransform, Transform camera) {
            _rootNode = rootTransform;

            List<ICameraNode> tempList = new List<ICameraNode>();
            ICameraNode temp;
            for (int i = 0; i < _rootNode.childCount; i++) {
                temp = _rootNode.GetChild(i).GetComponent<ICameraNode>();
                if (temp != null) {
                    tempList.Add(temp);
                    temp.SetCamera(camera);
                }
            }
            _nodes = tempList.ToArray();

            _currentNode = 0;
            _hasFinished = false;
        } 
    }
}