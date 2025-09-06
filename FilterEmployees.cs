using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
{
    if (employees.Count() == 0)
    {
        return "[]";
    }

    //Save employees that satisfy the conditions
    var filteredEmployees = employees.Where(employee => employee.Age >= 25 && employee.Age <= 40
                            && (employee.Department == "IT" || employee.Department == "Finance")
                            && employee.Salary >= 5000 && employee.Salary <= 9000
                            && employee.HireDate > new DateTime(2017, 1, 1))
                            .Select(employee => new
                            {
                                employee.Name,
                                employee.Salary
                            });

    var names = filteredEmployees.OrderByDescending(emp => emp.Name.Length).ThenBy(emp => emp.Name).Select(emp => emp.Name);
    var totalSalary = filteredEmployees.Sum(emp => emp.Salary);
    var averageSalary = filteredEmployees.Any() ? Math.Round(filteredEmployees.Average(emp => emp.Salary), 2) : 0;
    var minSalary = filteredEmployees.Any() ? filteredEmployees.Min(emp => emp.Salary) : 0;
    var maxSalary = filteredEmployees.Any() ? filteredEmployees.Max(emp => emp.Salary) : 0;

    var result = new
    {
        Names = names,
        TotalSalary = totalSalary,
        AverageSalary = averageSalary,
        MinSalary = minSalary,
        MaxSalary = maxSalary,
        Count = filteredEmployees.Count()
    };

    return JsonSerializer.Serialize(result, new JsonSerializerOptions
    {
        //Allows Turkish characters to appear normally in JSON
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });
}
