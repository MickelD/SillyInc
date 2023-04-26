using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Calendar : MonoBehaviour
{
    [SerializeField] private int _daysInAMonth;
    private int _daysInAYear;
    [SerializeField] private int _startingYear;
    [SerializeField] private int _startingDay;
    [SerializeField] private string[] _months;

    [SerializeField] private Transform _days;
    [SerializeField] private TextMeshProUGUI _monthDisplay;
    [SerializeField] private TextMeshProUGUI _yearDisplay;

    private void OnEnable()
    {
        GameManager.OnUpdateDay += UpdateCalendar;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateDay -= UpdateCalendar;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.Instance.StartNewDay();
        }
    }

    private void UpdateCalendar(int d)
    {
        int dd = _startingDay + d;

        _daysInAYear = _daysInAMonth * _months.Length;

        _startingYear += (dd % _daysInAYear == 0)? 1:0;

        int mm = (dd / (_daysInAMonth) % _months.Length);
        dd = (dd % _daysInAMonth) + 1;

        _yearDisplay.text = _startingYear.ToString();
        _monthDisplay.text = _months[mm];

        foreach (Transform child in _days)
        {
            child.gameObject.SetActive((child.GetSiblingIndex()) < dd);
        }
    }

}
