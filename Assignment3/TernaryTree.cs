/* COIS 3020H - Assignment 3 : PART A - Ternary Trees
 * Group Members: Abhinav Manocha, Sarah Roy, Tawab Wahab, Audrey Uwa.
 * Sources: https://www.geeksforgeeks.org/ternary-search-tree-deletion/
 * TernaryTree.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public interface IContainer<T>
    {
        void MakeEmpty();
        bool Empty();
        int Size();
    }

    //-------------------------------------------------------------------------

    public interface ITrie<T> : IContainer<T>
    {
        bool Insert(string key, T value);
        T Value(string key);
    }

    //-------------------------------------------------------------------------

    class Trie<T> : ITrie<T>
    {
        private Node root;                 // Root node of the Trie
        private int size;                  // Number of values in the Trie

        class Node
        {
            public char ch;                // Character of the key
            public T value;                // Value at Node; otherwise default
            public Node low, middle, high; // Left, middle, and right subtrees
            public int isleaf;

            // Node
            // Creates an empty Node
            // All children are set to null
            // Time complexity:  O(1)

            public Node(char ch)
            {
                this.ch = ch;
                value = default(T);
                low = middle = high = null;
                isleaf = 0;
            }
        }

        // Trie
        // Creates an empty Trie
        // Time complexity:  O(1)

        public Trie()
        {
            MakeEmpty();
            size = 0;
        }

        // Public Insert
        // Calls the private Insert which carries out the actual insertion
        // Returns true if successful; false otherwise

        public bool Insert(string key, T value)
        {
            return Insert(ref root, key, 0, value);
        }

        // Private Insert
        // Inserts the key/value pair into the Trie
        // Returns true if the insertion was successful; false otherwise
        // Note: Duplicate keys are ignored
        // Time complexity:  O(n+L) where n is the number of nodes and 
        //                                L is the length of the given key

        private bool Insert(ref Node p, string key, int i, T value)
        {
            if (p == null)
                p = new Node(key[i]);

            // Current character of key inserted in left subtree
            if (key[i] < p.ch)
                return Insert(ref p.low, key, i, value);

            // Current character of key inserted in right subtree
            else if (key[i] > p.ch)
                return Insert(ref p.high, key, i, value);

            else if (i + 1 == key.Length)
            // Key found
            {
                // But key/value pair already exists
                if (!p.value.Equals(default(T))) //this means there is already a value at that key, and that is NOT allowed.
                    return false;
                else
                {
                    // Place value in node
                    p.value = value;
                    size++;
                    return true;
                }
            }

            else
                // Next character of key inserted in middle subtree
                return Insert(ref p.middle, key, i + 1, value);
        }

        // Value
        // Returns the value associated with a key; otherwise default
        // Time complexity:  O(d) where d is the depth of the trie

        public T Value(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree           
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return the value if all characters of the key have been visited 
                    if (++i == key.Length)
                        return p.value;

                    // Move to next character of the key in the middle subtree   
                    p = p.middle;
                }
            }
            return default(T);   // Key too long
        }

        // Contains
        // Returns true if the given key is found in the Trie; false otherwise
        // Time complexity:  O(d) where d is the depth of the trie

        public bool Contains(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree           
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return true if the key is associated with a non-default value; false otherwise 
                    if (++i == key.Length)
                        return !p.value.Equals(default(T));

                    // Move to next character of the key in the middle subtree   
                    p = p.middle;
                }
            }
            return false;        // Key too long
        }

        //to check if current node is leaf node or not
        bool IsLeaf(Node root)
        {
            if (root.isleaf == 1)
                return true;
            return false;
        }

        //to check if current node has any child
        //node or not
        bool IsFreeNode(Node root)
        {
            if (root.low != null || root.middle != null || root.high != null)
                return false;
            return true;
        }
        // Public Remove
        // Calls the private Remove which carries out the actual removal
        // Returns true if successful; false otherwise
        public bool Remove(string key)
        {
            if (!Contains(key))
            {
                return false;
            }
            else
            {
                return Remove(ref root, key, 0);
            }
        }
        // Remove
        // Remove the given key (value) from the Trie
        // Returns true if the removal was successful; false otherwise
        private bool Remove(ref Node p, string key, int i)
        {
            //node p not found
            if (p == null)
                return false;

            // CASE 4 Key present in TST, having atleast
            // one other key as prefix key.
            if (key[i] == null)
            {
                // Unmark leaf node if present
                if (IsLeaf(p))
                {
                    p.isleaf = 0;
                    return IsFreeNode(p);
                }

                // else string is not present in TST and
                // return 0
                else
                    return false;
            }

            // CASE 3 Key is prefix key of another long
            // key in TST.
            if (key[i] < p.ch)
                return Remove(ref p.low, key, i);
            if (key[i] > p.ch)
                return Remove(ref p.high, key, i);

            // CASE 1 Key may not be there in TST.
            if (key[i] == p.ch)
            {
                // CASE 2 Key present as unique key
                if (Remove(ref p.middle, key, i + 1))
                {
                    // delete the last node, neither it has
                    // any child nor it is part of any other
                    // string
                    p.middle = null;
                    return !IsLeaf(p) && IsFreeNode(p);
                }
            }
            --size;
            return false;
        }

        // MakeEmpty
        // Creates an empty Trie
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            root = null;
        }

        // Empty
        // Returns true if the Trie is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return root == null;
        }

        // Size
        // Returns the number of Trie values
        // Time complexity:  O(1)

        public int Size()
        {
            return size;
        }

        // Public Print
        // Calls private Print to carry out the actual printing

        public void Print()
        {
            Print(root, "");
        }

        // Private Print
        // Outputs the key/value pairs ordered by keys
        // Time complexity:  O(n) where n is the number of nodes

        private void Print(Node p, string key)
        {
            if (p != null)
            {
                Print(p.low, key);
                if (!p.value.Equals(default(T)))
                    Console.WriteLine(key + p.ch + " " + p.value);
                Print(p.middle, key + p.ch);
                Print(p.high, key);
            }
        }

        //Print Ternary Tree (TT)
        //Calls the private method to print out the tree if the tree is not empty
        public void PrintTT()
        {
            if (root != null)
                PrintTT(root);
            else
                Console.WriteLine("Empty Tree");
        }
        
        //Print Ternary Tree(TT)
        //Time Complexity: O(n) where n is the number of nodes
        private void PrintTT(Node p)
        {
            if (p != null) 
            {
                if (p.low != null)
                    Console.Write(p.low.ch + " " + p.low.value + "::"); //print the char and value of left subtree
                else 
                    Console.Write("\t");
                
                PrintTT(p.low); //recursively print the left subtree
                
                if (p.middle != null)
                    Console.Write(p.middle.ch + " " + p.middle.value + "::"); //print the char and value of middle subtree
                else 
                    Console.Write("\t");
                
                PrintTT(p.middle); //recursively print the middle subtree
                
                if (p.high != null)
                    Console.Write(p.high.ch + " " + p.high.value + "::"); //print the char and value of right subtree
                else 
                    Console.Write("\t");
                
                PrintTT(p.high); //recursively print the right subtree
                
                Console.WriteLine();
            }
        }       
    }
}