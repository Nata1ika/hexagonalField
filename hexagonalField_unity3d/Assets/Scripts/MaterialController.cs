using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] Material _default;
    [SerializeField] Material _dissolve;
    [SerializeField] Material _bumpMap;
 
    public bool RandomColor { get; set; }

    void Awake()
    {
        HexCreator.CreateEvent += SetMaterial;
    }  

    void OnDestroy()
    {
        HexCreator.CreateEvent -= SetMaterial;
    }

    void SetMaterial(List<Hex> obj)
    {
        foreach (var hex in obj)
        {
            //hex.renderer.material = _default;
            //SetDissolve(hex.renderer);
            SetBumpMap(hex.renderer);
        }
    }

    void SetDissolve(MeshRenderer renderer)
    {
        renderer.material = _dissolve;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(UnityEngine.Random.RandomRange(0f, 1f), UnityEngine.Random.RandomRange(0f, 1f)));
        Color color;
        if (RandomColor)
        {
            color = new Color(UnityEngine.Random.RandomRange(0f, 1f), UnityEngine.Random.RandomRange(0f, 1f), UnityEngine.Random.RandomRange(0f, 1f));
        }
        else
        {
            float rnd = UnityEngine.Random.RandomRange(0f, 1f);
            color = new Color(rnd, rnd, rnd);
        }
        renderer.material.SetColor("_MainColor", color);
    }

    void SetBumpMap(MeshRenderer renderer)
    {
        renderer.material = _bumpMap;
    }
}
