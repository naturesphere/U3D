using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public GameObject mainCamera;
    public GameObject aim;
    public float heightDamping=0;
    public float distance=50;
    public float ZDamping =5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void LateUpdate()
    {

        float currentHeight = mainCamera.transform.position.y;//当前高度
        float wantedHeight = 3;//目标高度

        float currentRotationAngles = mainCamera.transform.eulerAngles.y;//当前角度

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);//调整高度

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngles, 0);//摄像机姿态四元数

        mainCamera.transform.Translate(0,0,2*Time.deltaTime*Mathf.Lerp(ZDamping,0.1f,Time.deltaTime),Space.Self);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, currentHeight, mainCamera.transform.position.z);

        if (mainCamera.transform.position.z < 15.5)
        {
            mainCamera.transform.LookAt(aim.transform);
        } 
    }
}
