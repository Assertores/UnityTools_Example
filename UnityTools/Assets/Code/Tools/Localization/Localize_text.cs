using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Localisation scripts must always have a public get list of Scriptreferences named 
/// </summary>
public class Localize_text : MonoBehaviour {

    #region ===== ===== DATA ===== =====

    public static List<Localize_text> references { get; private set; } = new List<Localize_text>();

    [SerializeField] Text _textFeald;
    [SerializeField] string _streamingAssetPath;
    [SerializeField] string _id;

    bool _errorFlag = false;

    #endregion
    #region ===== ===== REFRENCING ===== =====

    private void Awake() {
        if (!_textFeald) {
            _textFeald = GetComponent<Text>();

            if (!_textFeald) {
                _errorFlag = true;
                return;
            }
        }
        references.Add(this);
    }

    private void OnDestroy() {
        references.Remove(this);
    }

    #endregion
    #region ===== ===== API ===== =====

    public void ChangeLanguage(string Language) {
        if (_errorFlag)
            return;

        _textFeald.text = CSVReader.getValueAsStringFromStreamingAsset(_streamingAssetPath, _id, Language);
    }

    #endregion
}
