using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Agent
{
    public class AgentPuzzleState
    {

        public AgentPuzzleState Parent { get; set; }
        public int Dimension { get; set; }
        public int[,] State { get; set; }
        public (int i, int j) CurrentPosition { get; set; }
        public int Cost { get; set; }

        public AgentPuzzleState(int dimension)
        {
            this.Dimension = dimension;
            State = new int[Dimension, Dimension];
        }

        public AgentPuzzleState Clone()
        {
            var state = new AgentPuzzleState(this.Dimension)
            {
                Dimension = this.Dimension,
                CurrentPosition = this.CurrentPosition,
            };

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                    state.State[i, j] = this.State[i, j];
            }

            return state;
        }

        public bool StateEquals(AgentPuzzleState agentPuzzleState)
        {
            return this.ToTuple() == agentPuzzleState.ToTuple();
        }

        public void Print()
        {
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                    Console.Write(this.State[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public (int, int, int, int, int, int, int, int, int) ToTuple()
        {
            return (this.State[0, 0], this.State[0, 1], this.State[0, 2], this.State[1, 0], this.State[1, 1], this.State[1, 2], this.State[2, 0], this.State[2, 1], this.State[2, 2]);
        }

        public void Swap(int[,] state, int i, int j, int newI, int newJ)
        {
            int temp = state[i, j];
            state[i, j] = state[newI, newJ];
            state[newI, newJ] = temp;
        }

        public AgentPuzzleState MoveToUp()
        {
            var cloned = this.Clone();

            if(cloned.CurrentPosition.i > 0)
            {
                int newI = cloned.CurrentPosition.i - 1;
                int newJ = cloned.CurrentPosition.j;

                Swap(cloned.State, cloned.CurrentPosition.i, cloned.CurrentPosition.j, newI, newJ);
                cloned.CurrentPosition = (newI, newJ);
            }

            return cloned;
        }

        public AgentPuzzleState MoveRight()
        {
            var cloned = this.Clone();

            if(cloned.CurrentPosition.j < Dimension - 1)
            {
                int newI = cloned.CurrentPosition.i;
                int newJ = cloned.CurrentPosition.j + 1;

                Swap(cloned.State, cloned.CurrentPosition.i, cloned.CurrentPosition.j, newI, newJ);
                cloned.CurrentPosition= (newI, newJ);
            }

            return cloned;
        }

        public AgentPuzzleState MoveLeft()
        {
            var cloned = this.Clone();

            if(cloned.CurrentPosition.j > 0)
            {
                int newI = cloned.CurrentPosition.i;
                int newJ = cloned.CurrentPosition.j - 1;

                Swap(cloned.State, cloned.CurrentPosition.i, cloned.CurrentPosition.j, newI, newJ);
                cloned.CurrentPosition = (newI, newJ);
            }

            return cloned;
        }

        public AgentPuzzleState MoveDown()
        {
            var cloned = this.Clone();

            if(cloned.CurrentPosition.i < Dimension - 1)
            {
                int newI = cloned.CurrentPosition.i + 1;
                int newJ = cloned.CurrentPosition.j;

                Swap(cloned.State, cloned.CurrentPosition.i, cloned.CurrentPosition.j, newI, newJ);
                cloned.CurrentPosition = (newI, newJ);
            }

            return cloned;
        }

        public int CalculateHeuristic(AgentPuzzleState goalState)
        {
            int heuristic = 0;

            for(int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                    if (this.State[i, j] != goalState.State[i, j])
                        heuristic++;
            }

            return heuristic;
        }

        public int CalculateCostAndHeuristic(AgentPuzzleState goalState, int cost)
        {
            return this.CalculateHeuristic(goalState) + cost;
        }
    }
}
