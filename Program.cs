﻿using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using System.Reflection;


Console.Clear();
Console.WriteLine("Starting Assignment 2");


const string myPersonalID = "bcfb27b14b19129d0527649b878a2103a36123b692972440bf7e19fdc176ae38"; 
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/"; 
const string taskEndpoint = "task/";  
string taskID = ""; 


HttpUtils httpUtils = HttpUtils.instance;
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Magenta}{startRespons}{ANSICodes.Reset}\n\n"); 


//#### FIRST TASK 

    taskID = "otYK2";

    Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // 
    Console.WriteLine(task1Response);

    Task task1 = JsonSerializer.Deserialize<Task>(task1Response.content);
    Console.WriteLine($"Task: {ANSICodes.Effects.Bold}{task1?.title}{ANSICodes.Reset}\n{task1?.description}\nParameters: {Colors.Cyan}{task1?.parameters}{ANSICodes.Reset}");

    var answerArray = task1?.parameters.Split(',').Select(p => p.Trim()).Distinct().OrderBy(p => p).ToArray();
    string answer = string.Join(",", answerArray);

    Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, answer.ToString());
    Console.WriteLine($"Answer: {Colors.Green}{task1AnswerResponse}{ANSICodes.Reset}");


Console.WriteLine("\n----------------------------\n");

//#### SECOND TASK 

    taskID = "KO1pD3";

    Response task2Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); 
    Console.WriteLine(task2Response);

    Task task2 = JsonSerializer.Deserialize<Task>(task2Response.content);
    Console.WriteLine($"Task: {ANSICodes.Effects.Bold}{task2?.title}{ANSICodes.Reset}\n{task2?.description}\nParameters: {Colors.Cyan}{task2?.parameters}{ANSICodes.Reset}");

    
    
        int[] numbers = task2.parameters.Split(',').Select(int.Parse).ToArray();

        
        int commonDifference = numbers[1] - numbers[0];

        int nextNumber = numbers.Last() + commonDifference;

        string answerString = nextNumber.ToString();
    

    Response task2AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, answerString);
    Console.WriteLine($"\nAnwser: {Colors.Green}{task2AnswerResponse}{ANSICodes.Reset}");


Console.WriteLine("\n----------------------------\n");


//#### THIRD TASK

    taskID = "rEu25ZX";

    Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); 
    Console.WriteLine(task3Response);

    Task task3 = JsonSerializer.Deserialize<Task>(task3Response.content);
    Console.WriteLine($"Task: {ANSICodes.Effects.Bold}{task3?.title}{ANSICodes.Reset}\n{task3?.description}\nParameters: {Colors.Cyan}{task3?.parameters}{ANSICodes.Reset}");


    string regularNumberParameters = string.Join(",", task3?.parameters.Split(',').Select(RomanToInt).Select(n => n.ToString()));
            
    Response task3AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, regularNumberParameters);
    Console.WriteLine($"Answer: {Colors.Green}{task3AnswerResponse}{ANSICodes.Reset}");

    static int RomanToInt(string s)
    {
        Dictionary<char, int> romanValues = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        int total = 0;

        for (int i = 0; i < s.Length; i++)
        {
            int currentValue = romanValues[s[i]];

            if (i < s.Length - 1 && romanValues[s[i]] < romanValues[s[i + 1]])
            {
                total -= currentValue;
            }
            else
            {
                total += currentValue;
            }
        }return total;
    }
   Console.WriteLine("\n----------------------------\n");

//#### FOURTH TASK 

        taskID = "kuTw53L"; 
        
        Response task4Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); 
        Console.WriteLine(task4Response);

        Task task4 = JsonSerializer.Deserialize<Task>(task4Response.content);
        Console.WriteLine($"Task: {ANSICodes.Effects.Bold}{task4?.title}{ANSICodes.Reset}\n{task4?.description}\nParameters: {Colors.Cyan}{task4?.parameters}{ANSICodes.Reset}");

        List<int> Numbers = task4?.parameters.Split(',').Select(int.Parse).ToList();

        List<int> primeNumbers = GetPrimeNumbers(Numbers);

        string primeNumbersString = string.Join(",", primeNumbers);

        Response task4AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, primeNumbersString);
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