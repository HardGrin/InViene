using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Status : MonoBehaviour
{
    public static Status instance { get; private set; }


    private void Awake()
    {
        if(instance == null) instance = this;
        Debug.LogError("instance");
    }

    public void ChandgeStatus(List<int> syringeStates)
    {
        for(int i = 0; i < syringeStates.Count;i++)
        {
            //States[i] += syringeStates[i];
        }
    }
}
public class PlayerStatus : Status
{

    [Header("Chandgin Objects")]
    [SerializeField] private Volume volume;
    private Vignette vignette;
    private LiftGammaGain LFG;

    void Start()
    {
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out LFG);
    }
    // Update is called once per frame
    void Update()
    {
        VignetteBehaviour();
        LiftGammaGaneBehaviour();
    }
    public void VignetteBehaviour()
    {
        ////float vignetteWaight = Hemoglobin / RealBloodcount * 0.5f;
        float vignettespeed = 0.5f;
        if (vignette != null)
        {
            /////vignette.intensity.value = Mathf.PingPong(Time.time * vignettespeed, vignetteWaight);
            //LFG.lift.value = new Color(0,0,0,0);
        }
    }
    public void LiftGammaGaneBehaviour()
    {
         float radius = 1;
         float speed = 5;
         Vector4 baseLift = Vector4.zero;
    
        if (LFG != null)
        {
            float t = Time.time * speed;
    
            float r = baseLift.x + Mathf.Cos(t) * radius;
            float g = baseLift.y + Mathf.Sin(t) * radius;
            float b = baseLift.z + Mathf.Cos(t + Mathf.PI / 2f) * radius;
    
            LFG.lift.value = new Vector4(r, g, b, baseLift.w);
        }
    }
}
