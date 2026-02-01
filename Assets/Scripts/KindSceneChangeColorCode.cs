using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class KindSceneChangeColorCode : MonoBehaviour
{

    public Volume volume;
    ColorAdjustments colorAdjustments;

    private void OnEnable()
    {
        volume.profile.TryGet(out colorAdjustments);
        
        StartCoroutine(AfterEnabling());
       
    }

    IEnumerator AfterEnabling()
    {
        while(colorAdjustments.saturation.value <= 70)
        {
            yield return new WaitForSeconds(0.1f);
            colorAdjustments.saturation.value += 5;
        }
    }
}
