using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    public class CM_Rotate : MonoBehaviour, ICameraNode {

        [SerializeField] float _rotateTime;
        [SerializeField] float _waitingTime;
        [SerializeField] float _targetFOV;

        [SerializeField] bool _doCameraCollition;
        [SerializeField] float _targetDistance;

        float _collapsedTime = 0;

        bool _camErrorFlag;
        Transform _cam;
        Vector3 _startPos;
        float _startDistance;
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
                _startDistance = Vector3.Distance(_cam.position, transform.position);
            }
            _collapsedTime += timeDelta;

            if (_collapsedTime >= _rotateTime + _waitingTime) {
                _collapsedTime = 0;
                return true;
            }

            float temp = _collapsedTime / _rotateTime;
            if (_collapsedTime < _rotateTime) {
                _cam.rotation = Quaternion.Lerp(_startRot, transform.rotation, temp);
                _cam.position = transform.position;
                _cam.Translate(-Vector3.Lerp(_startPos - transform.position, transform.forward, temp).normalized * Mathf.Lerp(_startDistance, _targetDistance, temp));
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