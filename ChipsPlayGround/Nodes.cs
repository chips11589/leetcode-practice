﻿using System;
using System.Collections.Generic;

namespace Coding;

public class ListNode(int val = 0, ListNode next = null)
{
    public int val = val;
    public ListNode next = next;
}

public class Node(int _val)
{
    public int val = _val;
    public Node next = null;
    public Node random = null;
    public IList<Node> neighbors = [];

    public Node left;
    public Node right;

    public Node(int _val, Node _left, Node _right, Node _next) : this(_val)
    {
        left = _left;
        right = _right;
        next = _next;
    }

    public Node(int _val, List<Node> _neighbors) : this(_val)
    {
        neighbors = _neighbors;
    }
}


public class TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
{
    public int val = val;
    public TreeNode left = left;
    public TreeNode right = right;
}