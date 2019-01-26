using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AsserTOOLres {
    /// <summary>
    /// Localisation scripts must always have a public get list of Scriptreferences named references
    /// Localisation scripts must always have a public void function called ChangeLanguage(string Language)
    /// this script requires the CSVReader
    /// </summary>
    public class Localize_text : MonoBehaviour {

        #region ===== ===== DATA ===== =====

        /// <summary>
        /// a list of all referenced instances of this script
        /// </summary>
        public static List<Localize_text> references { get; private set; } = new List<Localize_text>();

        [SerializeField] Text[] _textField; //reference to the Textfield
        [SerializeField] string _streamingAssetPath; //the path to the csv file which is used for the Localisation
        [SerializeField] string _id; //the id of the string

        #endregion
        #region ===== ===== REFRENCING ===== =====

        /// <summary>
        /// registers this script to the static List if it is valid
        /// if _textField is not set, it tries to find a text component in the gameobject
        /// </summary>
        private void Awake() {
            if (_textField.Length == 0) {
                _textField = new Text[1] { GetComponent<Text>() };

                if (_textField.Length == 0) {
                    return;
                }
            }
            references.Add(this);
        }

        /// <summary>
        /// unregisters this script
        /// </summary>
        private void OnDestroy() {
            references.Remove(this);
        }

        #endregion
        #region ===== ===== API ===== =====

        /// <summary>
        /// changes the language of all referenced texts.
        /// if the string in this Language is not found the id will be displayed.
        /// </summary>
        /// <param name="Language">the name of the Language as it is written in the csv file</param>
        public void ChangeLanguage(string Language) {
            string temp = CSVReader.getValueAsStringFromStreamingAsset(_streamingAssetPath, _id, Language);
            if (temp.Contains("ERROR")) {
                foreach (var it in _textField) {
                    it.text = _id;
                }
            } else {
                foreach (var it in _textField) {
                    it.text = temp;
                }
            }


        }

        #endregion
    }
}