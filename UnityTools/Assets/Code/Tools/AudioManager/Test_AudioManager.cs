using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

public class Test_AudioManager : MonoBehaviour {

    [SerializeField] GameObject _testObject;
    [SerializeField] AudioSource _testSource;

    void Start() {
        StartCoroutine(Retest());
    }

    IEnumerator Retest() {
        if (AudioManager._reverences.Count == 0)
            yield break;

        AudioManager._reverences[0].PlayAudioByName("Message");

        yield return new WaitForSeconds(2);

        AudioManager._reverences[0].PlayAudioOnRef("Charge", _testSource);

        yield return new WaitForSeconds(2);

        AudioManager._reverences[0].PlayAudioOnRef("Explosion", _testObject);
    }
}