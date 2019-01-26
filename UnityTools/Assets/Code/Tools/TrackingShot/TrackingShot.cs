using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    public class TrackingShot : MonoBehaviour {

        #region ===== ===== CALLBACK ===== =====

        /// <summary>
        /// callback funktion when Cutsceen is finished.
        /// </summary>
        public static System.Action _onFinish;

        #endregion
        #region ===== ===== DATA ===== =====

        Transform _rootNode;
        ICameraNode[] _nodes;

        int _currentNode = 0;
        bool _hasFinished = true;

        #endregion
        #region ===== ===== SINGELTON ===== =====

        static TrackingShot Singelton = null;
        void Awake() {
            if(Singelton == null) {
                Singelton = this;
            } else {
                Destroy(this);
            }
        }
        void OnDestroy() {
            if(Singelton = this) {
                Singelton = null;
            }
        }

        #endregion
        #region ===== ===== CORE ===== =====

        void Update() {
            if (!_hasFinished) {
                if(_currentNode >= _nodes.Length) {
                    _onFinish();
                    return;
                }
                if (_nodes[_currentNode].NextTick()) {
                    _currentNode++;
                }
            }
        }

        #endregion
        #region ===== ===== API ===== =====

        /// <summary>
        /// Starts the Tracking shot.
        /// _onFinish will be called when the Tracking shot has finished.
        /// </summary>
        /// <param name="rootTransform">the root transform inwitch all the cutsceens nodes are as first childs. node scripts have to inharent ICameraNode</param>
        /// <param name="camera">the camera with witch the cuttscene should be played</param>
        public static void StartTrackingShot(Transform rootTransform, Transform camera) {

            if(Singelton == null) {
                GameObject tmp = new GameObject();
                tmp.AddComponent<TrackingShot>();
                tmp.name = "TrackingShotHandler";
            }

            Singelton._rootNode = rootTransform;

            List<ICameraNode> tempList = new List<ICameraNode>();
            ICameraNode temp;
            for (int i = 0; i < Singelton._rootNode.childCount; i++) {
                temp = Singelton._rootNode.GetChild(i).GetComponent<ICameraNode>();
                if (temp != null) {
                    tempList.Add(temp);
                    temp.SetCamera(camera);
                }
            }
            Singelton._nodes = tempList.ToArray();

            Singelton._currentNode = 0;
            Singelton._hasFinished = false;
        }

        #endregion
    }
}