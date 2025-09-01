using System;
using System.Linq;
using System.Collections.Generic;

    public static void MaxIncreasingSubArrayAsJson(List<int> numbers)
    {

        if (numbers.Count == 0)
        {
            return;
        }

        List<List<int>> increasingSubArrays = new List<List<int>>();
        List<int> currentSublist = new List<int>();

        //Find and save increasing subArrays
        for (int i = 0; i < numbers.Count; i++)
        {
            if (currentSublist.Count == 0)
            {
                currentSublist.Add(numbers[i]);
            }

            else if (numbers[i] > currentSublist[currentSublist.Count - 1])
            {
                currentSublist.Add(numbers[i]);
            }

            else
            {
                increasingSubArrays.Add(new List<int>(currentSublist));
                currentSublist = new List<int> { numbers[i] };
            }
        }


        increasingSubArrays.Add(new List<int>(currentSublist));

        var maxSubarray = increasingSubArrays.OrderByDescending(sub => sub.Sum()).First();

        
        // Test
        foreach (var max in maxSubarray)
        {
            Console.WriteLine(max);
        }
        

    }


