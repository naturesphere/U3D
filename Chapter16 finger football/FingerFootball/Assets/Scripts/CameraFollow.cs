using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject aim;
    public float heightDamping = 0;
    public float ZDamping = 5;
   
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void LateUpdate()
    {
        float currentHeight = mainCamera.transform.position.y;
        float wantedHeight = 3;
        float currentRotationAngles = mainCamera.transform.eulerAngles.y;
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngles, 0);//摄像机姿态四元数
        mainCamera.transform.Translate(0, 0, 2 * Time.deltaTime * Mathf.Lerp(ZDamping, 0.1f, Time.deltaTime), Space.Self);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, currentHeight, mainCamera.transform.position.z);
        if (mainCamera.transform.position.z < 15.5)
            mainCamera.transform.LookAt(aim.transform);
    }
}
