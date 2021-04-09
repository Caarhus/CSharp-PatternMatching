using System;
using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;

namespace toll_calculator
{
    public class TollCalculator
    {
        /// <summary>
        /// CalculateToll
        ///     This function take a vehicle param and returns the calculated toll for that input.
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns value=decimal> </returns>
        /*
        public decimal CalculateToll(object vehicle)
        {
            switch (vehicle)
            {
                case Car c:
                    return 2.00m;
                case Taxi t:
                    return 3.50m;
                case Bus b:
                    return 5.00m;
                case DeliveryTruck t:
                    return 10.00m;
                case null:
                    throw new ArgumentNullException(paramName: nameof(vehicle), message: "Vehicle must not be null");
                default:
                    throw new ArgumentException(paramName: nameof(vehicle), message: "Not a known Vehicle type");
            }
        }
        */

        // Here's our first use case of Patterns becoming helpful!
        // We can use a switch expression that uses the Type Pattern (not the same as a switch statement)
        // This new extension combine testing a value and extracting that information.
        /*
        public decimal CalculateToll(object vehicle) =>
           vehicle switch
           {
               Car c => 2.00m,
               Taxi t => 3.50m,
               Bus b => 5.00m,
               DeliveryTruck t => 10.00m,
               { } => throw new ArgumentException(paramName: nameof(vehicle), message: "Not a known Vehicle type"),
               null => throw new ArgumentNullException(paramName: nameof(vehicle), message: "Vehicle must not be null")
           };
        */

        // Part 2
        // Now the city is pushing for us to be more eco friendly...and I guess COVID is no longer a thing!!!
        // The toll authority wants to encourage vehicles to travel at maximum capacity
        // They've decided to charge more when vehicles have fewer passengers,
        // and encourage fuller vehicles by offering lower pricing
        /*
        public decimal CalculateToll(object vehicle) =>
           vehicle switch
           {
               Car { Passengers: 0 }    => 2.00m + 0.50m,
               Car { Passengers: 1 }    => 2.0m,
               Car { Passengers: 2 }    => 2.0m - 0.50m,
               Car c                    => 2.00m - 1.0m,
               
               Taxi { Fares: 0 }        => 3.50m + 1.00m,
               Taxi { Fares: 1 }        => 3.50m,
               Taxi { Fares: 2 }        => 3.50m - 0.50m,
               Taxi t                   => 3.50m - 1.00m,
               
               Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => 5.00m + 2.00m,
               Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => 5.00m - 1.00m,
               Bus b                    => 5.00m,
               
               DeliveryTruck t when (t.GrossWeightClass > 5000) => 10.00m + 5.00m,
               DeliveryTruck t when (t.GrossWeightClass < 3000) => 10.00m - 2.00m,
               DeliveryTruck t          => 10.00m,
               
               { } => throw new ArgumentException(paramName: nameof(vehicle), message: "Not a known Vehicle type"),
               null => throw new ArgumentNullException(paramName: nameof(vehicle), message: "Vehicle must not be null")
           };
        */

        // However, it seems like we have another pattern forming!
        // You can use switch statements for these recursive patterns

        public decimal CalculateToll(object vehicle) =>
           vehicle switch
           {
               Car c => c.Passengers switch
               {
                   0 => 2.00m + 0.5m,
                   1 => 2.0m,
                   2 => 2.0m - 0.5m,
                   _ => 2.00m - 1.0m
               },

               Taxi t => t.Fares switch
               {
                   0 => 3.50m + 1.00m,
                   1 => 3.50m,
                   2 => 3.50m - 0.50m,
                   _ => 3.50m - 1.00m
               },

               Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => 5.00m + 2.00m,
               Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => 5.00m - 1.00m,
               Bus b => 5.00m,

               DeliveryTruck t when (t.GrossWeightClass > 5000) => 10.00m + 5.00m,
               DeliveryTruck t when (t.GrossWeightClass < 3000) => 10.00m - 2.00m,
               DeliveryTruck t => 10.00m,

               { } => throw new ArgumentException(paramName: nameof(vehicle), message: "Not a known Vehicle type"),
               null => throw new ArgumentNullException(paramName: nameof(vehicle), message: "Vehicle must not be null")
           };
    }
}