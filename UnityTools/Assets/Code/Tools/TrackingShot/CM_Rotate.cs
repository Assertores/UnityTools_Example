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
        bool _finishedRotatoin = false;

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
                _finishedRotatoin = false;
                if(_targetDistance < 0) {
                    _targetDistance = _startDistance;
                }
            }
            _collapsedTime += timeDelta;

            if (_collapsedTime >= _rotateTime + _waitingTime) {
                _collapsedTime = 0;
                return true;
            }

            float temp = _collapsedTime / _rotateTime;
            if (_collapsedTime < _rotateTime) {
                _cam.rotation = Quaternion.Slerp(Quaternion.LookRotation((transform.position - _startPos).normalized), transform.rotation, temp);
                _cam.position = transform.position;
                float dist = Mathf.Lerp(_startDistance, _targetDistance, temp);
                if (_doCameraCollition) {
                    Ray ray = new Ray(transform.position, -_cam.forward);
                    RaycastHit hit;
                    if(Physics.Raycast(ray,out hit, dist)) {
                        dist = Vector3.Distance(transform.position, hit.point);
                    }
                }
                _cam.Translate(-Vector3.forward * dist);
                _cam.rotation = Quaternion.Slerp(_startRot, transform.rotation, temp);
                _cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(_startFOV, _targetFOV, temp);
            } else if (!_finishedRotatoin) {
                _cam.rotation = transform.rotation;
                _cam.position = transform.position;
                float dist = _targetDistance;
                if (_doCameraCollition) {
                    Ray ray = new Ray(transform.position, -_cam.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, dist)) {
                        dist = Vector3.Distance(transform.position, hit.point);
                    }
                }
                _cam.Translate(-Vector3.forward * dist);
                _cam.GetComponent<Camera>().fieldOfView = _targetFOV;
                _finishedRotatoin = true;
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