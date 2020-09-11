using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public PostProcessingManager postProcessingManager;
    public float maxFlickerIntensity = 24f;
    public float minFlickerIntensity = 15f;

    [Range(0.05f, 2)]
    public float speed = 0.5f;

    private int randomizer = 0;


    // Start is called before the first frame update
    void Reset()
    {
        postProcessingManager = GetComponent<PostProcessingManager>();
    }

    private void Update()
    {
        Flick();
    }

    // Flicker DOF
    private void Flick()
    {
        float newVal;
        if (randomizer == 0)
        {
            newVal = (Random.Range(minFlickerIntensity, maxFlickerIntensity));

        }
        else { newVal = (Random.Range(minFlickerIntensity, maxFlickerIntensity)); }

        randomizer = Random.Range(0, 2);

        postProcessingManager.SetDOF(Mathf.Lerp(postProcessingManager.GetDOF(), newVal, Mathf.Sqrt(speed * Time.deltaTime)));
    }
}
