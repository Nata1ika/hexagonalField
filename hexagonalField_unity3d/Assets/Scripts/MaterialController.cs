using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] Material _default;
    [SerializeField] Material _dissolve;
    [SerializeField] Material _bumpMap;
    [SerializeField] Material _carPaint;
    [SerializeField] Material _dirt;
    [SerializeField] Material _mud;
    [SerializeField] Material _rough;

    public TypeMaterial TypeMaterial { get; set; }

    void Awake()
    {
        HexCreator.ShowEvent += SetMaterial;
        HexCreator.HideEvent += Hide;
    }  

    void OnDestroy()
    {
        HexCreator.ShowEvent -= SetMaterial;
    }

    void Hide()
    {
        StopAllCoroutines();
    }

    void SetMaterial(List<Hex> obj)
    {
        if (TypeMaterial == TypeMaterial.Random)
        {
            SetRandom(obj);
        }
        else if (TypeMaterial == TypeMaterial.Default)
        {
            SetDefault(obj, false);
        }
        else if (TypeMaterial == TypeMaterial.DefaultRandomColor)
        {
            SetDefault(obj, true);
        }
        else if (TypeMaterial == TypeMaterial.Dissolve)
        {
            SetDissolve(obj, false);
        }
        else if (TypeMaterial == TypeMaterial.DissolveRandomColor)
        {
            SetDissolve(obj, true);
        }
        else
        {
            SetOtherMaterial(obj);
        }
    }

    void SetRandom(List<Hex> obj)
    {
        List<Hex> objDissolve = new List<Hex>();
        foreach (var hex in obj)
        {
            var typeMaterial = (TypeMaterial)Random.Range(1, 10);
            if (typeMaterial == TypeMaterial.DefaultRandomColor || typeMaterial == TypeMaterial.Default)
            {
                SetDefault(hex.renderer, true);
            }
            else if (typeMaterial == TypeMaterial.DissolveRandomColor || typeMaterial == TypeMaterial.Dissolve)
            {
                SetDissolve(hex.renderer, true);
                objDissolve.Add(hex);
            }
            else
            {
                hex.renderer.material = GetMaterial(typeMaterial);
            }
        }

        StartCoroutine(SetDissolveCoroutine(objDissolve));
    }

    Material GetMaterial(TypeMaterial typeMaterial)
    {
        if (typeMaterial == TypeMaterial.BumpMap)
        {
            return _bumpMap;
        }
        else if (typeMaterial == TypeMaterial.CarPaint)
        {
            return _carPaint;
        }
        else if (typeMaterial == TypeMaterial.Dirt)
        {
            return _dirt;
        }
        else if (typeMaterial == TypeMaterial.Mud)
        {
            return _mud;
        }
        else if (typeMaterial == TypeMaterial.Rough)
        {
            return _rough;
        }
        else
        {
            return null;
        }
    }

    void SetOtherMaterial(List<Hex> obj)
    {
        var material = GetMaterial(TypeMaterial);
        if (material == null)
        {
            return;
        }

        foreach (var hex in obj)
        {
            hex.renderer.material = material;
        }
    }

    void SetDefault(List<Hex> obj, bool randomColor)
    {
        foreach (var hex in obj)
        {
            SetDefault(hex.renderer, randomColor);
        }
    }

    void SetDefault(MeshRenderer renderer, bool randomColor)
    {
        renderer.material = _default;
        Color color;
        if (randomColor)
        {
            color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        else
        {
            float rnd = Random.Range(0f, 1f);
            color = new Color(rnd, rnd, rnd);
        }
        renderer.material.color = color;
    }

    void SetDissolve(List<Hex> obj, bool randomColor)
    {        
        foreach (var hex in obj)
        {
            SetDissolve(hex.renderer, randomColor);
        }

        StartCoroutine(SetDissolveCoroutine(obj));
    }

    void SetDissolve(MeshRenderer renderer, bool randomColor)
    {
        renderer.material = _dissolve;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
        Color color;
        if (randomColor)
        {
            color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        }
        else
        {
            float rnd = Random.Range(0f, 1f);
            color = new Color(rnd, rnd, rnd);
        }
        renderer.material.SetColor("_MainColor", color);
    }

    IEnumerator SetDissolveCoroutine(List<Hex> obj)
    {
        foreach (var hex in obj)
        {
            hex.renderer.material.SetFloat("_Range", Random.Range(-2f, 0f));
        }

        float time = 0;
        while (time < 5)
        {
            float deltaTime = Time.deltaTime;
            foreach (var hex in obj)
            {
                var current = hex.renderer.material.GetFloat("_Range");
                hex.renderer.material.SetFloat("_Range", current + deltaTime);
            }
            time += deltaTime;
            yield return null;
        }
        yield return null;
    }    
}

public enum TypeMaterial
{
    Random = 0,
    Default = 1,
    DefaultRandomColor = 2,
    Dissolve = 3,
    DissolveRandomColor = 4,
    BumpMap = 5,
    CarPaint = 6,
    Dirt = 7,
    Mud = 8,
    Rough = 9
};
