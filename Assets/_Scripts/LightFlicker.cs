using UnityEngine;
using UnityEngine.Rendering.Universal; // Make sure to include this namespace

public class LightFlicker : MonoBehaviour
{
    public float minIntensity = 1f;
    public float maxIntensity = 2f;
    public float flickerSpeed = 0.1f; // Adjust for faster or slower flickering

    public Light2D light2D;
    private float _randomOffset;

    // Start is called before the first frame update
    void Start()
    {
        if (light2D == null)
        {
            Debug.LogError("Light2D component not found on this GameObject!");
            enabled = false; // Disable the script if no Light2D is present
        }

        // Give each light a slightly different starting point for the noise
        _randomOffset = Random.value * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (light2D != null)
        {
            // Use Perlin noise to create a smooth, organic flicker
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed + _randomOffset, 0f);

            // Remap the noise value (0-1) to our desired intensity range
            light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }
    }
}