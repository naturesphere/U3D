using UnityEngine;
using System.Collections;

public class MoveLight : MonoBehaviour {

    public GameObject lightScreen;

	// Use this for initialization
	void Start () {
        lightScreen = Instantiate(lightScreen) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount != 0) {
            lightScreen.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 1));
        }
	}
}
