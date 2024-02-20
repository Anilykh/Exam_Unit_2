using HTTPUtils;
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
string taskID = "otYK2"; // We get the taskID from the previous response and use it to get the task (look at the console output to find the taskID)

//#### FIRST TASK 
// Fetch the details of the task from the server.
Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + taskID); // Get the task from the server
Console.WriteLine(task1Response);

Task task1 = JsonSerializer.Deserialize<Task>(task1Response.content);
Console.WriteLine($"Task: {ANSICodes.Effects.Bold}{task1?.title}{ANSICodes.Reset}\n{task1?.description}\nParameters: {Colors.Cyan}{task1?.parameters}{ANSICodes.Reset}");

var answerArray = task1?.parameters.Split(',').Select(p => p.Trim()).Distinct().OrderBy(p => p).ToArray();
string answer = string.Join(",", answerArray);

Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + taskID, answer.ToString());
Console.WriteLine($"Answer: {Colors.Green}{task1AnswerResponse}{ANSICodes.Reset}");

taskID = "KO1pD3";
Console.WriteLine("\n----------------------------\n");








