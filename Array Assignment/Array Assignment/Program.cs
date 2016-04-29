using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Assignment {


    /// <summary>
    /// 
    /// ---------------CLASS COMMENT---------------
    /// Name:               Michael Leontieff-Smith
    /// Student Number:     #n9455396
    /// Date:               24/8/15
    /// 
    /// Rainfall Calculator - Calculates the chance 
    /// of rain throughout the year using random
    /// values. Statistics pertianing to a user-
    /// selected monththis
    /// information is printed at the end
    /// 
    /// 
    /// </summary>
    class Program {
        
        // enum expressing the months of the year
        enum Months {January = 1, February, March, April, May, June, July, August, September, October, November, December}
        
        //length of month array which is parralell to the Months enum
        static int[] daysInMonth = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        //months in year constant
        const int MONTHS_IN_YEAR = 12;

        /// <summary>
        /// Program Entry Point, Calls the required functions to generate and print rainfall reports
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            
            // initialise variables allowing values to be passed through to
            // functions
            int month;
            int[][] rainfall;
            bool keep_running = true;

            // Call Greeting
            Welcome();

            // Call required functions, others cascade from others
            rainfall = Generate_Rainfall();

            do
            {
                month = GrabOption();
                keep_running = GenerateRainfallReport(month, rainfall);
            } while (keep_running);

            ExitProgram();

        }//end Main



        /// <summary>
        /// Generates the rainfall values using random numbers and inputting them
        /// into their respective locations in the rainfall array
        /// </summary>
        /// <returns>returns the completed, generated rainfall report</returns>
        static int[][] Generate_Rainfall()
        {
            // initialise random number generators
            Random chanceOfRain = new Random();
            Random amountOfRain = new Random();

            //intialise array with first dimension representing the numver of months
            int[][] rainfall = new int[MONTHS_IN_YEAR][];

            // loop through the first dimension
            for (int i = 0; i < MONTHS_IN_YEAR; i++)
            {
                //initialise dimension with approprite length of month
                rainfall[i] = new int[daysInMonth[i]];

                //for each of the elements in the second dimension
                //1. Determine whether it will rain
                //2. If it will rain (chance_of_rain = 1 not 2, 3, 4)
                //   then determine the amount of rain for that day
                for (int j = 0; j < daysInMonth[i]; j++)
                {
                    int amount_of_rain = 0;
                    // Pick random number between 1 and 4 inclusive
                    // This determines the chance of rain 25%
                    int chance_of_rain = chanceOfRain.Next(1, 5);
                    if (chance_of_rain == 1)
                    {
                        amount_of_rain = amountOfRain.Next(1, 28);
                    }
                    rainfall[i][j] = amount_of_rain;
                }
            }
            PrintRainfall(rainfall);
            //return the completed array
            return rainfall;
        }// Generate Rainfall

        /// <summary>
        /// Accepts generated array parameter and prints out rainfall 
        /// corresponding to Month Enum
        /// </summary>
        /// <param name="rainfall">Generated rainfall array</param>

        static void PrintRainfall(int[][] rainfall)
        {
            // for each month in the year
            // 1. Print the corresponding enum (month) pertaining to the following data
            // 2. Print the rainfall values that don't equal 0
            for (int i = 0; i < MONTHS_IN_YEAR; i++)
            {
                Console.Write("\t");
                Console.WriteLine("\n\n{0}: {1} days\n", (Months) i + 1, daysInMonth[i]);
                Console.Write("\t");

                // index values i and j pertain to the approprite element in the array
                for (int j = 0; j < rainfall[i].Length; j++)
                {
                    if (rainfall[i][j] != 0)
                    {     
                        Console.Write("{0} ", rainfall[i][j]);
                    }
                }
            }
        }// end PrintRainfall

        /// <summary>
        /// Grabs the users option for month-specific rainfall reporting and validates it 
        /// before returning to parent function
        /// </summary>
        /// <returns>Returns validated option</returns>
        static int GrabOption()
        {
            string user_input;
            int validated_input=0;
            bool keep_trying = true;
            string message = "\n\nEnter a value between 1 and 12 which corresponds to a month of the year: ";
            
            while (keep_trying)
            {
                Console.Write(message);
                user_input = Console.ReadLine();
                // if user input is of the valid type
                if (int.TryParse(user_input, out validated_input))
                {
                    // if value can cast to int but isn't within range
                    if (validated_input > 0 && validated_input <= 12)
                    {
                        keep_trying = false;
                    }
                    else
                    {
                        Console.WriteLine("\nError! {0} is out of range", user_input);
                    }
                }
                else
                {
                    Console.WriteLine("\nError! {0} is an invalid input", user_input);
                }
            }
            return validated_input;  
        } // end GrabOption

        /// <summary>
        /// Generic Greeting
        /// </summary>
        static void Welcome() {
            Console.WriteLine("\n\n\t Welcome to Yearly Rainfall Report \n\n");
        }//end Welcome

        /// <summary>
        /// Consolidates the function calling and printing of the month-specific
        /// report generation
        /// </summary>
        /// <param name="month">The user-selected month for which to generate the report</param>
        /// <param name="rainfall">The generated rainfall array which houses the rainfall data</param>
        static bool GenerateRainfallReport(int month, int[][] rainfall)
        {
            int days_with_rain, days_without_rain, 
            max_rain, max_rain_day;

            string user_input;
            
            double average_rainfall;
            
            // Convert month to value that can be used to reference 
            // array element (month < 12)
            month--;

            // Collect all the required statistics by calling the appropriate functions
            // and inputting their returning values into their respective variables
            days_without_rain = DaysWithoutRain(rainfall, month);
            days_with_rain = daysInMonth[month] - days_without_rain;
            max_rain = MaxRain(rainfall, month);
            max_rain_day = MaxRainDay(rainfall, month, max_rain);
            average_rainfall = AverageRainfall(rainfall, month);

            // Print each line of the report
            Console.WriteLine("\nThere were {0} days without rain in the month of {1} and {2} with rain", days_without_rain, (Months) month + 1, days_with_rain); // cast month to enum
            Console.WriteLine("\nand the maximum amount of rain on a single day in {0} was {1}mm", (Months) month + 1, max_rain); // cast month to enum 
            Console.WriteLine("\nWhich occured on day {0} of {1}", max_rain_day + 1, (Months) month + 1); // max_rain_day refers to index, add one to refer to enum value
            Console.WriteLine("\nand the average rainfall for {0} was {1:f2}ml", (Months) month + 1, average_rainfall);

            Console.Write("\nWould you like to display another month? (Y/N): ");
            user_input = Console.ReadLine();

            if (user_input == "y")
            {
                return true;
            }

            return false;
            
           
        } // end GenerateRainfallReport



        /// <summary>
        /// Calculates the number of days without rain
        /// </summary>
        /// <param name="rainfall">Generated rainfall array</param>
        /// <param name="month">month selcted by user</param>
        /// <returns></returns>
        static int DaysWithoutRain(int[][] rainfall, int month)
        {
            int no_rain_counter = 0;

            foreach (int i in rainfall[month])
            {
                // no rain clause
                if (i == 0)
                {
                    no_rain_counter++;
                }
            }

            //return number of days without rain
            return no_rain_counter;
        }

        /// <summary>
        /// Calculates the average rainfall for the given month
        /// </summary>
        /// <param name="rainfall">Generated rainfall array</param>
        /// <param name="month">user selected month</param>
        /// <returns></returns>
        static double AverageRainfall(int[][] rainfall, int month)
        {
            // initialise average as double
            double average_rainfall = 0, total_rainfall = 0;

            // for each element in the 2nd dimension (days of month)
            // add value to value, complete for each day of the month
            for (int i = 0; i < daysInMonth[month]; i++)
            {
                total_rainfall = total_rainfall + rainfall[month][i];
            }

            // calculate average
            average_rainfall = total_rainfall / daysInMonth[month];
            return average_rainfall;
        } // end AerageRainfall

        /// <summary>
        /// Calculates the Max Rain for the given month
        /// </summary>
        /// <param name="rainfall">Generated Rainfall Array</param>
        /// <param name="month">User Selected month</param>
        /// <returns></returns>
        static int MaxRain(int[][] rainfall, int month)
        {
            // initialise max rain as zero, this will increase as higher 
            // values are found
            int max_rain = 0;

            for (int i = 0; i < daysInMonth[month]; i++ )
            {
                // if the current index has element larger than the current max rain
                // then replace it
                if (rainfall[month][i] > max_rain)
                {
                    max_rain = rainfall[month][i];
                }
            }
            // return max rain
            return max_rain;
        } // end MaxRain


        /// <summary>
        /// Calculates the Max Rain day by accepting the date with the most rain 
        /// and searching the array for the value
        /// </summary>
        /// <param name="rainfall">Generated rainfall array</param>
        /// <param name="month">User selected month</param>
        /// <param name="max_rain">the max rain value generated by the MaxRain function</param>
        /// <returns>returns the day matching the max rainfall value, otherwise 0 (this code path isn't reached)</returns>
        static int MaxRainDay (int[][] rainfall, int month, int max_rain)
        {
            for (int i = 0; i < daysInMonth[month]; i++)
            {
                if (rainfall[month][i] == max_rain)
                {
                    // return index of max rain day
                    return i;
                }
            }
            return 0;
        } // end MaxRainDay
        
        /// <summary>
        /// Called when final operation completed
        /// </summary>
        static void ExitProgram() {
            Console.Write("\n\nPress any key to exit.");
            Console.ReadKey();
        }//end ExitProgram

    }//end class
}//end namespace
