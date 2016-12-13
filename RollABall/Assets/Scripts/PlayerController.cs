using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float mSpeed=1.0f;
    public Text mCountText;
    public Text mWinText;

    private Rigidbody rb;
    private int mCount = 0;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCountText();
        mWinText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement*mSpeed);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            mCount++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        mCountText.text = "Count: " + mCount.ToString();
        if (mCount >= 12)
            mWinText.text = "You Win";
    }
}
