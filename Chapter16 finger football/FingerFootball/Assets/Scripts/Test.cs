using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public Vector2 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
	}
}
