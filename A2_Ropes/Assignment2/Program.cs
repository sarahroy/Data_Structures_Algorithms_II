/* 
 * Group Members: Abhinav Manocha, Sarah Roy, Tawab Wahab, Audrey Uwa.
 * Sources: 
 * Program.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment_2;


string s1 = "Thisis"; //str has 7 chars
string s2 = "Ropes";
Rope rope = new Rope(s1);
Rope r = new Rope(s2);
//Testing Insert
Console.WriteLine("Testing Insert");
rope.Insert("our", rope.Length() - 1);
rope.Insert("second", rope.Length() - 1);
rope.Insert("assignment", rope.Length() - 1);
rope.Insert("", 4);

//Testing print
Console.WriteLine("\nPrinting rope:");
rope.PrintRope();
Console.WriteLine();

//Testing delete
Console.WriteLine("Testing Delete");
rope.Delete(0, 5);
rope.Delete(31, 38);
Console.WriteLine();
rope.PrintRope();

//Testing reverse
Console.WriteLine("Testing Reverse");
rope.Reverse();
Console.WriteLine();
rope.PrintRope();

//Testing CharAt
Console.WriteLine("Testing CharAt");
rope.CharAt(0);//first char 
rope.CharAt(-1); //out of bounds
rope.CharAt(rope.Length() - 1); //last char
Console.WriteLine();

//Testing IndexOf
Console.WriteLine("Testing IndexOf");
rope.IndexOf('g');
rope.IndexOf('z'); //char doesn't exist in our rope
Console.WriteLine();

//Testing substring
Console.WriteLine("Testing Substring");
rope.Substring(0, 10); //first 10 chars

//Testing Concatenate
Console.WriteLine("Testing Concatenate");
rope.Concatenate(rope, r);
rope.PrintRope();

//Testing Insert
Console.WriteLine("Testing Insert");
rope.Insert(s2, 5);
rope.PrintRope();

//Testing Delete
Console.WriteLine("Testing Delete");
rope.Insert(s2, 5);
rope.PrintRope();

//Testing Combining left and right siblings with length <=5 
Console.WriteLine("Testing Combining left and right siblings with length <=5 ");
rope.CombineLR();