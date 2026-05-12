namespace Slay_Tree
{
    public class SplayTree
    {
        public Node Root;
    public int Operations { get; private set; }

    private void RotateLeft(Node x)
    {
        Node y = x.Right;
        x.Right = y.Left;
        if (y.Left != null) y.Left.Parent = x;
        y.Parent = x.Parent;
        if (x.Parent == null) Root = y;
        else if (x == x.Parent.Left) x.Parent.Left = y;
        else x.Parent.Right = y;
        y.Left = x;
        x.Parent = y;
        Operations += 3; // обновления указателей
    }

    private void RotateRight(Node x)
    {
        Node y = x.Left;
        x.Left = y.Right;
        if (y.Right != null) y.Right.Parent = x;
        y.Parent = x.Parent;
        if (x.Parent == null) Root = y;
        else if (x == x.Parent.Right) x.Parent.Right = y;
        else x.Parent.Left = y;
        y.Right = x;
        x.Parent = y;
        Operations += 3;
    }

    private void Splay(Node x)
    {
        while (x.Parent != null)
        {
            Operations++; // проверка родителя
            if (x.Parent.Parent == null)
            {
                if (x == x.Parent.Left) RotateRight(x.Parent);
                else RotateLeft(x.Parent);
            }
            else if (x == x.Parent.Left && x.Parent == x.Parent.Parent.Left)
            {
                RotateRight(x.Parent.Parent);
                RotateRight(x.Parent);
            }
            else if (x == x.Parent.Right && x.Parent == x.Parent.Parent.Right)
            {
                RotateLeft(x.Parent.Parent);
                RotateLeft(x.Parent);
            }
            else if (x == x.Parent.Right && x.Parent == x.Parent.Parent.Left)
            {
                RotateLeft(x.Parent);
                RotateRight(x.Parent);
            }
            else
            {
                RotateRight(x.Parent);
                RotateLeft(x.Parent);
            }
        }
    }

    public int Insert(int key)
    {
        Operations = 0;
        if (Root == null) { Root = new Node(key); Operations = 1; return Operations; }

        Node curr = Root, parent = null;
        while (curr != null)
        {
            Operations++;
            parent = curr;
            if (key == curr.Key) { Splay(curr); return Operations; }
            curr = (key < curr.Key) ? curr.Left : curr.Right;
        }

        Node newNode = new Node(key);
        newNode.Parent = parent;
        if (key < parent.Key) parent.Left = newNode;
        else parent.Right = newNode;
        Splay(newNode);
        return Operations;
    }

    public bool Search(int key)
    {
        Operations = 0;
        Node curr = Root;
        Node lastVisited = null;
        while (curr != null)
        {
            Operations++;
            lastVisited = curr;
            if (key == curr.Key) break;
            curr = (key < curr.Key) ? curr.Left : curr.Right;
        }
        if (lastVisited != null) Splay(lastVisited);
        return curr != null && curr.Key == key;
    }

    public bool Delete(int key)
    {
        Operations = 0;
        Search(key); // поднимает key в корень, если он есть
        if (Root == null || Root.Key != key) return false;

        Node oldRoot = Root;
        if (oldRoot.Left == null)
        {
            Root = oldRoot.Right;
            if (Root != null) Root.Parent = null;
        }
        else
        {
            Node maxLeft = oldRoot.Left;
            while (maxLeft.Right != null)
            {
                Operations++;
                maxLeft = maxLeft.Right;
            }
            Operations++;
            Splay(maxLeft);
            Root.Right = oldRoot.Right;
            if (Root.Right != null) Root.Right.Parent = Root;
        }
        return true;
    }
    }
}