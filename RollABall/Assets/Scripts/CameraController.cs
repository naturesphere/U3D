using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject mPlayer;
    private Vector3 mOffset;
	// Use this for initialization
	void Start ()
    {
        mOffset = transform.position - mPlayer.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = mPlayer.transform.position + mOffset;
	}
}
