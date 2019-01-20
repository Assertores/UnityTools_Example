using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Localisation scripts must always have a public get list of Scriptreferences named references
/// Localisation scripts must always have a public void funktion caled ChangeLanguage(string Language)
/// this script requiers the CSVReader
/// </summary>
public class Localize_text : MonoBehaviour {

    #region ===== ===== DATA ===== =====

    /// <summary>
    /// a list of all referenced instances of this script
    /// </summary>
    public static List<Localize_text> references { get; private set; } = new List<Localize_text>();

    [SerializeField] Text[] _textFeald; //reference to the Textfiel
    [SerializeField] string _streamingAssetPath; //the path to the csv file whitch is used for the Localisation
    [SerializeField] string _id; //the id of the string

    #endregion
    #region ===== ===== REFRENCING ===== =====

    /// <summary>
    /// registers this script to the static List if it is valide
    /// if _textFeald is not set, it tryes to find a text component in the gameobject
    /// </summary>
    private void Awake() {
        if (_textFeald.Length == 0) {
            _textFeald = new Text[1] { GetComponent<Text>() };

            if (_textFeald.Length == 0) {
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
    /// <param name="Language">the name of the Language as it is wirtten in the csv file</param>
    public void ChangeLanguage(string Language) {
        string temp = CSVReader.getValueAsStringFromStreamingAsset(_streamingAssetPath, _id, Language);
        if (temp.Contains("ERROR")) {
            foreach (var it in _textFeald) {
                it.text = _id;
            }
        } else {
            foreach (var it in _textFeald) {
                it.text = temp;
            }
        }

        
    }

    #endregion
}
