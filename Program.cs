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
string taskID = "rEu25ZX"; // We get the taskID from the previous response and use it to get the task (look at the console output to find the taskID)

//#### THIRD TASK 
// Fetch the details of the task from the server.
Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
Console.WriteLine(task3Response);

Task task3 = JsonSerializer.Deserialize<Task>(task3Response.content);
Console.WriteLine($"Task: {ANSICodes.Effects.Bold}{task3?.title}{ANSICodes.Reset}\n{task3?.description}\nParameters: {Colors.Cyan}{task3?.parameters}{ANSICodes.Reset}");


    string regularNumberParameters = string.Join(",", task3?.parameters
            .Split(',')
            .Select(RomanToInt)
            .Select(n => n.ToString()));

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

 public class Task
{
    public string? title { get; set; }
    public string? description { get; set; }
    public string? taskID { get; set; }
    public string? userID { get; set; }
    public string? parameters { get; set; }
}