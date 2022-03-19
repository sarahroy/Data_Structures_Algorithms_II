/* COIS 3020H - Assignment 1
 * Group Members: Abhinav Manocha, Sarah Roy, Tawab Wahab, Audrey Uwa.
 * Sources: https://www.geeksforgeeks.org/shortest-path-unweighted-graph/
 * Graph.cs
 * Comments from Marker: Great Work! Sarah. Your Fastest Route Method implementation is Impressive. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    class AirportNode
    {
        public string Name { get; set; } //property for name field.
        public string Code { get; set; } //property for code field.
        public List<AirportNode> Destinations { get; set; } //property for list of destinations.
        public bool Visited { get; set; }   //used for BFS 
        public int Distance { get; set; } //used for BFS to store distance from origin airport

        /// <summary>
        /// Constructor for class AirportNode. 5%
        /// </summary>
        /// <param name="name">Name of the new airport</param>
        /// <param name="code">Code of the new airport</param>
        public AirportNode(string name, string code)
        {
            this.Name = name;
            this.Code = code;
            this.Visited = false; //set Visited flag to false initially
            this.Distance = -1; //Distances are set to -1 initially
            Destinations = new List<AirportNode>(); //list of Destinations linked to a node
        }

        /// <summary>
        /// Method to add destination airport. 5%
        /// </summary>
        /// <param name="destAirport">Destination airport</param>
        /// <returns></returns>
        public bool AddDestination(AirportNode destAirport)
        {
            if (!Destinations.Contains(destAirport))
            {
                Destinations.Add(destAirport);   //Adds the aiport into the list A
                Console.WriteLine(destAirport.Name+ " has been succesfully added");
                return true;
            }
            Console.WriteLine(destAirport.Name+ " already exist");
            return false;

        }

        /// <summary>
        /// Method to remove destination. 5%
        /// </summary>
        /// <param name="destAirport">Destination airport</param>
        /// <returns></returns>
        public bool RemoveDestination(AirportNode destAirport)
        {
            if (Destinations.Remove(destAirport))
            {
                Console.WriteLine("The destination " + destAirport.ToString() + " is removed.");
                return true;
            }
            Console.WriteLine("The destination does not exist");
            return false;
        }

        /// <summary>
        /// ToString method overload to print out airport name, code, and list of destinations. 5%
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return Name + " (Code: " + this.Code + ")";
        }
    }

    class RouteMap
    {
        private List<AirportNode> A; //List of airport nodes. 

        /// <summary>
        /// constructor for class RouteMap. 5%
        /// </summary>
        public RouteMap()
        {
            A = new List<AirportNode>();
        }


        /// <summary>
        /// Method to find airport by name. 5%
        /// </summary>
        /// <param name="name">Name of the airport</param>
        /// <returns></returns>
        public bool FindAirportName(string name)
        {
            int i;

            for (i = 0; i < A.Count; i++)
            {
                if (A[i].Name.Equals(name))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Method to find airport by code. 5%
        /// </summary>
        /// <param name="code">Airport code</param>
        /// <returns></returns>
        public bool FindAirportCode(string code)
        {

            int i;

            for (i = 0; i < A.Count; i++)
            {
                if (A[i].Code.Equals(code))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Method to add airport node. Duplicates not allowed. 5%
        /// </summary>
        /// <param name="a">Name of the airport</param>
        /// <returns></returns>
        public bool AddAirport(AirportNode a)
        {
            if (!FindAirportName(a.Name) || !FindAirportCode(a.Code))
            {
                A.Add(a);
                Console.WriteLine(a.Name+" has been succesfully added");
                return true;
            }
            else
            {
                Console.WriteLine(a.Name+" already exists");
                return false;
            }
        }


        /// <summary>
        /// Method to remove airport node. Node must exist. 5%
        /// </summary>
        /// <param name="a">Name of the airport</param>
        /// <returns></returns>
        public bool RemoveAirport(AirportNode a)
        {
            if (FindAirportName(a.Name))
            {
                A.Remove(a);
                Console.WriteLine(a.Name+ " has been succesfully removed");
                return true;
            }
            else
            {
                Console.WriteLine("The airport does not exist");
                return false;
            }
        }

        /// <summary>
        /// Method to add a route. 5%
        /// </summary>
        /// <param name="origin">origin airport</param>
        /// <param name="dest">destination airport</param>
        /// <returns></returns>
        public bool AddRoute(AirportNode origin, AirportNode dest)
        {
            if (FindAirportName(origin.Name) && FindAirportName(dest.Name))
                return origin.AddDestination(dest);

            else
                Console.WriteLine("Airport not found");
            return false;
        }


        /// <summary>
        /// Method to remove a route. 5%
        /// </summary>
        /// <param name="origin">origin airport</param>
        /// <param name="dest">destination airport</param>
        /// <returns></returns>
        public bool RemoveRoute(AirportNode origin, AirportNode dest)
        {
            if (FindAirportName(origin.Name))
                return origin.RemoveDestination(dest);

            Console.WriteLine("Airport not found");
            return false;

        }

        
        /// <summary>
        /// Method to find the shortest path using Breadth First Search between two given airports. 15%
        /// Source: https://www.geeksforgeeks.org/shortest-path-unweighted-graph/
        /// </summary> 
        /// <param name="origin">Origin Airport</param>
        /// <param name="Destination">Destination Airport</param>
        /// <returns></returns>
        public void FastestRoute(AirportNode origin, AirportNode Destination)
        {
            //Initially, all nodes in the map are marked as unvisited and all distances at -1
            foreach (var airport in A)
            {
                airport.Visited = false;
                airport.Distance = -1;
            }
            List<AirportNode> path = new List<AirportNode>(); //List to store airports in shortest path
            List<AirportNode> pred = new List<AirportNode>(); //List to store predecessor airports 

            //checking if the airports exist
            if (!FindAirportName(origin.Name) || !FindAirportName(Destination.Name))
                Console.WriteLine("Given airports does not exist");
            else //both airports exist
            {
                //Conduct BFS on our RouteMap to find the shortest path between the given airports
                if (!BFS(origin, Destination, pred))//BFS() returned false - path not found 
                {
                    Console.WriteLine(origin.Name + " and " + Destination.Name + " are not connected. \n");
                }

                else //BFS() returned true - path found
                {
                    AirportNode crawl = Destination;
                    path.Add(crawl); //adds destination airport to path
                    foreach (AirportNode node in pred) //for every airport in the predecessor list
                    {
                        if (node == Destination) //stopping condition
                        {
                            break;
                        }
                        else
                        {
                            while (node.Distance != -1) //while the airport dist from origin is greater than -1
                            {
                                path.Add(node); //add node to the path
                                crawl = node;
                                --node.Distance ;

                            }
                        }
                    }
                    
                    List<AirportNode> shortestPath = path.Distinct().ToList();// Making sure there are no duplicate airports in path
                    shortestPath.Add(origin); //add origin to the path

                    //Print path
                    Console.WriteLine("The shortest path from " + origin.Name + " to " + Destination.Name + " is:");
                    for (int i = shortestPath.Count - 1; i >= 0; i--)
                    {
                        Console.Write(shortestPath[i].Name + "(" + shortestPath[i].Code + ")  "); 
                    }
                    Console.WriteLine("\n\n");
                }
            }

        }

        /// <summary>
        /// Method that implements a modified version of BFS algorithm to find the shortest path. 
        /// Source: https://www.geeksforgeeks.org/shortest-path-unweighted-graph/
        /// </summary>
        /// <param name="src">Origin/Source Airport</param>
        /// <param name="target">Destination/Target Airport</param>
        /// <param name="dist">List of distances from the source/origin</param>
        /// <param name="pred">List to store predecessor nodes</param>
        /// <returns></returns>
        private bool BFS(AirportNode src, AirportNode target, List<AirportNode> pred)
        {
            Queue<AirportNode> Q = new Queue<AirportNode>(); //Queue to store nodes that are to be explored

            List<AirportNode> path = new List<AirportNode>(); //List to store airports in the shortest path

            src.Visited = true; //origin is first to be visited so change visited flag of origin airport to true
            src.Distance = 0;//distance from origin to itself should be 0
            Q.Enqueue(src); //Start BFS at the origin and add origin airport to the end of the Q

            while (Q.Any()) //while the Q is not empty
            {
                AirportNode currentAirport = Q.First(); //takes the first element in the Q
                Q.Dequeue(); //remove first element of the Q to explore connecting Destinations
                foreach (AirportNode airport in currentAirport.Destinations) //foreach loop goes through all connecting airports
                {
                    if (!airport.Visited) //if unexplored 
                    {
                        airport.Visited = true; //mark node as visited
                        airport.Distance = currentAirport.Distance + 1; //calculate distance for node from the origin airport
                        pred.Add(airport); //add node to predecessor list
                        Q.Enqueue(airport); //adds node to then end of the Q
                        
                        if (airport == target)// stopping condition (when we find our destination)
                        {
                            Console.WriteLine("Path found from "+src.Name+ " to "+target.Name + "\n");
                            return true; //path found 
                        }
                            
                    }
                }
            }
            return false; //path not found
        }

    }
}