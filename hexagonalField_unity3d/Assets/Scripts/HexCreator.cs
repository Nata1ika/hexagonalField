using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCreator : MonoBehaviour
{
    public static System.Action<List<Hex>> ShowEvent;
    public static System.Action HideEvent;

    [SerializeField] GameObject _prefab;

    public const float DELTAX = 1.503f;
    public const float DELTAZ = 1.732f;

    public int CountX { get; set; }
    public int CountZ { get; set; }

    List<Hex> _hex = new List<Hex>();

    private void OnDestroy()
    {
        _prefab.SetActive(true);
    }

    public void Show()
    {
        _prefab.SetActive(false);
        CteateOrDestroyNeedCount();
        SetPosition();

        if (ShowEvent != null)
        {
            ShowEvent(_hex);
        }
    }

    public void Hide()
    {
        foreach (var hex in _hex)
        {
            hex.gameObject.SetActive(false);
        }
        if (HideEvent != null)
        {
            HideEvent();
        }
    }

    /// <summary>
    /// добавляем или удаляем до достижения нужного количества
    /// </summary>
    void CteateOrDestroyNeedCount()
    {
        int currentCount = _hex.Count;
        int needCount = CountX * CountZ;
        if (currentCount < needCount)
        {
            for (int i = currentCount; i < needCount; i++)
            {
                _hex.Add(new Hex(Instantiate(_prefab, transform)));
            }
        }
        else if (currentCount > needCount)
        {
            for (int i = needCount; i < currentCount; i++)
            {
                GameObject.Destroy(_hex[0].gameObject);
                _hex.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// ставим все элементы в нужное место
    /// </summary>
    void SetPosition()
    {
        for (int i = 0; i < _hex.Count; i++)
        {
            int x = i / CountX;
            int z = i % CountX;

            _hex[i].transform.position = GetPosition(x, z);
        }
    }

    /// <summary>
    /// положение конкретного объекта по индексу
    /// </summary>
    public Vector3 GetPosition(int x, int z)
    {
        float offset = x % 2 == 0 ? 0 : DELTAZ / 2;
        return new Vector3(x * DELTAX, 0, offset + z * DELTAZ);
    }    
}

public class Hex
{
    public GameObject gameObject;
    public Transform transform;
    public MeshRenderer renderer;

    public Hex(GameObject obj)
    {
        gameObject = obj;
        transform = obj.transform;
        renderer = obj.GetComponent<MeshRenderer>();
        //animation = obj.GetComponent<Animation>();
    }
}
