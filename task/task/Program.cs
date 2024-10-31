using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Task2
{
    class Program
    {
        static void Main()
        {
            LinkedList linkedList = new LinkedList ();
            linkedList.addNode(1);
            linkedList.addNode(2);

            if (linkedList.isPalindrome())
            {
                linkedList.print();
                Console.WriteLine(" IS a Palindrome");
            }
            else
            {
                linkedList.print();
                Console.WriteLine(" IS NOT a Palindrome");
            }

        }
    }

    public class Node 
    {
        public Node (int data)
        {
            Data = data;
        }
        public int Data { get; set; }
        public Node Next { get; set; }
    }

    public class LinkedList
    {
        Node head;

        public void addNode(int data)
        {
            Node node = new Node(data);

            if (head == null)
            {
                head = node;
            }
            else
            {
                Node current = head;
                // ищем последний элемент
                while (current.Next != null)
                {
                    current = current.Next;
                }
                //устанавливаем последний элемент
                current.Next = node;
            }
        }

        public bool isPalindrome()
        {
            int i = 0;
            Node current = head;
            Node temp = head;

            while (temp != null)
            {
                temp = temp.Next;
                i++;
            }

            int[] arr1 = new int[i];
            int count = i;

            for (int j = 0; j < count; j++)
            {
                arr1[j] = current.Data;
                current = current.Next;
            }

            int[] arr2 = new int[count];
            int y = 0;

            for (int x = i - 1; x >= 0; x--)
            {
                arr2[y] = arr1[x];
                y++;
            }

            for (int x = 0; x < count; x++)
            {
                if (arr1[x] != arr2[x])
                {
                    return false;
                }
            }
            return true;
        }

        public void print ()
        {
            Node current = head;
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
        }

    }
}

