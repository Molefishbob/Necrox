using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Templates : MonoBehaviour {

	public string template1;
	public string template2;
	public string template3;
	public string template4;
	public string template5;
	public string template6;
	public string test1;

	public string GetRandomRowTemplate(int index) {
		if (index == 0) {
			switch ((int) Random.Range(1,6)) {
				case 1:
					return template1;
				case 2:
					return template2;
				case 3:
					return template3;
				case 4:
					return template4;
				case 5:
					return template5;
				case 6:
					return template6;
				default:
					return template1;
			}
		} else if (index == 1) {
			return test1;
		} else {
			return test1;
		}
	}
}