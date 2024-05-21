using System;
using System.Collections.Generic;

namespace Coding;

/// <summary>
/// https://leetcode.com/problems/insert-delete-getrandom-o1
/// </summary>
public class RandomizedSet
{
    private readonly List<int> _list;
    private readonly Dictionary<int, int> _valuePositions;
    private readonly Random _random;

    /// <summary>
    /// ["RandomizedSet","insert","insert","remove","insert","remove","getRandom"]
    /// [[],[0],[1],[0],[2],[1],[]]
    /// </summary>
    public RandomizedSet()
    {
        _list = new List<int>(); // 1
        _valuePositions = new Dictionary<int, int>(); // [1,0], [2,1]
        _random = new Random();
    }

    public bool Insert(int val)
    {
        if (_valuePositions.ContainsKey(val))
        {
            return false;
        }

        _list.Add(val);
        _valuePositions[val] = _list.Count - 1;

        return true;
    }

    public bool Remove(int val) // 0, 2
    {
        if (!_valuePositions.ContainsKey(val))
        {
            return false;
        }

        var targetPosition = _valuePositions[val]; // 0, 1
        _list[targetPosition] = _list[^1];
        _valuePositions[_list[targetPosition]] = targetPosition;

        _list.RemoveAt(_list.Count - 1);
        _valuePositions.Remove(val);

        return true;
    }

    public int GetRandom()
    {
        return _list[_random.Next(0, _list.Count)];
    }
}

/**
 * Your RandomizedSet object will be instantiated and called as such:
 * RandomizedSet obj = new RandomizedSet();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */
