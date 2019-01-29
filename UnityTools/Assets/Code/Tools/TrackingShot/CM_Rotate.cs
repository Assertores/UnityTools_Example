using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    public class CM_Rotate : MonoBehaviour, ICameraNode {

        [SerializeField] float _rotateTime = 2;
        [SerializeField] float _waitingTime = 2;
        [SerializeField] float _targetFOV = 60;

        [SerializeField] bool _LockCameraInward = true;
        [SerializeField] bool _doCameraCollition = true;
        [SerializeField] float _targetDistance = -1;

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
                _cam.rotation = Quaternion.Slerp(Quaternion.LookRotation((transform.position - _startPos).normalized), Quaternion.LookRotation(transform.forward), temp);
                _cam.position = transform.position;
                float dist = Mathf.Lerp(_startDistance, _targetDistance, temp);
                if (_doCameraCollition) {
                    Ray ray = new Ray(transform.position, -_cam.forward);
                    RaycastHit hit;
                    if(Physics.Raycast(ray,out hit, dist)) {
                        dist = Vector3.Distance(transform.position, hit.point) - 1f;
                    }
                }
                _cam.Translate(-Vector3.forward * dist);
                _cam.rotation = Quaternion.Slerp(_startRot, Quaternion.LookRotation(transform.forward * ((_LockCameraInward) ? 1 : -1)), temp);
                _cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(_startFOV, _targetFOV, temp);
            } else if (!_finishedRotatoin) {
                _cam.rotation = Quaternion.LookRotation(transform.forward);
                _cam.position = transform.position;
                float dist = _targetDistance;
                if (_doCameraCollition) {
                    Ray ray = new Ray(transform.position, -_cam.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, dist)) {
                        dist = Vector3.Distance(transform.position, hit.point) - 1f;
                    }
                }
                _cam.Translate(-Vector3.forward * dist);
                _cam.rotation = Quaternion.LookRotation(transform.forward * ((_LockCameraInward) ? 1 : -1));
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