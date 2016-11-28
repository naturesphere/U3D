using UnityEngine;
using System.Collections;

public class DongHua : MonoBehaviour {
    public GameObject guanggaoban1;
    public GameObject guanggaoban2;
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1,0);
    public string textureName = "_MainTex";
    Vector2 uvOffset = Vector2.zero;

    void Update()
    {
        uvOffset += (uvAnimationRate * 0.01f * Time.deltaTime);
        if (guanggaoban1.GetComponent<Renderer>().enabled)
        {
            guanggaoban1.GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
        if (guanggaoban2.GetComponent<Renderer>().enabled)
        {
            guanggaoban2.GetComponent<Renderer>().materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }

    }
}
