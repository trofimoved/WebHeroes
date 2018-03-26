using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Code;

namespace WebHeroes.Playground
{
    public class PriorityQueue<T>
    {
        // В этом примере я использую несортированный массив, но в идеале
        // это должна быть двоичная куча. Существует открытый запрос на добавление
        // двоичной кучи к стандартной библиотеке C#: https://github.com/dotnet/corefx/issues/574
        //
        // Но пока её там нет, можно использовать класс двоичной кучи:
        // * https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
        // * http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
        // * http://xfleury.github.io/graphsearch.html
        // * http://stackoverflow.com/questions/102398/priority-queue-in-net

        private List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

        public int Count
        {
            get { return elements.Count; }
        }

        public void Enqueue(T item, double priority)
        {
            elements.Add(Tuple.Create(item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Item2 < elements[bestIndex].Item2)
                {
                    bestIndex = i;
                }
            }

            T bestItem = elements[bestIndex].Item1;
            elements.RemoveAt(bestIndex);
            return bestItem;
        }
    }

    public class PathFinding
    {
        public Dictionary<Position, Position> cameFrom
            = new Dictionary<Position, Position>();
        public Dictionary<Position, double> costSoFar
            = new Dictionary<Position, double>();

        public BaseTerrain[] AStarSearch(Board board, Position startPosition, Position endPosition)
        {
            var frontier = new PriorityQueue<Position>();
            frontier.Enqueue(startPosition, 0);

            cameFrom[startPosition] = startPosition;
            costSoFar[startPosition] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.x == endPosition.x && current.y == endPosition.y)
                {
                    break;
                }

                foreach (var next in board.Neighbors(current))
                {
                    double newCost = costSoFar[current]
                        + board.Cost(current, next);
                    if (costSoFar.FirstOrDefault(x => x.Key.x == next.x && x.Key.y == next.y).Key == null
                        || newCost < costSoFar.FirstOrDefault(x => x.Key.x == next.x && x.Key.y == next.y).Value)
                    {
                        costSoFar[next] = newCost;
                        double priority = newCost + Position.Distance(next, endPosition);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }

            return null;
        }
    }
}