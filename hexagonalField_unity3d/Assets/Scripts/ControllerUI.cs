using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] HexCreator _hexCreator;
    [SerializeField] MaterialController _materialController;
    [SerializeField] InputField _inputCountX;
    [SerializeField] InputField _inputCountZ;
    [SerializeField] Button[] _buttons;
    Image _activeMaterial;

    private void Start()
    {
        _inputCountX.onEndEdit.AddListener(ChangeCountX);
        _inputCountZ.onEndEdit.AddListener(ChangeCountZ);

        _inputCountX.text = "5";
        _inputCountZ.text = "5";
        _hexCreator.CountX = 5;
        _hexCreator.CountZ = 5;

        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;
            Button button = _buttons[i];
            _buttons[i].onClick.AddListener(() => { ChangeMaterial(index, button); });
        }

        ChangeMaterial(0, _buttons[0]);
    }    

    private void OnDestroy()
    {
        _inputCountX.onEndEdit.RemoveListener(ChangeCountX);
        _inputCountZ.onEndEdit.RemoveListener(ChangeCountZ);
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].onClick.RemoveAllListeners();
        }
    }

    public void Go()
    {
        gameObject.SetActive(false);
        _hexCreator.Show();
    }

    public void ShowUI()
    {
        _hexCreator.Hide();
        gameObject.SetActive(true);
    }

    void ChangeMaterial(int index, Button button)
    {
        if (_activeMaterial != null)
        {
            _activeMaterial.color = Color.white;
        }
        _activeMaterial = button.image;
        _activeMaterial.color = Color.green;

        _materialController.TypeMaterial = (TypeMaterial)index;
    }

    private void ChangeCountX(string value)
    {
        int result;
        if (int.TryParse(value, out result))
        {
            _hexCreator.CountX = result;
        }
        else
        {
            _inputCountX.text = _hexCreator.CountX.ToString();
        }
    }

    private void ChangeCountZ(string value)
    {
        int result;
        if (int.TryParse(value, out result))
        {
            _hexCreator.CountZ = result;
        }
        else
        {
            _inputCountX.text = _hexCreator.CountZ.ToString();
        }
    }
}
