using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public GameObject player;
    public bool scflag = true;
    //public GameObject cube;
	// Use this for initialization
    void Start() {
        
    }
	
	// Update is called once per frame
    void Update() {
        if (Staticer.animationString == "down")
        {
            BoxCollider b = this.GetComponentInChildren<BoxCollider>(); 
            b.size = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            BoxCollider b = this.GetComponentInChildren<BoxCollider>(); ;
            b.size = new Vector3(0.2f, 0.1f, 0.1f);
        }
        if (Staticer.isPlay)
        {
            GetComponent<Animation>().CrossFade(Staticer.animationString);
            Staticer.isPlay = false;
        }
        if (Staticer.isSave && Staticer.animationString != null && Staticer.animationString!="down")
        {
            Vector3 po = player.transform.position;
            Vector3 aimpo = Staticer.endPoint;

            if (GetComponent<Animation>()[Staticer.animationString].normalizedTime >= 0.2f && GetComponent<Animation>()[Staticer.animationString].normalizedTime <= 0.7)//在动画过程中加事件
            {
                if (Time.deltaTime != 0)
                {
                    player.transform.position = new Vector3(Mathf.Lerp(po.x, aimpo.x, 5 * Time.deltaTime)
                    , Mathf.Lerp(po.y, aimpo.y, 5 * Time.deltaTime)
                    , po.z);//移动接球
                }
            }
            
        }
        if (Staticer.isSave && Staticer.animationString != null && GetComponent<Animation>()[Staticer.animationString].normalizedTime >= 0.9f && scflag)
        {
            scflag = false;
        }
	}
}
