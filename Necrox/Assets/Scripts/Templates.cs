using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Templates : MonoBehaviour {

	public string test1;
	public string _standardTemplate1;
	public string _standardTemplate2;
	public string _standardTemplate3;
	public string _standardTemplate4;
	public string _standardTemplate5;
	public string _standardTemplate6;
    public string _debrisTemplate1;
    public string _debrisTemplate2;
    public string _debrisTemplate3;
    public string _debrisTemplate4;
    public string _debrisTemplate5;
    public string _debrisTemplate6;

    public string GetRandomRowTemplate() {
			switch ((int)Random.Range(1,7)) {
				case 1:
					return _standardTemplate1;
				case 2:
					return _standardTemplate2;
				case 3:
					return _standardTemplate3;
				case 4:
					return _standardTemplate4;
				case 5:
					return _standardTemplate5;
				case 6:
					return _standardTemplate6;
				default:
					return test1;
			}
	}
	public string getRandomRowDebrisTemplate() {

		switch ((int)Random.Range(1,7)) {
				case 1:
					return _debrisTemplate1;
				case 2:
					return _debrisTemplate2;
				case 3:
					return _debrisTemplate3;
				case 4:
					return _debrisTemplate4;
				case 5:
					return _debrisTemplate5;
				case 6:
					return _debrisTemplate6;
				default:
					return test1;
			}

	}
}