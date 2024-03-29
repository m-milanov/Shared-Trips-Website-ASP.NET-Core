﻿namespace SharedTrips.Data
{
    public class DataConstants
    {
        public class Trip
        {
            public const int MaxTripPassengers = 6;
            public const int MinTripPassengers = 1;

            public const int MaxPrice = 100;
            public const int MinPrice = 0;
        }

        public class Car 
        {
            public const int BrandMaxLength = 20;
            public const int BrandMinLength = 2;

            public const int ModelMaxLength = 30;
            public const int ModelMinLength = 2;

            public const int YearMaxVal = 2050;
            public const int YearMinVal = 1980;
        }

        public class Driver
        {
            public const int PhoneNumberMaxLength = 20;
            public const int PhoneNumberMinLength = 6;

            public const int NameMaxLength = 40;
            public const int NameMinLength = 2;
        }

        public class Passenger 
        {
            public const int NameMaxLength = 40;
            public const int NameMinLength = 2;

            public const int AgeMaxVal = 100;
            public const int AgeMinVal = 16;

            public const int PassWordMaxLength = 100;
            public const int PassWordMinLength = 6;
        }

    }
}
