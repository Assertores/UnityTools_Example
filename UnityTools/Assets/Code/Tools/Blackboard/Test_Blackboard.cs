using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsserTOOLres;

public class Test_Blackboard : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		Retest();
	}

	void Retest() {
		Blackboard.NewBlackboard<int>();
		Blackboard.AddValueToBlackboard("Health", 3.0f);
		Blackboard.AddValueToBlackboard("Lives", 5);
		Blackboard.AddValueToBlackboard("Language", "German");
		Blackboard.AddValueToBlackboard("Player", this.transform);

		float fTemp;
		int iTemp;
		string sTemp;
		Transform tTemp;
		Blackboard.GetValueFromBlackboard(out fTemp, "Health");
		Blackboard.GetValueFromBlackboard(out iTemp, "Lives");
		Blackboard.GetValueFromBlackboard(out sTemp, "Language");
		print("Health: " + fTemp + " | Lives: " + iTemp + " | Language: " + sTemp);

		Blackboard.GetValueFromBlackboard(out tTemp, "Player");
		print(tTemp.ToString());

		if(!Blackboard.GetValueFromBlackboard(out iTemp, "Suns")) {
			print("id Suns not set");
		}
		if(!Blackboard.GetValueFromBlackboard(out fTemp, "Lives")) {
			print("Lives is an int not an float");
		}
		if(!Blackboard.GetValueFromBlackboard(out sTemp, "English")) {
			print("English was not found");
		}
		if(!Blackboard.GetValueFromBlackboard(out tTemp, "Steave")) {
			print("the position of Steave was not found");
		}

		Blackboard.AddValueToBlackboard("Player", this.transform, 4);

		sTemp = "";
		if(Blackboard.GetValueFromBlackboard(out tTemp, "Player", 0))
			sTemp += tTemp.ToString() + " ";
		else
			sTemp += "| ";
		if(Blackboard.GetValueFromBlackboard(out tTemp, "Player", 1))
			sTemp += tTemp.ToString() + " ";
		else
			sTemp += "| ";
		if(Blackboard.GetValueFromBlackboard(out tTemp, "Player", 2))
			sTemp += tTemp.ToString() + " ";
		else
			sTemp += "| ";
		if(Blackboard.GetValueFromBlackboard(out tTemp, "Player", 3))
			sTemp += tTemp.ToString() + " ";
		else
			sTemp += "| ";
		if(Blackboard.GetValueFromBlackboard(out tTemp, "Player", 4))
			sTemp += tTemp.ToString() + " ";
		else
			sTemp += "| ";

		print(sTemp);

	}
}
