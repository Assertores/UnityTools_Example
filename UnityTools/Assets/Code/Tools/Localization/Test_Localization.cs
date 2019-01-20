using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Localization : MonoBehaviour {

    void Start() {
        StartCoroutine(Retest());
    }

    IEnumerator Retest() {
        print("i'm changing the Language to German on " + Localize_text.references.Count + " instances.");
        foreach(var it in Localize_text.references) {
            it.ChangeLanguage("German");
        }

        yield return new WaitForSeconds(3);

        print("i'm changing the Language to English on " + Localize_text.references.Count + " instances.");
        foreach (var it in Localize_text.references) {
            it.ChangeLanguage("English");
        }

        yield return new WaitForSeconds(3);

        print("i'm testing vor Latin whitch is not in the csv file");
        foreach (var it in Localize_text.references) {
            it.ChangeLanguage("Latin");
        }
    }
}
