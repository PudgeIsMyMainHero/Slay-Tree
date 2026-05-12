namespace Slay_Tree
{
    public class Node
    {
        public int Key;
        public Node Left, Right, Parent;
        public Node(int key) => Key = key;
    }
}