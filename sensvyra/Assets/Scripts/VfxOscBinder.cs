using UnityEngine;
using UnityEngine.VFX;

public class VfxOscBinder : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private VisualEffect vfx;

    [Header("VFX property names (exposed in VFX Graph)")]
    [SerializeField] private string calmProperty = "Calm";
    [SerializeField] private string focusProperty = "Focus";

    [Header("Input mapping")]
    [Tooltip("Incoming OSC expected range")]
    [SerializeField] private Vector2 inputRange = new Vector2(0f, 1f);

    [Tooltip("Remapped range sent to VFX")]
    [SerializeField] private Vector2 calmOutRange = new Vector2(0f, 1f);
    [SerializeField] private Vector2 focusOutRange = new Vector2(0f, 1f);

    [Header("Smoothing")]
    [Tooltip("Larger = more smoothing")]
    [SerializeField] private float calmSmooth = 8f;
    [SerializeField] private float focusSmooth = 8f;

    private float calmTarget, calmSmoothed;
    private float focusTarget, focusSmoothed;

    void Reset()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // These get called by OscEventReceiver UnityEvents
    public void SetCalm(float v)
    {
        calmTarget = Remap(v, inputRange.x, inputRange.y, calmOutRange.x, calmOutRange.y);
    }

    public void SetFocus(float v)
    {
        focusTarget = Remap(v, inputRange.x, inputRange.y, focusOutRange.x, focusOutRange.y);
    }

    void Update()
    {
        // Simple critically-damped-ish smoothing
        calmSmoothed = Mathf.Lerp(calmSmoothed, calmTarget, 1f - Mathf.Exp(-calmSmooth * Time.deltaTime));
        focusSmoothed = Mathf.Lerp(focusSmoothed, focusTarget, 1f - Mathf.Exp(-focusSmooth * Time.deltaTime));

        if (vfx != null)
        {
            if (!string.IsNullOrEmpty(calmProperty))  vfx.SetFloat(calmProperty,  calmSmoothed);
            if (!string.IsNullOrEmpty(focusProperty)) vfx.SetFloat(focusProperty, focusSmoothed);
        }
    }

    private static float Remap(float v, float inMin, float inMax, float outMin, float outMax)
    {
        if (Mathf.Approximately(inMin, inMax)) return outMin;
        var t = Mathf.InverseLerp(inMin, inMax, v);
        return Mathf.Lerp(outMin, outMax, t);
    }
}
