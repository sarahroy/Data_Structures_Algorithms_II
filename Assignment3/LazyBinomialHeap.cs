using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class BinomialNode<T>
    {
        public T Item { get; set; } //Public item of type T
        public BinomialNode<T> LeftMostChild { get; set; } //Left most child
        public BinomialNode<T> RightSibling { get; set; } //Right most child

        // Constructor for BinomialNode 
        public BinomialNode(T item) //Accepts item T as item
        {
            Item = item; //Item
            LeftMostChild = null; //Left most child set to null
            RightSibling = null;  //Right most child set to null

        }
    }

    //--------------------------------------------------------------------------------------

    public interface IBinomialHeap<T> : IContainer<T> where T : IComparable
    {
        void Add(T item); // Add an item to a binomial heap
        void Remove();  // Remove the item with the highest priority
        T Front();  // Return the item with the highest priority
        void Coalesce(); //Merge Binomial list
    }

    //--------------------------------------------------------------------------------------

    // Binomial Heap
    // Where the magic happens

    public class BinomialHeap<T> : IBinomialHeap<T> where T : IComparable
    {
        private BinomialNode<T> head;  // Head of the root list
        private List<BinomialNode<T>>[] rootListArr;  // Head of the root list
        private BinomialNode<T> highest; //node with highest priority
        private int size;              // Size of the binomial heap
        private BinomialNode<T>[] B; //array of binomial trees 

        // Contructor
        // Time complexity:  O(1)
        public BinomialHeap()
        {
           
            B = new BinomialNode<T>[1]; //Setting a var B as new Binomial Node
            rootListArr[0] = new List<BinomialNode<T>>(); //Creating a new list of binomial Nodes of type T
            size = 0; //Initializing size to 0  
            highest = null; //Setting highest to null
        }


        // Add
        // Inserts an item into the binomial heap
        // Time complexity:  O(log n)

        public void Add(T item)
        {
            BinomialNode<T> node = new BinomialNode<T>(item);

            // setting the new item in array[0] of binomial tree (size zero) 
            // new items get added at that spot.

            node.RightSibling = B[0].RightSibling;
            B[0].RightSibling = node;
            // This if statement will passhonly when an item is first added.
            // //if (highest == null)                    
            // When the first item is inserted, the greatest value will be null.
                highest = B[0].RightSibling;       // The highest item in Heap at B[0] will then become the first item in Heap.


            // If the newly added item has a higher priority than the current highest, the highest should be updated.
            if (highest != null && highest.Item.CompareTo(B[0].RightSibling) == 0)
                size++;
            //Any new items are subsequently added to the root list at B[0].                highest = B[0].RightSibling;                           
            // any new items are then added to the front of the root list at B[0]
            // increase size

        }

        // FindHighest
        // Returns the preceding reference to the highest priority item
        // Assumption:  Root list contains at least one binomial tree
        // Time complexity:  O(log n)

        private BinomialNode<T> FindHighest()
        {
            BinomialNode<T> p = head, q = head;
            T highest;

            // Set highest to the first item
            highest = p.RightSibling.Item;

            // Traverse the root list
            // Find the root/item with the highest priority
            while (p.RightSibling.RightSibling != null)
            {
                p = p.RightSibling;
                if (p.RightSibling.Item.CompareTo(highest) > 0)
                {
               //Assign the 'highest' value to the first item..Item;
                    q = p;
                }
            }
            return q;//Determine which root/item has the highest priority.
        }
        
        // Remove
        // Removes the item with the highest priority from the binomial heap
        // Time complexity:  O(log n)
        public void Remove()
        {
            // if the heap is empty, return
            if (size == 0)
                return;

            // if the heap is not empty, remove the item with the highest priority
            // by combining the root list of the heap with the root list of the children of the removed item
            // and then calling coalesce to combine all trees of the same size in appropriate buckets

            BinomialNode<T> temp = B[0];
            BinomialNode<T> temp2 = B[0].RightSibling;

            // if the heap has only one item, then remove it
            if (size == 1)
            {
                B[0] = new BinomialNode<T>(default(T));
                size--;
                return;
            }

            // if the heap has more than one item, then remove the item with the highest priority
            // by combining the root list of the heap with the root list of the children of the removed item
            // and then calling coalesce to combine all trees of the same size in appropriate buckets

            while (temp2 != null)
            {
                temp.RightSibling = temp2.RightSibling;
                temp = temp.RightSibling;
                temp2 = temp2.RightSibling;
            }

            // call coalesce to combine all trees of the same size in appropriate buckets
            Coalesce();

            // update the size of the heap
            size--;

        }


        // Front
        // Returns the item with the highest priority
        // Time complexity:  O(log n)

        public T Front()
        {
            if (Empty())    
                throw new Exception("Heap is empty");
            else
                return highest.Item;
        }
        // BinomialLink
        // Makes child the leftmost child of root
        // Time complexity:  O(1)

        private void BinomialLink(BinomialNode<T> child, BinomialNode<T> root)
        {
            child.RightSibling = root.LeftMostChild;
            root.LeftMostChild = child;
        }


        // MakeEmpty
        // Creates an empty binomial heap
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            head.RightSibling = null;
            size = 0;
        }

        // Empty
        // Returns true is the binomial heap is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return size == 0;
        }

        // Size
        // Returns the number of items in the binomial heap
        // Time complexity:  O(1)

        public int Size()
        {
            return size;
        }

        //Coalesce method
        //Time complexity:  O(log n)
        public void Coalesce()
        {
            List<BinomialNode<T>>[] newRootListArr = new List<BinomialNode<T>>[rootListArr.Length]; //New list of binomial Nodes
            
            for (int i = 0; i < rootListArr.Length; i++) //For loop
            {
                newRootListArr[i] = new List<BinomialNode<T>>();
            }

            for (int i = 0; i < rootListArr.Length; i++)
            {
                while (rootListArr[i].Count > 0)
                {
                    BinomialNode<T> current = rootListArr[i][0];
                    rootListArr[i].RemoveAt(0);

                    // Find the correct position in the new binomial heap
                    int j = i;
                    while (newRootListArr[j].Count > 0 && current.Item.CompareTo(newRootListArr[j][0].Item) > 0)
                    {
                        j++;
                    }

                    newRootListArr[j].Add(current);
                }
            }

            rootListArr = newRootListArr;
        }
        //An attempt at printing the code 

        //implementing a private print method to print the binomial tree 
        public void Print()
        {
            this.Print(head,0);
        }
        private void Print(BinomialNode<T> p, int level)
        {
            if (p == null)
                return;

            Print(p.RightSibling, level + 1);

            for (int i = 0; i < level; i++)
                Console.Write("   ");

            Console.WriteLine(p.Item);

        }
    }

    //--------------------------------------------------------------------------------------

    // Used by class BinomailHeap<T>
    // Implements IComparable and overrides ToString (from Object)

    public class PriorityClass : IComparable
    {
        private int priorityValue;
        private char letter;

        public PriorityClass(int priority, char letter)
        {
            this.letter = letter;
            priorityValue = priority;
        }

        public int CompareTo(Object obj)
        {
            PriorityClass other = (PriorityClass)obj;   // Explicit cast
            return priorityValue - other.priorityValue;  // High values have higher priority
        }

        public override string ToString()
        {
            return letter.ToString() + " with priority " + priorityValue;
        }
    }
    
}