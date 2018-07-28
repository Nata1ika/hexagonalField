using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] HexCreator _hexCreator;
    [SerializeField] CinemachineFreeLook _freeLookVirtualCamera;

    private void Awake()
    {
        HexCreator.CreateEvent += UpdatePosition;
    }

    private void OnDestroy()
    {
        HexCreator.CreateEvent -= UpdatePosition;
    }

    private void UpdatePosition(List<Hex> obj)
    {
        transform.position = GetCentrPosition();
        float radius = MaxRadius();
        _freeLookVirtualCamera.m_Orbits[0].m_Radius = radius * 2f;
        _freeLookVirtualCamera.m_Orbits[1].m_Radius = radius * 1.5f;
        _freeLookVirtualCamera.m_Orbits[2].m_Radius = radius * 1.2f;
    }

    /// <summary>
    /// положение центра
    /// </summary>
    Vector3 GetCentrPosition()
    {
        var first = _hexCreator.GetPosition(0, 0);
        var last = _hexCreator.GetPosition(_hexCreator.CountX - 1, _hexCreator.CountZ - 1);

        return 0.5f * (first + last);
    }

    float MaxRadius()
    {
        return Mathf.Max(HexCreator.DELTAX * _hexCreator.CountX, HexCreator.DELTAZ * _hexCreator.CountZ);
    }
}
