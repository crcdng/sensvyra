using UnityEngine;

public class OscModifyPassthrough : MonoBehaviour
{
    
    public float scaleAmount = 0.3f;
    private float scale = 0.0f;
    private Vector3 scale0;

    private Vector3 p0; // original position

   void Start()
    {
        // Store the starting local position once
        p0 = transform.localPosition;
        scale0 = transform.localScale;
    }

    public void GetCalm(float v)
    {
        Debug.Log("osc passthrough calm: " + v);
        scale = v;
    }

    public void GetFocus(float v)
    {
        Debug.Log("osc passthrough focus: " + v);
        scale = v;
    }

    void Update()
    {
            transform.localScale = scale0 * (1.0f + scale*scaleAmount);
    }
}

