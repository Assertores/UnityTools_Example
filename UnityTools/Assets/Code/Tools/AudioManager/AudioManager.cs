using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsserTOOLres {
    /// <summary>
    /// make shure that all added audioSources are not PlayOnAwake
    /// </summary>
    public class AudioManager : MonoBehaviour {

        #region ===== ===== REFERENCES ===== =====

        public static List<AudioManager> _reverences = new List<AudioManager>();

        #endregion
        #region ===== ===== DATA ===== =====

        [Tooltip("make shure that all added audioSources are not PlayOnAwake")]
        [SerializeField] Audio[] _clips;

        Dictionary<string, AudioSource> _clipRefs = new Dictionary<string, AudioSource>();

        #endregion
        #region ===== ===== SINGELTON ===== =====

        void Awake() {
            if(_reverences.Count > 0) {
                Destroy(this);
                return;
            }
            foreach(var it in _clips) {
                if (it.refereces && !_clipRefs.ContainsKey(it.name)) {
                    _clipRefs.Add(it.name, it.refereces);
                }
            }
            _reverences.Add(this);
        }

        private void OnDestroy() {
            _reverences.Remove(this);
        }

        #endregion
        #region ===== ===== API ===== =====

        /// <summary>
        /// Plays an audio by referenced name
        /// </summary>
        /// <param name="name">the name as key</param>
        public void PlayAudioByName(string name) {
            _clipRefs[name]?.Play();
        }

        /// <summary>
        /// adds a AudioSource component to the GameObject, sets it to the referenced audio and plays it
        /// </summary>
        /// <param name="name">the name as key</param>
        /// <param name="reference">the referenced GameObject</param>
        public void PlayAudioOnRef(string name, GameObject reference) {
            if (!_clipRefs.ContainsKey(name))
                return;

            AudioSource temp = reference.AddComponent<AudioSource>();

            PlayAudioOnRef(name, temp);
        }

        /// <summary>
        /// sets the referenced AudioSource to the Rreferenced audio and plays it
        /// </summary>
        /// <param name="name">the name as key</param>
        /// <param name="reference">the referenced AudioSource</param>
        public void PlayAudioOnRef(string name, AudioSource reference) {
            if (!_clipRefs.ContainsKey(name))
                return;

            //----- ----- Deep Copy ----- -----
            System.Reflection.PropertyInfo[] fields = typeof(AudioSource).GetProperties();
            print("i'm here: " + typeof(AudioSource).GetField("volume"));
            foreach (var field in fields) {
                print("i'm coping " + field.Name);
                if (!field.CanWrite || field.Name == "name")
                    continue;

                field.SetValue(reference, field.GetValue(_clipRefs[name]));
            }

            reference.Play();
        }

        #endregion
    }
}