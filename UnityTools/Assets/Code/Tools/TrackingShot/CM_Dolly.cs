using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    public class CM_Dolly : MonoBehaviour, ICameraNode {

        [SerializeField] float _dollyTime;
        [SerializeField] float _waitingTime;
        [SerializeField] float _targetFOV;

        float _collapsedTime = 0;

        bool _camErrorFlag;
        Transform _cam;
        Vector3 _startPos;
        Quaternion _startRot;
        float _startFOV;

        public bool NextTick() {
            return NextTick(Time.deltaTime);
        }

        public bool NextTick(float timeDelta) {
            if (_camErrorFlag) {
                return true;
            }

            if (_collapsedTime == 0) {
                _startPos = _cam.position;
                _startRot = _cam.rotation;
                _startFOV = _cam.GetComponent<Camera>().fieldOfView;
            }
            _collapsedTime += timeDelta;


            if (_collapsedTime >= _dollyTime + _waitingTime) {
                _collapsedTime = 0;
                return true;
            }

            float temp = _collapsedTime / _dollyTime;
            if (_collapsedTime < _dollyTime) {
                _cam.position = Vector3.Lerp(_startPos, transform.position, temp);
                _cam.rotation = Quaternion.Lerp(_startRot, transform.rotation, temp);
                _cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(_startFOV, _targetFOV, temp);
            }

            return false;
        }

        public void SetCamera(Transform cam) {
            if (!cam.GetComponent<Camera>()) {
                _camErrorFlag = true;
                return;
            }
            _camErrorFlag = false;
            _cam = cam;
        }
    }
}