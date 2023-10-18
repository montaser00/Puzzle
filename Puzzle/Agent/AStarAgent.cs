using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Agent
{
    public class AStarAgent: SolutionAgent
    {

        private PriorityQueue<AgentPuzzleState, int> _priorityQueue;

        public AStarAgent(AgentPuzzleState initialState, AgentPuzzleState goalState) : base(initialState, goalState)
        {
        }

        public (bool success, Stack<AgentPuzzleState> sequenceHistory) ExecuteAStarSearch()
        {
            this._priorityQueue = new PriorityQueue<AgentPuzzleState, int>();
            this._visited = new HashSet<(int, int, int, int, int, int, int, int, int)>();

            this._priorityQueue.Enqueue(this._initialState, this._initialState.CalculateHeuristic(this._goalState));

            while (_priorityQueue.Count > 0)
            {
                var currentNode = _priorityQueue.Dequeue();

                if (currentNode.StateEquals(_goalState))
                    return (true, GetHistorySequence(currentNode));

                EnqueActionsWithHeuristicAndCost(currentNode);
                MarkAsVisited(currentNode);
            }

            return (false, default);
        }

        protected void EnqueActionsWithHeuristicAndCost(AgentPuzzleState currentNode)
        {
            EnqueActionWithHeuristic(currentNode.MoveToUp(), currentNode);
            EnqueActionWithHeuristic(currentNode.MoveRight(), currentNode);
            EnqueActionWithHeuristic(currentNode.MoveDown(), currentNode);
            EnqueActionWithHeuristic(currentNode.MoveLeft(), currentNode);
        }

        protected void EnqueActionWithHeuristic(AgentPuzzleState agentPuzzleState, AgentPuzzleState current)
        {
            if (!agentPuzzleState.StateEquals(current) && !_visited.Contains(agentPuzzleState.ToTuple()))
            {
                if (agentPuzzleState.Parent == null)
                    agentPuzzleState.Cost = 1;
                else
                    agentPuzzleState.Cost = agentPuzzleState.Parent.Cost + 1;

                agentPuzzleState.Parent = current;
                _priorityQueue.Enqueue(agentPuzzleState, agentPuzzleState.CalculateCostAndHeuristic(_goalState, agentPuzzleState.Cost));
            }
        }
    }
}
