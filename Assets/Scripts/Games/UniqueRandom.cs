
using System;
using System.Collections.Generic;

public static class UniqueRandom
{
    private static int _prevIndex;
    public static T GetNewRandom<T>(T[] arr)
    {
        var newIndex = GetRandom(0, arr.Length);

        while (newIndex == _prevIndex)
            newIndex = GetRandom(0, arr.Length);
        
        _prevIndex = newIndex;
        
        return arr[newIndex];
    }

    private static int GetRandom(int min, int max)
    {
        var rand = new Random();
        return rand.Next(min, max);
    }
    
    // Sequence Test
    public static Stack<T> GenerateSequence<T>(T[] arr, int length)
    {
        Stack<T> members = new Stack<T>();

        for (int i = 0; i < length; i++)
        {
            members.Push(GetNewRandom(arr));
        }
        
        return members;
    }
}