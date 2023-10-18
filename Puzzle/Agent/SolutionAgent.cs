using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Agent
{
    public class SolutionAgent
    {

        #region Private Members

        protected AgentPuzzleState _initialState { get; set; }

        protected AgentPuzzleState _goalState { get; set; }

        private Queue<AgentPuzzleState> _queue { get; set; }

        protected HashSet<(int, int, int, int, int, int, int, int, int)> _visited { get; set; }
        #endregion

        #region Constructr

        public SolutionAgent(AgentPuzzleState initialState, AgentPuzzleState goalState)
        {
            this._initialState = initialState;
            this._goalState = goalState;
        }
        #endregion

        #region Algorithms

        public (bool success, Stack<AgentPuzzleState> sequenceHisotry) ExecuteBreadthFirstSearch()
        {
            this._queue = new Queue<AgentPuzzleState>();
            this._visited = new HashSet<(int, int, int, int, int, int, int, int, int)>();

            this._queue.Enqueue(this._initialState);

            while(_queue.Count > 0)
            {
                var currentNode = _queue.Dequeue();

                if (currentNode.StateEquals(_goalState))
                    return (true, GetHistorySequence(currentNode));

                //currentNode.Print();

                QueueActions(currentNode);
                MarkAsVisited(currentNode);
            }

            return (false, default);
        }

        protected void MarkAsVisited(AgentPuzzleState currentNode)
        {
            this._visited.Add(currentNode.ToTuple());
        }

        protected void QueueActions(AgentPuzzleState currentNode)
        {
            QueueAction(currentNode.MoveToUp(), currentNode);
            QueueAction(currentNode.MoveRight(), currentNode);
            QueueAction(currentNode.MoveDown(), currentNode);
            QueueAction(currentNode.MoveLeft(), currentNode);
        }

        protected void QueueAction(AgentPuzzleState agentPuzzleState, AgentPuzzleState current)
        {
            if(!agentPuzzleState.StateEquals(current) && !_visited.Contains(agentPuzzleState.ToTuple()))
            {
                agentPuzzleState.Parent = current.Clone();
                _queue.Enqueue(agentPuzzleState);
            }
        }

        protected Stack<AgentPuzzleState> GetHistorySequence(AgentPuzzleState currentNode)
        {
            var sequenceHistory = new Stack<AgentPuzzleState>();

            while(currentNode != null)
            {
                sequenceHistory.Push(currentNode);
                currentNode = currentNode.Parent;
            }

            return sequenceHistory;
        }
        #endregion
    }
}
