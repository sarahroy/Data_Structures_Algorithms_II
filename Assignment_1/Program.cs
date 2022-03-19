/* COIS 3020H - Assignment 1
 * Group Members: Abhinav Manocha, Sarah Roy, Tawab Wahab, Audrey Uwa.
 * Sources: https://www.geeksforgeeks.org/shortest-path-unweighted-graph/
 * Program.cs
 * Comments from Marker: Great Work! Sarah. Your Fastest Route Method implementation is Impressive. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment_1;

/// <summary>
/// Implementing a "main method" to create your own Route Map and to drive test cases. 5%
/// NOTE: We are using .NET 6 which doesn't require an explicit main method.
/// </summary>

//Create a map
RouteMap AirCanadaMap = new RouteMap(); //creating AirCanada's Map

//Creating all 13 airports
AirportNode Calgary = new AirportNode("Calgary International Airport", "YYC");
AirportNode Edmonton = new AirportNode("Edmonton International Airport", "YEG");
AirportNode Fredericton = new AirportNode("Fredericton International Airport", "YFC");
AirportNode Gander = new AirportNode("Gander International Airport", "YQX");
AirportNode Halifax = new AirportNode("Halifax Stanfield International Airport", "YHZ");
AirportNode Moncton = new AirportNode("Greater Moncton Roméo LeBlanc International Airport", "YQM");
AirportNode Montreal = new AirportNode("Montréal–Trudeau International Airport", "YUL");
AirportNode Ottawa = new AirportNode("Ottawa Macdonald–Cartier International Airport", "YOW");
AirportNode Quebec = new AirportNode("Québec/Jean Lesage International Airport", "YQB");
AirportNode StJohns = new AirportNode("St. John's International Airport", "YYT");
AirportNode Toronto = new AirportNode("Toronto Pearson International Airport", "YYZ");
AirportNode Vancouver = new AirportNode("Vancouver International Airport", "YVR");
AirportNode Winnipeg = new AirportNode("Winnipeg International Airport", "YWG");

//Adding all 13 airports to the AirCanadaMap
AirCanadaMap.AddAirport(Calgary);
AirCanadaMap.AddAirport(Edmonton);
AirCanadaMap.AddAirport(Fredericton);
AirCanadaMap.AddAirport(Gander);
AirCanadaMap.AddAirport(Halifax);
AirCanadaMap.AddAirport(Moncton);
AirCanadaMap.AddAirport(Montreal);
AirCanadaMap.AddAirport(Ottawa);
AirCanadaMap.AddAirport(Quebec);
AirCanadaMap.AddAirport(StJohns);
AirCanadaMap.AddAirport(Toronto);
AirCanadaMap.AddAirport(Vancouver);
AirCanadaMap.AddAirport(Winnipeg);

/////////////////////////TEST CASES///////////////////////////

///Test1: Adding a destination that already exists
Console.WriteLine("\nTest1: Adding a destination that already exists");
AirCanadaMap.AddAirport(Toronto);

///Test2: Adding routes to and from airports that does exist in the map 
Console.WriteLine("\nTest2: Adding routes to and from airports that does exist in the map");
AirCanadaMap.AddRoute(Toronto, Vancouver);//Toronto->Vancouver
AirCanadaMap.AddRoute(Vancouver, Toronto);//Vancouver->Toronto
AirCanadaMap.AddRoute(Halifax, StJohns);//Halifax->StJohns
AirCanadaMap.AddRoute(Winnipeg, Toronto);//Winnipeg->Toronto
AirCanadaMap.AddRoute(Calgary, Edmonton);//Calgary->Edmonton
AirCanadaMap.AddRoute(Edmonton, Calgary);//Edmonton->Calgary
AirCanadaMap.AddRoute(Winnipeg, Calgary);//Winnipeg->Calgary
AirCanadaMap.AddRoute(Calgary, Winnipeg);//Calgary->Winnipeg
AirCanadaMap.AddRoute(Calgary, Vancouver);//Calgary->Vancouver
AirCanadaMap.AddRoute(Edmonton, Vancouver);//Edmonton->Vancouver
AirCanadaMap.AddRoute(Montreal, Toronto);//Montreal->Toronto
AirCanadaMap.AddRoute(Montreal, Halifax);//Montreal->Halifax
AirCanadaMap.AddRoute(Halifax, Montreal);//Halifax->Montreal
AirCanadaMap.AddRoute(Gander, StJohns);//Gander->StJohns
AirCanadaMap.AddRoute(StJohns, Gander);//StJohns->Gander
AirCanadaMap.AddRoute(StJohns, Halifax);//StJohns->Halifax
AirCanadaMap.AddRoute(Gander, Halifax);//Gander->Halifax
AirCanadaMap.AddRoute(Halifax, Toronto);//Halifax->Toronto
AirCanadaMap.AddRoute(Fredericton, Quebec);//Halifax->Toronto
AirCanadaMap.AddRoute(Quebec,Fredericton); //Quebec->Fredericton
AirCanadaMap.AddRoute(Quebec,Ottawa); //Quebec->Ottawa
AirCanadaMap.AddRoute(Ottawa,Quebec); //Ottawa->Quebec
AirCanadaMap.AddRoute(Montreal,Ottawa);//Montreal->Ottawa
AirCanadaMap.AddRoute(Ottawa,Toronto); //Ottawa->Toronto
AirCanadaMap.AddRoute(Toronto,Ottawa); //Toronto->Ottawa

///Test3: Adding a duplicate route 
Console.WriteLine("\nTest3: Adding a duplicate route ");
AirCanadaMap.AddRoute(Toronto,Vancouver);

///Test4: Find fastest route between airports that are connected
Console.WriteLine("\nTest4: Find fastest route between airports that are connected");
AirCanadaMap.FastestRoute(Fredericton, Ottawa);
AirCanadaMap.FastestRoute(Toronto, Vancouver);
AirCanadaMap.FastestRoute(Vancouver, Fredericton);
AirCanadaMap.FastestRoute(StJohns, Gander);

///Test5: Find fastest route between airports that are not connected
Console.WriteLine("\nTest5: Find fastest route between airports that are not connected");
AirCanadaMap.FastestRoute(Moncton, Gander);

///Test6: Remove an airport that exists in the map
Console.WriteLine("\nTest6: Remove an airport that exists in the map");
AirCanadaMap.RemoveAirport(Quebec);

///Test7: Remove an airport that does not exist in the map
Console.WriteLine("\nTest7: Remove an airport that does not exist in the map");
AirCanadaMap.RemoveAirport(Quebec);

///Test8: Find fastest route for an airport that does not exist (deleted airport)
Console.WriteLine("\nTest8: Find fastest route for an airport that does not exist (deleted airport)");
AirCanadaMap.FastestRoute(Quebec, Winnipeg);

///Test9: Adding a route to a destination that doesn't exist
Console.WriteLine("\nTest9: Adding a route to a destination that doesn't exist");
AirCanadaMap.AddRoute(Vancouver, Quebec);

///Test10: Removing a route from the map from airports that does exist
Console.WriteLine("\nTest10: Removing a route from the map from airports that does exist");
AirCanadaMap.RemoveRoute(Calgary,Edmonton);

///Test11: Removing a route from the map from airports that doesn't exist
Console.WriteLine("\nTest11: Removing a route from the map from airports that doesn't exist");
AirCanadaMap.RemoveRoute(Quebec, Edmonton);

///Test12: Removing all airports
Console.WriteLine("\nTest12: Removing all airports");
AirCanadaMap.RemoveAirport(Calgary);
AirCanadaMap.RemoveAirport(Edmonton);
AirCanadaMap.RemoveAirport(Fredericton);
AirCanadaMap.RemoveAirport(Gander);
AirCanadaMap.RemoveAirport(Halifax);
AirCanadaMap.RemoveAirport(Moncton);
AirCanadaMap.RemoveAirport(Montreal);
AirCanadaMap.RemoveAirport(Ottawa);
AirCanadaMap.RemoveAirport(StJohns);
AirCanadaMap.RemoveAirport(Toronto);
AirCanadaMap.RemoveAirport(Vancouver);
AirCanadaMap.RemoveAirport(Winnipeg);

///Test13: Finding fastest route between airports when the map is empty
Console.WriteLine("\nTest13: Finding fastest route between airports when the map is empty");
AirCanadaMap.FastestRoute(Toronto,Gander);