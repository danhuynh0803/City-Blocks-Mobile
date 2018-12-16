using UnityEngine;
using System.Collections;

public class DoNotDestroy : MonoBehaviour {

	static bool isInstantiated = false;
	
	// To keep persistent when changing levels
	void Awake() {
        if (!isInstantiated)
        {
            isInstantiated = true;
            DontDestroyOnLoad(this.gameObject);
        }
	}




}
