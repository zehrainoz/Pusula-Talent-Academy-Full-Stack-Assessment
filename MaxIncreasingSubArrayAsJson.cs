using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        if (numbers.Count == 0)
        {
            return "[]";
        }

        List<List<int>> increasingSubArrays = new List<List<int>>();
        List<int> currentSubArray = new List<int>();

        //Find and save increasing subArrays
        for (int i = 0; i < numbers.Count; i++)
        {
            if (currentSubArray.Count == 0)
            {
                currentSubArray.Add(numbers[i]);
            }

            //Add current element if greater than the last element in the subArray
            else if (numbers[i] > currentSubArray[currentSubArray.Count - 1])
            {
                currentSubArray.Add(numbers[i]);
            }

            //Add current subArray when the sequence is broken
            else
            {
                increasingSubArrays.Add(new List<int>(currentSubArray));
                currentSubArray = new List<int> { numbers[i] };
            }
        }

        //Add the last subarray because it wasn't added yet
        increasingSubArrays.Add(new List<int>(currentSubArray));

        //Save maximum increasing subArray
        var maxIncreasingSubArray = increasingSubArrays.OrderByDescending(sub => sub.Sum()).First();

        return JsonSerializer.Serialize(maxIncreasingSubArray);
    }


