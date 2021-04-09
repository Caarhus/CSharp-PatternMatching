using System;
using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;

/*
 * Keywords: is, switch, when
 * 
 * Background Summary of Pattern Matching:
 *  - These new extensions combine testing a value and extracting that information.
 *  - They enable cleaner syntax to examine data and manipulate control flow based on any condition of that data.
 *  - Great for implementing behavior based on types and property values.
 *  - Can be combined with other techniques to create complete algorithms.
 *  
 *  Modern development often includes integrating data from multiple sources and presenting information
 *  and insights from that data in a single cohesive application. Since we don't always have control or
 *  access to all the types that represent the incoming data, pattern matching is a great feature to
 *  extend capabilities.
 * 
 * 
 * Scenarios on when to use:
 *  The objects you need to work with aren't in an object hierarchy that matches your goals.
 *  - e.g. working a distributed application with classes that are part of unrelated systems.
 * 
 *  The functionality you're adding isn't part of the core abstraction for the classes.
 *  - e.g. the toll paid by a vehicle changes for different types of vehicles, but the toll
 *    isn't a core function of the vehicle.
 * 
 * 
 * TIP: When the shape of the data and the operations on that data are not described together,
 *      pattern matching features in C# make it easier to work with!
 */

namespace toll_calculator
{
    class Program
    {
        /*
         * ===== TollTag: The Dallas Toll System =====
         * Note: Every vehicle must pay!!!
         * 
         * Charging Table:
         *  Car             $2.00.
         *  Taxi            $3.50.
         *  Bus             $5.00.
         *  DeliveryTruck   $10.00
         * 
         * Charging Table Part 2:
         *  Cars and taxis with no passengers pay extra $0.50 (solo driver).
         *  Cars and taxis with 2 passengers get $0.50 discount.
         *  Cars and taxis with 3+ passengers get a $1.00 discount.
         *  Buses that are less than 50% full pay an extra $2.00.
         *  Buses that are more than 90% full get a $1.00 discount.
         *  Trucks over 5000 lbs are charged an extra $5.00.
         *  Light trucks under 3000 lbs are given a $2.00 discount.
        */
        static void Main(string[] args)
        {
            //Part 1, showing off standard switch statement
            var tollCalc = new TollCalculator();
            var car = new Car();
            var taxi = new Taxi();
            var bus = new Bus();
            var truck = new DeliveryTruck();

            Console.WriteLine("Welcome to TollTag, Dallas' Premier Toll System\n");
            Console.WriteLine("Part 1:");
            Console.WriteLine($"The toll for a car is {tollCalc.CalculateToll(car)}");
            Console.WriteLine($"The toll for a taxi is {tollCalc.CalculateToll(taxi)}");
            Console.WriteLine($"The toll for a bus is {tollCalc.CalculateToll(bus)}");
            Console.WriteLine($"The toll for a truck is {tollCalc.CalculateToll(truck)}");
            try
            {
                tollCalc.CalculateToll("this will fail");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Caught an argument exception when using the wrong type");
            }
            try
            {
                tollCalc.CalculateToll(null!);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Caught an argument exception when using null");
            }

            
            
            //Part 2, show off new switch expression + patterns for adding occupants
            var tollCalc2 = new TollCalculator();
            var soloDriver = new Car();
            var twoRideShare = new Car { Passengers = 1 };
            var threeRideShare = new Car { Passengers = 2 };
            var fullVan = new Car { Passengers = 5 };
            var emptyTaxi = new Taxi();
            var singleFare = new Taxi { Fares = 1 };
            var doubleFare = new Taxi { Fares = 2 };
            var fullVanPool = new Taxi { Fares = 5 };
            var lowOccupantBus = new Bus { Capacity = 90, Riders = 15 };
            var normalBus = new Bus { Capacity = 90, Riders = 75 };
            var fullBus = new Bus { Capacity = 90, Riders = 85 };
            var heavyTruck = new DeliveryTruck { GrossWeightClass = 7500 };
            var mediumTruck = new DeliveryTruck { GrossWeightClass = 4000 };
            var lightTruck = new DeliveryTruck { GrossWeightClass = 2500 };
            Console.WriteLine("-------\n");
            Console.WriteLine("Part 2:");
            Console.WriteLine($"The toll for a solo driver is {tollCalc2.CalculateToll(soloDriver)}");
            Console.WriteLine($"The toll for a two ride share is {tollCalc2.CalculateToll(twoRideShare)}");
            Console.WriteLine($"The toll for a three ride share is {tollCalc2.CalculateToll(threeRideShare)}");
            Console.WriteLine($"The toll for a full van is {tollCalc2.CalculateToll(fullVan)}");
            Console.WriteLine($"The toll for an empty taxi is {tollCalc2.CalculateToll(emptyTaxi)}");
            Console.WriteLine($"The toll for a single fare taxi is {tollCalc2.CalculateToll(singleFare)}");
            Console.WriteLine($"The toll for a double fare taxi is {tollCalc2.CalculateToll(doubleFare)}");
            Console.WriteLine($"The toll for a full van taxi is {tollCalc2.CalculateToll(fullVanPool)}");
            Console.WriteLine($"The toll for a low-occupant bus is {tollCalc2.CalculateToll(lowOccupantBus)}");
            Console.WriteLine($"The toll for a regular bus is {tollCalc2.CalculateToll(normalBus)}");
            Console.WriteLine($"The toll for a bus is {tollCalc2.CalculateToll(fullBus)}");
            Console.WriteLine($"The toll for a truck is {tollCalc2.CalculateToll(heavyTruck)}");
            Console.WriteLine($"The toll for a truck is {tollCalc2.CalculateToll(mediumTruck)}");
            Console.WriteLine($"The toll for a truck is {tollCalc2.CalculateToll(lightTruck)}");
            try
            {
                tollCalc2.CalculateToll("this will fail");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Caught an argument exception when using the wrong type");
            }
            try
            {
                tollCalc2.CalculateToll(null);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Caught an argument exception when using null");
            }
        }
    }
}