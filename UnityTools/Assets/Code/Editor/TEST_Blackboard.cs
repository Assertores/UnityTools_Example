using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AsserTOOLres;

namespace AsserTOOLres_tests {
	public class TEST_Blackboard {
		const string health = "Health";
		const string lives = "Lives";
		const string language = "Language";
		const string data = "Data";

		const float three = 3.0f;
		const int five = 5;
		const string german = "German";
		const string suns = "Suns";
		const string english = "English";
		ExampleClass example = new ExampleClass();

		class ExampleClass {

		}

		[Test]
		public void StoreValues() {
			Blackboard.ClearBlackboard();

			Assert.IsTrue(Blackboard.NewBlackboard<int>());
			Assert.IsFalse(Blackboard.NewBlackboard<int>());

			Blackboard.AddValueToBlackboard(health, three);
			Blackboard.AddValueToBlackboard(lives, five);
			Blackboard.AddValueToBlackboard(language, german);
			Blackboard.AddValueToBlackboard(data, example);
		}

		[Test]
		public void RetreaveValues() {
			Blackboard.ClearBlackboard();
			StoreValues();

			float fTemp;
			int iTemp;
			string sTemp;
			ExampleClass eTemp;

			Assert.IsTrue(Blackboard.GetValueFromBlackboard(out fTemp, health));
			Assert.AreEqual(three, fTemp);
			Assert.IsTrue(Blackboard.GetValueFromBlackboard(out iTemp, lives));
			Assert.AreEqual(five, iTemp);
			Assert.IsTrue(Blackboard.GetValueFromBlackboard(out sTemp, language));
			Assert.AreEqual(german, sTemp);
			Assert.IsTrue(Blackboard.GetValueFromBlackboard(out eTemp, data));
			Assert.AreEqual(example, eTemp);
		}

		[Test]
		public void CatchFalseInput() {
			Blackboard.ClearBlackboard();
			StoreValues();

			float fTemp;
			int iTemp;
			string sTemp;

			Assert.IsFalse(Blackboard.GetValueFromBlackboard(out iTemp, suns));
			Assert.IsFalse(Blackboard.GetValueFromBlackboard(out fTemp, lives));
			Assert.IsFalse(Blackboard.GetValueFromBlackboard(out sTemp, english));
		}

		[Test]
		public void StackedValues() {
			Blackboard.ClearBlackboard();

			Blackboard.AddValueToBlackboard(data, example);
			Blackboard.AddValueToBlackboard(data, new ExampleClass(), 4);

			ExampleClass eTemp1;
			ExampleClass eTemp;
			ExampleClass eTemp2;

			Assert.IsTrue(Blackboard.GetValueFromBlackboard(out eTemp1, data, 0));
			Assert.IsFalse(Blackboard.GetValueFromBlackboard(out eTemp, data, 1));
			Assert.IsFalse(Blackboard.GetValueFromBlackboard(out eTemp, data, 2));
			Assert.IsFalse(Blackboard.GetValueFromBlackboard(out eTemp, data, 3));
			Assert.IsTrue(Blackboard.GetValueFromBlackboard(out eTemp2, data, 4));

			Assert.AreNotEqual(eTemp1, eTemp2);
		}
	}
}
