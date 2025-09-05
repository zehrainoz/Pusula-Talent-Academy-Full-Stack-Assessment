using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Xml;

    public static string FilterPeopleFromXml(string xmlData)
    {
         XElement people;

        //Parse fragment
        try
        {
            people = XElement.Parse(xmlData);
        }
        catch (XmlException ex)
        {
            return JsonSerializer.Serialize(new { Error = "Invalid XML: " + ex.Message });
        }
        
        //Saves persons that satisfy the condition
        var persons = people.Elements("Person")
                            .Where(person => (int)person.Element("Age") > 30
                            && (string)person.Element("Department") == "IT"
                            && (int)person.Element("Salary") > 5000
                            && (DateTime)person.Element("HireDate") < new DateTime(2019, 1, 1))
                            .Select(person => new
                            {
                                Name = (string)person.Element("Name"),
                                Salary = (int)person.Element("Salary"),
                            });

        var personNames = persons.OrderBy(person => person.Name).Select(person => person.Name).ToList();
        var totalSalary = persons.Sum(person => person.Salary);
        var averageSalary = persons.Count() > 0 ? persons.Average(p => p.Salary) : 0;
        var maxSalary = persons.Count() > 0 ? persons.Max(p => p.Salary) : 0;

        var result = new
        {
            Names = personNames,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MaxSalary = maxSalary,
            Count = persons.Count()
        };

        return JsonSerializer.Serialize(result);
    } 