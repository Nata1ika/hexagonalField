using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    void Awake()
    {
        HexCreator.ShowEvent += StartAnimation;
    }    

    void OnDestroy()
    {
        HexCreator.ShowEvent -= StartAnimation;
    }

    private void StartAnimation(List<Hex> obj)
    {        
        StartCoroutine(AnimationCoroutine(obj));
    }

    IEnumerator AnimationCoroutine(List<Hex> obj)
    {
        float[] wait = new float[obj.Count];
        for (int i = 0; i < wait.Length; i++)
        {
            wait[i] = Random.Range(-0.5f, 0f);
        }

        float time = 0;
        while (time < 0.7f)
        {
            float delta = Time.deltaTime;

            for (int i = 0; i < obj.Count; i++)
            {
                wait[i] += delta;
                if (wait[i] >= 0 && ! obj[i].gameObject.activeSelf)
                {
                    obj[i].gameObject.SetActive(true);
                }
            }

            time += delta;
            yield return null;
        }

        yield return null;
    }
}
