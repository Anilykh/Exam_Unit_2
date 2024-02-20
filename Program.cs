﻿using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using System.Reflection;


Console.Clear();
Console.WriteLine("Starting Assignment 2");

// SETUP 
const string myPersonalID = "bcfb27b14b19129d0527649b878a2103a36123b692972440bf7e19fdc176ae38"; // GET YOUR PERSONAL ID FROM THE ASSIGNMENT PAGE https://mm-203-module-2-server.onrender.com/
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/"; // baseURl + startEndpoint + myPersonalID
const string taskEndpoint = "task/";   // baseURl + taskEndpoint + myPersonalID + "/" + taskID

// Creating a variable for the HttpUtils so that we dont have to type HttpUtils.instance every time we want to use it
HttpUtils httpUtils = HttpUtils.instance;

//#### REGISTRATION
// We start by registering and getting the first task
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Magenta}{startRespons}{ANSICodes.Reset}\n\n"); // Print the response from the server to the console
string taskID = "kuTw53L"; // We get the taskID from the previous response and use it to get the task (look at the console output to find the taskID)

//#### THIRD TASK 
// Fetch the details of the task from the server.
        Response task4Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
        Console.WriteLine(task4Response);

        Task task4 = JsonSerializer.Deserialize<Task>(task4Response.content);
        Console.WriteLine($"Task: {ANSICodes.Effects.Bold}{task4?.title}{ANSICodes.Reset}\n{task4?.description}\nParameters: {Colors.Cyan}{task4?.parameters}{ANSICodes.Reset}");

        List<int> numbers = task4?.parameters.Split(',').Select(int.Parse).ToList();

        List<int> primeNumbers = GetPrimeNumbers(numbers);

        string primeNumbersString = string.Join(",", primeNumbers);

        Response task3AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, primeNumbersString);
        Console.WriteLine($"Answer: {Colors.Green}{task3AnswerResponse}{ANSICodes.Reset}");
                                              
 static List<int> GetPrimeNumbers(List<int> numbers)
    {
        
        List<int> prime = new List<int>();

        foreach (int number in numbers)
        {
            if (IsPrime(number))
            {
                prime.Add(number);
            }
        }

        return prime;
    }

     static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }



 public class Task
{
    public string? title { get; set; }
    public string? description { get; set; }
    public string? taskID { get; set; }
    public string? userID { get; set; }
    public string? parameters { get; set; }
}