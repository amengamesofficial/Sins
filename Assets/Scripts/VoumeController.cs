using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    public static VolumeController instance;
    public Volume volume; // Volume رو از Inspector وصل کنید
    private ColorAdjustments colorAdjustments;

    void Start()
    {
        // چک می‌کنیم که Volume و Profile وجود دارند
        if (volume != null && volume.profile.TryGet<ColorAdjustments>(out var ca))
        {
            colorAdjustments = ca;
        }
        instance = this;
    }

    
}
