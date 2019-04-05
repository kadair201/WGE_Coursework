using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeSortScript : MonoBehaviour {

    int[] numbers;
    int initialNumbersLength;
    public InputField[] num;

	// Use this for initialization
	void Start () {
        numbers = new int[4];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SortNumbers()
    {
        for (int i = 0; i < 4; i++)
        {
            numbers[i] = System.Int32.Parse(num[i].text);
        }
        initialNumbersLength = numbers.Length;
        MergeSort(numbers);
    }

    void MergeSort(int[] numbers)
    {
        // if there is only one number, don't try to split into two arrays
        if (numbers.Length <= 1) return;

        int arrayLength = numbers.Length / 2;
        int[] firstHalf = new int[arrayLength];
        int[] secondHalf = new int[arrayLength];

        for (int i = 0; i < numbers.Length; i++)
        {
            if (i <= arrayLength-1)
            {
                firstHalf[i] = numbers[i];
                //Debug.Log("number " + firstHalf[i] + " added to firstHalf");
            }
            else if (i > arrayLength-1)
            {
                secondHalf[i - arrayLength] = numbers[i];
                //Debug.Log("number " + secondHalf[i - arrayLength] + " added to secondHalf");
            }
        }

        MergeSort(firstHalf);
        MergeSort(secondHalf);

        numbers = Merge(firstHalf, secondHalf);

        //if (numbers.Length == initialNumbersLength)
        //{
            for (int x = 0; x < numbers.Length; x++)
            {
                Debug.Log("Number " + x + " : " + numbers[x]);
            }
        //}
        
    }

    int[] Merge(int[] a, int[] b)
    {
        int[] merged = new int[a.Length + b.Length];
        int i, j, m;
        i = j = m = 0;

        while (i < a.Length && j < b.Length)
        {
            if (a[i] <= b[j])
            {
                merged[m] = a[i];
                i++;
                m++;
            }
            else
            {
                merged[m] = b[j];
                j++;
                m++;
            }
        }

        if (i < a.Length)
        {
            // add the rest of the elements in a to the end of merged
            for (int k = 0; k < a.Length; k++)
            {
                merged[m] = a[k];
                m++;
            }
        }
        else
        {
            // add the rest of the elements in b to the end of merged
            for (int k = 0; k < b.Length; k++)
            {
                merged[m] = b[k];
                m++;
            }
        }

        return merged;
    }
}
