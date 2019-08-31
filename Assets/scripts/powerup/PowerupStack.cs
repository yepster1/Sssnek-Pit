using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class PowerupStack
// {
    
//     static readonly int MAX = 2; 
//     int top; 
//     string[] stack = new string[MAX]; 

//     bool IsEmpty() 
//     { 
//         return (top < 0); 
//     } 
//     public Stack() 
//     { 
//         top = -1; 
//     } 
//    public bool Push(string data) 
//     { 
//         if (top >= MAX) 
//         { 
//             Console.WriteLine("Stack Overflow"); 
//             return false; 
//         } 
//         else
//         { 
//             stack[++top] = data; 
//             return true; 
//         } 
//     } 

//     public string Pop() 
//     { 
//         if (top < 0) 
//         { 
//             Console.WriteLine("Stack Underflow"); 
//             return 0; 
//         } 
//         else
//         { 
//             string value = stack[top--]; 
//             return value; 
//         } 
//     } 

//     public void Peek() 
//     { 
//         if (top < 0) 
//         { 
//             Console.WriteLine("Stack Underflow"); 
//             return; 
//         } 
//         else
//             Console.WriteLine("The topmost element of Stack is : {0}", stack[top]); 
//     } 

//     public void PrintStack() 
//     { 
//         if (top < 0) 
//         { 
//             Console.WriteLine("Stack Underflow"); 
//             return; 
//         } 
//         else
//         { 
//             Console.WriteLine("Items in the Stack are :"); 
//             for (int i = top; i >= 0; i--) 
//             { 
//                 Console.WriteLine(stack[i]); 
//             } 
//         } 
//     } 
  
// }
