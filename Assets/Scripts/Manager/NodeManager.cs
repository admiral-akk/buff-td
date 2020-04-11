using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    void Start()
    {
        IDictionary<int, IDictionary<int, Node>> gameBoard = new Dictionary<int, IDictionary<int, Node>>();
        Node[] nodes = GameObject.FindObjectsOfType<Node>();
        foreach (Node node in nodes)
        {
            int x = xIndex(node);
            int z = zIndex(node);
            if (!gameBoard.ContainsKey(x))
            {
                gameBoard.Add(x, new Dictionary<int, Node>());
            }
            gameBoard[x][z] = node;
        }

        foreach (Node node in nodes)
        {
            int x = xIndex(node);
            int z = zIndex(node);
            node.SetAdjacentNodes(GetAdjacentNodes(gameBoard, node, x, z));
        }
    }

    int xIndex(Node node)
    {
        return Mathf.RoundToInt(node.transform.position.x / 1.5f);
    }

    int zIndex(Node node)
    {
        return Mathf.RoundToInt(node.transform.position.z / 1.5f);
    }

    private IList<Node> GetAdjacentNodes(IDictionary<int, IDictionary<int, Node>> gameBoard, Node node, int x, int z)
    {
        List<Node> nodes = new List<Node>();
        nodes.Add(GetNodeIfExists(gameBoard, x + 1, z));
        nodes.Add(GetNodeIfExists(gameBoard, x - 1, z));
        nodes.Add(GetNodeIfExists(gameBoard, x, z + 1));
        nodes.Add(GetNodeIfExists(gameBoard, x, z - 1));
        nodes.RemoveAll(n => n == null);
        return nodes;
    }

    private Node GetNodeIfExists(IDictionary<int, IDictionary<int, Node>> gameBoard, int x, int z)
    {
        if (gameBoard.ContainsKey(x) && gameBoard[x].ContainsKey(z))
        {
            return gameBoard[x][z];
        }
        return null;
    }
}
