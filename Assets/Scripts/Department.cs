using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Department
{
    public string name;
    public bool active;
    public bool unlocked;
    public HiringTiers[] departmentTiers;
    public int employees;
    public int salary;

    public bool onStrike = false;

    public Department(string name, bool active, bool unlocked, HiringTiers[] departmentTiers, int employees, int salary)
    {
        this.name = name;
        this.active = active;
        this.unlocked = unlocked;
        this.departmentTiers = departmentTiers;
        this.employees = employees;
        this.salary = salary;
    }

    public void SetEmployees(int set)
    {
        int previousStaff = employees;

        employees = set;

        active = employees > 0;
    }
}