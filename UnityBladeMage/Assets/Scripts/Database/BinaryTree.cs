using System;
using System.Collections;
using System.Globalization;

public class BinaryTree<T>
{
	private BinaryTreeNode<T> root;
	private int count;

	public BinaryTree()
	{
		root = null;
		count = 0;
	}

	public virtual void Clear()
	{
		root = null;
		count = 0;
	}

	public BinaryTreeNode<T> Root
	{
		get
		{
			return root;
		}
		set
		{
			root = value;
		}
	}

	public virtual void Add(T data)
	{
		Comparer comparer = new Comparer(new CultureInfo( 0x040A, false ));

		// create a new Node instance
		BinaryTreeNode<T> n = new BinaryTreeNode<T>(data);
		int result;
		
		// now, insert n into the tree
		// trace down the tree until we hit a NULL
		BinaryTreeNode<T> current = root, parent = null;
		while (current != null)
		{
			result = comparer.Compare(current.Value, data);
			if (result == 0)
				// they are equal - attempting to enter a duplicate - do nothing
				return;
			else if (result > 0)
			{
				// current.Value > data, must add n to current's left subtree
				parent = current;
				current = current.Left;
			}
			else if (result < 0)
			{
				// current.Value < data, must add n to current's right subtree
				parent = current;
				current = current.Right;
			}
		}
		
		// We're ready to add the node!
		count++;
		if (parent == null)
			// the tree was empty, make n the root
			root = n;
		else
		{
			result = comparer.Compare(parent.Value, data);
			if (result > 0)
				// parent.Value > data, therefore n must be added to the left subtree
				parent.Left = n;
			else
				// parent.Value < data, therefore n must be added to the right subtree
				parent.Right = n;
		}
	}
}
