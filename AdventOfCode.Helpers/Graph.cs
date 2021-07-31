using System;
using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public class Graph<T>
    {
        private readonly List<GraphNode<T>> nodes = new();
        public List<GraphNode<T>> Nodes => this.nodes;

        public void Add(GraphNode<T> node)
        {
            node.Graph = this;
            this.nodes.Add(node);
        }

        public void Remove(GraphNode<T> node)
        {
            foreach (var edge in node.Edges)
            {
                edge.Remove(node);
            }
            this.nodes.Remove(node);
            node.Graph = null;
        }
    }

    public class GraphNode<T>
    {
        private readonly HashSet<GraphNode<T>> edges = new();

        public Graph<T>? Graph { get; set; }
        public T Value { get; set; }
        public IEnumerable<GraphNode<T>> Edges => this.edges;

        public GraphNode(T value)
        {
            Value = value;
        }

        public void Add(GraphNode<T> edge)
        {
            if (this.Graph is null)
                throw new InvalidOperationException("Node does not belong to a graph.");
                
            this.Graph.Add(edge);
            this.edges.Add(edge);
            edge.edges.Add(this);
        }

        public void Remove(GraphNode<T> edge)
        {
            this.edges.Remove(edge);
            edge.edges.Remove(this);
        }

        public static implicit operator GraphNode<T>(T value) => new(value);

        public override string ToString() => this.Value?.ToString() ?? "null";
    }
}