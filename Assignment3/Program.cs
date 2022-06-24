/* COIS 3020H - Assignment 3 : PART A - Ternary Trees
 * Group Members: Abhinav Manocha, Sarah Roy, Tawab Wahab, Audrey Uwa.
 * Sources: https://www.geeksforgeeks.org/ternary-search-tree-deletion/
 * Program.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment3;
class Program
{
        static void Main(string[] args)
        {
            Trie<int> T = new Trie<int>();

            T.Insert("bag", 10);
            T.Insert("bat", 20);
            T.Insert("cab", 70);
            T.Insert("bagel", 30);
            T.Insert("beet", 40);
            T.Insert("abc", 60);

            T.Print();

            Console.WriteLine("Size: "+T.Size());

            Console.WriteLine(T.Value("abc"));
            Console.WriteLine(T.Value("beet"));
            Console.WriteLine(T.Value("a"));

            Console.WriteLine(T.Contains("baet"));
            Console.WriteLine(T.Contains("beet"));
            Console.WriteLine(T.Contains("abc"));

            T.PrintTT();

            Console.WriteLine("Remove beet");
            T.Remove("beet");

            Console.WriteLine("Size: " + T.Size());
            Console.WriteLine("Remove abc");
            T.Remove("abc");
            Console.WriteLine("Remove abc after its already removed");
            T.Remove("abc");

            Console.WriteLine("Size: " + T.Size());
        int i;
        Random r = new Random();

        BinomialHeap<PriorityClass> BH = new BinomialHeap<PriorityClass>();

        for (i = 0; i < 20; i++)
        {
            BH.Add(new PriorityClass(r.Next(50), (char)('a')));
        }
        //First output
        Console.WriteLine(BH.Size());
        //second output (for each tree what is the degree)

        while (!BH.Empty())
        {
            Console.WriteLine(BH.Front().ToString());
            BH.Remove();
            Console.ReadLine();
        }
        BH.Print();
        Console.ReadLine();
        Console.ReadKey();
        }
}