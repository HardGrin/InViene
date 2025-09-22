using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Volume volume;
    public Vignette vignette;
    public LiftGammaGain LFG;


    [Header("��������� ��������")]
    public float radius = 0.2f;   // ������ ����� (��������� ������ ������ ����)
    public float speed = 1f;
    private Vector4 baseLift;


    void Start()
    {
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out LFG);
    }

    // Update is called once per frame
    void Update()
    {
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.PingPong(Time.time * 0.5f, 0.5f);
            //LFG.lift.value = new Color(0,0,0,0);
        }
        if (LFG != null)
        {
            float t = Time.time * speed;

            // �������� �� ���������� � RGB
            float r = baseLift.x + Mathf.Cos(t) * radius;
            float g = baseLift.y + Mathf.Sin(t) * radius;
            float b = baseLift.z + Mathf.Cos(t + Mathf.PI / 2f) * radius; // �������� �� ����

            // ��������� w (�������������) ��� � ��������
            LFG.lift.value = new Vector4(r, g, b, baseLift.w);
        }
    }
}
