using UnityEngine;
using System.Collections;
//Contains no platform specific methods

public class ParticleManager : MonoBehaviour {

	private ParticleSystem ps;

    // Use this for initialization
    void Start () {
		ps = GetComponent<ParticleSystem> ();
    }
	
	// Update is called once per frame
	void Update () {
		if (ps) {
			if (!ps.IsAlive ()) {
				Destroy (gameObject);
			}
		}
	}
}
