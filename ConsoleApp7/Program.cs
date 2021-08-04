using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input demand 1 ");
            int aDemand = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Input demand 2");
            int bDemand = Convert.ToInt32(Console.ReadLine());

            var tomatoes = new Boolean[aDemand, bDemand];

            for (int i = 0; i < aDemand; i++)
            {
                for (int j = 0; j < bDemand; j++)
                {
                    Console.WriteLine("Input state of tomato(true if you want 1, false if you want 0");
                    tomatoes[i, j] = Convert.ToBoolean(Console.ReadLine());
                }
            }

            int result = 0;

            if (!isContainNotFresh(tomatoes))
            {
                Console.WriteLine("Not contains not fresh tomatoes!");
            }
            else
            {
                while (isAllNotFresh(tomatoes))
                {
                    tomatoes = OneLoop(tomatoes);
                    result++;
                }
            }

            Console.WriteLine($"Result: {result}");

        }

        static void Print(bool[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.WriteLine($"{ array[i, j]} ");
                }

                Console.WriteLine();
            }
        }

        static bool[,] OneLoop(bool[,] currentArray)
        {
            var resultArray = new Boolean[currentArray.GetLength(0), currentArray.GetLength(1)];

            for (int i = 0; i < currentArray.GetLength(0); i++)
            {
                for (int j = 0; j < currentArray.GetLength(1); j++)
                {
                    resultArray[i, j] = currentArray[i, j];
                }
            }

            for (int i = 0; i < currentArray.GetLength(0); i++)
            {
                for (int j = 0; j < currentArray.GetLength(1); j++)
                {
                    if (currentArray[i, j] == true)
                    {
                        //check center site
                        if (i != 0 && j != 0 && i != currentArray.GetLength(0) -1 && j != currentArray.GetLength(1) - 1)
                        {
                            resultArray[i + 1, j] = true;
                            resultArray[i, j + 1] = true;
                            resultArray[i + 1, j + 1] = true;
                            resultArray[i - 1, j + 1] = true;
                            resultArray[i - 1, j - 1] = true;
                            resultArray[i, j - 1] = true;
                            resultArray[i + 1, j - 1] = true;
                            resultArray[i - 1, j] = true;
                        }

                        //left top corner
                        if (i == 0 && j == 0)
                        {
                            if (currentArray.GetLength(0) > 1)
                            {
                                resultArray[i + 1, j] = true; //In case if we have the dimension less than 1
                            }

                            if (currentArray.GetLength(1) > 1)
                            {
                                resultArray[i, j + 1] = true;
                            }

                            if (currentArray.GetLength(0) > 1 && currentArray.GetLength(1) > 1)
                            {
                                resultArray[i + 1, j + 1] = true;
                            }
                        }

                        //right top corner
                        if (i == 0 - 1 && j == currentArray.GetLength(1) - 1)
                        {
                            if (currentArray.GetLength(0) > 1)
                            {
                                resultArray[i - 1, j] = true;
                            }

                            if (currentArray.GetLength(1) > 1)
                            {
                                resultArray[i, j - 1] = true;
                            }

                            if (currentArray.GetLength(0) > 1 && currentArray.GetLength(1) > 1)
                            {
                                resultArray[i - 1, j - 1] = true;
                            }
                        }

                        //right bottom corner
                        if (i == currentArray.GetLength(0) - 1 && j == currentArray.GetLength(1) - 1)
                        {
                            if (currentArray.GetLength(0) > 1)
                            {
                                resultArray[i + 1, j] = true;
                            }

                            if (currentArray.GetLength(1) > 1)
                            {
                                resultArray[i, j - 1] = true;
                            }

                            if (currentArray.GetLength(0) > 1 && currentArray.GetLength(1) > 1)
                            {
                                resultArray[i - 1, j + 1] = true;
                            }
                        }

                        //left bottom corner
                        if (i == currentArray.GetLength(0) - 1 && j == currentArray.GetLength(1) - 1)
                        {
                            if (currentArray.GetLength(0) > 1)
                            {
                                resultArray[i + 1, j] = true;
                            }

                            if (currentArray.GetLength(1) > 1)
                            {
                                resultArray[i, j + 1] = true;
                            }

                            if (currentArray.GetLength(0) > 1 && currentArray.GetLength(1) > 1)
                            {
                                resultArray[i + 1, j + 1] = true;
                            }
                        }

                        //top side
                        if (i == 0 && j != 0 && j != currentArray.GetLength(1) - 1)
                        {
                            resultArray[i, j - 1] = true;
                            resultArray[i + 1, j + 1] = true;
                            resultArray[i + 1 , j] = true;
                            resultArray[i + 1, j + 1] = true;
                            resultArray[i, j + 1] = true;
                        }

                        //right side
                        if (j == currentArray.GetLength(1) - 1 && i != 0 && i != currentArray.GetLength(0) - 1)
                        {
                            resultArray[i + 1, j] = true;
                            resultArray[i - 1, j + 1] = true;
                            resultArray[i, j - 1] = true;
                            resultArray[i - 1, j - 1] = true;
                            resultArray[i - 1, j] = true;
                        }

                        //bottom side
                        if (i == currentArray.GetLength(1) - 1 && j != 0 && j == currentArray.GetLength(0) - 1)
                        {
                            resultArray[i, j + 1] = true;
                            resultArray[i - 1, j + 1] = true;
                            resultArray[i - 1, j] = true;
                            resultArray[i - 1, j - 1] = true;
                            resultArray[i, j - 1] = true;
                        }

                        //left side
                        if (j == 0 && i != 0 && i < currentArray.GetLength(1) - 2)
                        {
                            resultArray[i - 1, j] = true;
                            resultArray[i - 1, j + 1] = true;
                            resultArray[i, j + 1] = true;
                            resultArray[i + 1, j + 1] = true;
                            resultArray[i + 1, j] = true;
                        }
                    }
                }
            }

            return resultArray;

        }

        //check if array contain at least one is not fresh
        static bool isContainNotFresh(bool[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == true)
                        return true;
                }
            }

            return false;
        }
        
        static bool isAllNotFresh(bool[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == false)
                        return true;
                }
            }

            return false;
        }

    }
}
