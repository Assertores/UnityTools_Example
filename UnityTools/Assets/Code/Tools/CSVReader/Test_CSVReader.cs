using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_CSVReader : MonoBehaviour {

    [SerializeField] string WholePath;
    [SerializeField] string StreamingAssetPath;

    void Start() {
        Retest();
    }

    void Retest() {

        //if i get the Generic funktions to work ===== ===== ----- -----
        //Dictionary<string, List<object>> temp = CSVReader.LoadCSV(WholePath);
        Dictionary<string, List<string>> temp = CSVReader.LoadCSV(WholePath);

        foreach (var it in temp) {
            string line = it.Key + ":";
            foreach(var jt in it.Value) {
                //if i get the Generic funktions to work ===== ===== ----- -----
                //line += " " + jt.ToString();
                line += " " + jt;
            }
            print(line);
        }

        string sTemp;
        int iTemp;
        float fTemp;
        bool bTemp;

        sTemp = CSVReader.getValueAsString(WholePath, "WATER", "German");
        iTemp = CSVReader.getValueAsIntFromStreamingAsset(StreamingAssetPath, "LIVE", 0);
        fTemp = CSVReader.getValueAsFloatFromStreamingAsset(StreamingAssetPath, "SPEED", 0);
        bTemp = CSVReader.getValueAsBoolFromStreamingAsset(StreamingAssetPath, "NUDE", 0);

        print("WATER in German: " + sTemp + " | LIVE: " + iTemp + " | SPEED: " + fTemp + " | NUDE: " + bTemp);

        //if i get the Generic funktions to work ===== ===== ----- -----
        /*
        CSVReader.getValue(out sTemp, WholePath, "WATER", "German");
        CSVReader.getValueFromStreamingAsset(out iTemp, StreamingAssetPath, "LIVE", 0);
        CSVReader.getValueFromStreamingAsset(out fTemp, StreamingAssetPath, "SPEED", 0);
        CSVReader.getValueFromStreamingAsset(out bTemp, StreamingAssetPath, "NUDE", 0);

        print("WATER in German: " + sTemp + " | LIVE: " + iTemp + " | SPEED: " + fTemp + " | NUDE: " + bTemp);
        */
    }
}
