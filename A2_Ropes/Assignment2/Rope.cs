/*
 * Group Members: Abhinav Manocha, Sarah Roy, Tawab Wahab, Audrey Uwa.
 * Sources: 
 * Rope.cs
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class Node
    {
        public string S { get; set; } //string S in the leaf node 
        public int length { get; set; } //number of characters in the node
        public Node left, right;//left and right pointer
        public Node() //creating an empty node
        {
            this.S = null;
            this.length = 0;
            left = right = null;
        }
        public Node(string data) //creating a node with string data
        {
            this.S = data;
            this.length = data.Length;
            left = right = null;
        }
    }
    class Rope
    {
        Node root; //root pointer
        public const int MAX_SIZE = 10; //maximum size of leaf substring
        public const int MIN_SIZE = 5; //minimum size

        /// <summary>
        /// Create an empty rope. 1 mark
        /// </summary>
        public Rope()
        {
            root = new Node(); //empty rope where string S is null
        }

        /// <summary>
        /// Create a balanced rope from a given string. 8 marks.
        /// </summary>
        /// <param name="S"></param>
        public Rope(string S)
        {
            root = new Node(S); //new rope made with string S
        }

        /// <summary>
        /// Return the concatenation of ropes R1 and R2. 4 marks.
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public Rope Concatenate(Rope R1, Rope R2)
        {
            Rope merged_rope = new Rope(); ; //make a new empty rope
            Node node;
            if (R1 == null)
            {
                merged_rope = R2; //if R1 is empty, then new rope = R2
                merged_rope.root.left = R2.root.left; //copying the left node 
                merged_rope.root.right = R2.root.right; //copying the right node
                merged_rope.root.length = R2.root.length; //length of new rope = length of R2
            }
            else if (R2 == null)
            {
                merged_rope = R1; //or if R2 is empty then return R1
                merged_rope.root.left = R1.root.left; //copying the left node
                merged_rope.root.right = R1.root.right; //copying the right node
                merged_rope.root.length = R1.root.length; //length of new rope = length of R1

            }
            else if (R1 != null && R2 != null)
            {
                merged_rope.root.left = R1.root.left; //copying the left node
                merged_rope.root.right = R2.root.right; //copying the right node
                merged_rope.root.length = R1.root.length + R2.root.length; //sum of both R1 & R2 would be the length of new rope
            }
            return merged_rope;
        }

        /// <summary>
        /// Return the two roples split at index i. 10 marks
        /// </summary>
        /// <param name="i"></param>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        public void Split(int i, Rope R1, Rope R2)
        {

            Node current = root;
            List<Node> route = new List<Node>();
            List<Node> children = new List<Node>();
            while (current.left != null && current.right != null)
            {
                if (i <= current.left.length) { current = current.left; }
                else
                {
                    i -= current.left.length;
                    current = current.right;
                }
                route.Add(current);
            }
            if (current.S != null)
            {
                if (current.S[i] != current.S.Max())
                {
                    Node Node1 = new Node(current.S.Substring(0, i));
                    Node Node2 = new Node(current.S.Substring(i));
                    current.left = Node1;
                    current.right = Node2;
                }
                int pathsize = route.Count;
                for (int j = 0; j < pathsize; ++j)
                {
                    current = route.Last();
                    current.length -= current.right.length;
                    children.Add(current.right);
                    current.right = null;
                    route.RemoveAt(route.Count - 1);
                }
            }

        }
        /// <summary>
        /// Method to traverse the rope.
        /// </summary>
        /// <param name="root"></param>
        private void Traverse(Node root)
        {
            if (root == null)
            {
                return;
            }
            Traverse(root.left); //recursively traverses the rope with left rope
            Traverse(root.right); //recursively traverses the rope with right rope
        }
        /// <summary>
        /// Insert S at index i. 6 marks.
        /// </summary>
        /// <param name="S"></param>
        /// <param name="i"></param>
        public void Insert(string S, int i)
        {
            Rope R; //create new rope
            if (root == null && S != null && i < this.root.length) //check if S in not null and i is within range
            {
                Split(i, this, this);//Split current rope at i
                R = new Rope(S);//create balanced rope with string S
                this.root.S += S;//prepend S to the current rope
                Concatenate(this, R);//Concatenate the current rope and R
                this.root = R.root;//set root to the root of R
                Concatenate(this, R);//Concatenate the (new) current rope and R
            }

            /////////// We Tried as per the slides shown in class :( /////////
            /* Rope R1 = new Rope(S);
            Rope R2 = new Rope();
            Rope R3 = new Rope();
            Split(i, R2, R3);
            Rope temp = new Rope();
            temp = Concatenate(R1, R2);
            temp = Concatenate(temp, R3);
            root = temp.root; */

            /*Split(i, this, this); //Split current rope at i 
            S.Prepend(CharAt(i));
            if (root == null && S!=null && i<root.length) //check if S in not null and i is within range
            {
                root.S.Prepend(S);
                root = new Node();
                root.S = S;
            }
            if (S != null)
                root.S[i] = S;
            else if (i < root.length)
            {
                root.left = Insert(root.left.S, i);
            }
            else
            {
                root.right = insert(root.right, v);
            }
            this.Split(i,);
            insert(this.root,S,i);*/


        }
        private Node insert(Node root, string s, int i)
        {
            if (root == null)
            {
                root = new Node();
                root.S = s;
            }
            else if (i < root.length)
            {
                root.left = insert(root.left, s, i);
            }
            else
            {
                root.right = insert(root.right, s, i);
            }
            return root;
        }
        /// <summary>
        /// Delete the substring S[i,j]. 6 marks.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void Delete(int i, int j)
        {
            if (root == null && i < this.root.length && j < this.root.length)
            {
                Rope R1 = new Rope();  //Create Rope 1
                Rope R2 = new Rope();  //Create Rope 2
                Rope R3 = new Rope();  //Create Rope 3
                Split(i - 1, R1, R2);  //Split at i-1 
                Split(j, R2, R3);       //Split at j
                Concatenate(R1, R3);   //Finally concatonate
            }
        }

        /// <summary>
        /// Return a substring S[i,j] (8 marks)
        /// </summary>
        /// Source: https://www.sanfoundry.com/java-program-implement-rope/
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public string Substring(int i, int j)
        {
            string str = "";
            bool found = false;
            Node node = root; //starting from the root

            if (j > node.length) //making sure j is not out
            {
                found = true;
                j -= node.length;

                if (i > node.length)
                {
                    i -= node.length;
                    str = node.right.S.Substring(i, j);
                    return str;
                }
                else
                    str = node.right.S.Substring(0, j);
            }

            if (!found)
            {
                while (j <= node.length)
                    node = node.left;
                j -= node.length;
                if (i >= node.length)
                {
                    i -= node.length;
                    str = node.right.S.Substring(i, j) + str;
                    return str;
                }

                str = node.right.S.Substring(0, j);
            }
            node = node.left;
            while (i < node.length)
            {
                str = node.right.S + str;
                node = node.left;
            }

            i -= node.length;
            str = node.right.S.Substring(i) + str;

            return str;
        }

        /// <summary>
        /// Return the character at index i. 4 marks.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>

        public char CharAt(int i)
        {
            Node node = root;
            if (node == null)
            {
                return ' ';
            }
            else
            {
                if (i > node.length)
                {
                    i -= node.length;
                    return node.right.S.ElementAt(i);
                }

                while (i < node.length)
                    node = node.left;

                i -= node.length;
                return node.right.S.ElementAt(i);
            }

        }


        /// <summary>
        /// Return the index of the first occurence of character c. 4 marks.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int IndexOf(char c)
        {
            return this.root.S.IndexOf(c); // this function return index of the character which is predifined in the libraries
        }

        /// <summary>
        /// Reverse the string represented by the current rope. 6 marks.
        /// Source: https://www.tutorialspoint.com/how-to-invert-a-binary-search-tree-using-recursion-in-chash
        /// </summary>
        public void Reverse()
        {
            reverse(this.root);
        }
        private Node reverse(Node node)
        {
            if (node == null)
            {
                return null;
            }
            Node left = reverse(node.left);
            Node right = reverse(node.right);
            node.left = right;
            node.right = left;
            return root;
        }
        /// <summary>
        /// Return the length of the string. 1 mark.
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            int l = 0;
            return len(this.root, l);
        }
        private int len(Node root, int l)
        {
            if (root == null) //check if root is not null
            {
                l += 0; //if root is null add 0 to the length
            }
            else
            {
                l += root.length; //update length
                                  //traverse the rope recursively
                len(root.right, l);
                len(root.left, l);
            }


            return l;
        }

        /// <summary>
        /// Return the string represented by the current rope. 4 marks. 
        /// </summary>
        /// <returns></returns>
        string ToString()
        {
            return this.root.left.S.ToString() + this.root.right.ToString();
        }

        /// <summary>
        /// Print the augmented binary tree of the current rope. 4 marks.
        /// </summary>
        public void PrintRope()
        {
            printRope(root);
            Console.WriteLine();
        }
        private void printRope(Node node)
        {
            if (node != null)
            {
                printRope(node.left);
                if (node.S != null)
                    Console.WriteLine(node.S);
                printRope(node.right);
            }
        }

        /// <summary>
        /// Optimization 1: Method to compress the route back to the root. 4 marks.
        /// </summary>
        void Compress()
        {

        }

        /// <summary>
        /// Optimization 2: Combine Left and Right siblings whose total string length is 5 or less. 4 marks.
        /// </summary>
        public void CombineLR()
        {
            combineLR(this.root);
        }
        private void combineLR(Node root)
        {
            Rope r;
            if (this.root == null)
            {
                return;
            }
            else
            {
                r = new Rope();
                if (root.left.length <= 5)
                {
                    r.root.left = root.left;
                }
                if (root.right.length <= 5)
                {
                    r.root.right = root.right;
                }
                combineLR(root.left);
                combineLR(root.right);
            }
            r.PrintRope();
        }
        /// <summary>
        /// Optimization 3: Rebalance rope periodically. 4 marks.
        /// </summary>
        void RebalanceRope()
        {

        }
    }
}
