using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Agent
{
    public class BestFirstSearchAgent: SolutionAgent
    {

        private PriorityQueue<AgentPuzzleState, int> _priorityQueue;

        public BestFirstSearchAgent(AgentPuzzleState initialState, AgentPuzzleState goalState): base(initialState, goalState)
        {
        }

        public (bool success, Stack<AgentPuzzleState> sequenceHistory) ExecuteBestFirstSearch()
        {
            this._priorityQueue = new PriorityQueue<AgentPuzzleState, int>();
            this._visited = new HashSet<(int, int, int, int, int, int, int, int, int)>();

            this._priorityQueue.Enqueue(this._initialState, this._initialState.CalculateHeuristic(this._goalState));

            while (_priorityQueue.Count > 0)
            {
                var currentNode = _priorityQueue.Dequeue();

                if (currentNode.StateEquals(_goalState))
                    return (true, GetHistorySequence(currentNode));

                EnqueActionsWithHeuristic(currentNode);
                MarkAsVisited(currentNode);
            }

            return (false, default);
        }

        protected void EnqueActionsWithHeuristic(AgentPuzzleState currentNode)
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
                agentPuzzleState.Parent = current;
                _priorityQueue.Enqueue(agentPuzzleState, agentPuzzleState.CalculateHeuristic(_goalState));
            }
        }
    }
}
