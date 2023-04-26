using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditableDepartmentGraph : MonoBehaviour
{
    [SerializeField] private Slider _sliderComponent;
    [SerializeField] private GameObject _closedImage;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Image _oldValueImage;
    [SerializeField] private Image _minValueImage;

    [SerializeField] private Color _disabledColor;
    [SerializeField] private Color _enabledColor;

    [HideInInspector] public Department AffectedDep;
    [HideInInspector] public int RealValue;

    public void SetUpSliderAsEmployeeCount()
    {
        _sliderComponent.enabled = AffectedDep.unlocked && !AffectedDep.onStrike;

        _closedImage.SetActive(!AffectedDep.active || AffectedDep.onStrike);

        _sliderComponent.maxValue = AffectedDep.departmentTiers[^1].employeeThreshold;
        _sliderComponent.minValue = 0;
        _sliderComponent.value = RealValue = AffectedDep.employees;

        _valueText.text = AffectedDep.employees.ToString();

        _oldValueImage.fillAmount = (_sliderComponent.value - _sliderComponent.minValue) / (_sliderComponent.maxValue - _sliderComponent.minValue);
        _minValueImage.fillAmount = AffectedDep.departmentTiers[0].employeeThreshold / _sliderComponent.maxValue;
    }

    public void SetUpSliderAsWage(int minWage, int maxWage)
    {
        _sliderComponent.enabled = AffectedDep.unlocked;

        _closedImage.SetActive(!AffectedDep.active || AffectedDep.onStrike);

        _sliderComponent.maxValue = maxWage;
        _sliderComponent.minValue = minWage;
        _sliderComponent.value = RealValue = AffectedDep.salary;

        _valueText.text = AffectedDep.salary.ToString();

        _oldValueImage.fillAmount = (_sliderComponent.value - _sliderComponent.minValue) / (_sliderComponent.maxValue - _sliderComponent.minValue);
    }

    public void UpdateDepartmentStaff(float value)
    {
        if (!AffectedDep.onStrike)
        {
            if (value < AffectedDep.departmentTiers[0].employeeThreshold)
            {
                RealValue = 0;
                _closedImage.SetActive(true);


                _valueText.color = _disabledColor;
            }
            else
            {
                RealValue = (int)value;
                _closedImage.SetActive(false);

                _valueText.color = _enabledColor;
            }

            UpdateGraphValue();
        }
    }

    public void UpdateDepartmentWages(float value)
    {
        RealValue = (int)value;

        _valueText.color = RealValue <= _sliderComponent.minValue ? _disabledColor : _enabledColor;

        UpdateGraphValue();
    }

    private void UpdateGraphValue()
    {
        _valueText.text = RealValue.ToString();
    }
}
