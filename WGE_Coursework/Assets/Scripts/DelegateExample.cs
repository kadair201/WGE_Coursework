using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateExample {
    // this delegate gives a template for a function that will take an int and return an int
    // we don't actually know what it will do or return until this function is actually implemented
    delegate int NumberChanger(int number);


    public void Start()
    {
        // starting number
        int number = 10;


        // first we want to double this number so we make a new NumberChanger and pass in the DoubleNumberChanger function defined below
        NumberChanger doubler = new NumberChanger(DoubleNumberChanger); // Notice we pass in the function DoubleNumberChanger without the brackets because we're not calling it
        

        // now we call our doubler just like a function, and this literally just finds DoubleNumberChanger and calls it
        int doubledNumber = doubler(number);


        // now we want to half the number again so we make another NumberChanger but pass in the HalfNumberChanger instead
        NumberChanger halver = new NumberChanger(HalfNumberChanger);

        int halvedNumber = halver(number);
    }

    // and these are our actual defined functions, they conform to the delegate NumberChanger at the top because they return an int and recieve an int
    //    delegate int NumberChanger(int number); -- see?
    int DoubleNumberChanger(int number)
    {
        return number * 2;
    }

    int HalfNumberChanger(int number)
    {
        return number / 2;
    }
}
