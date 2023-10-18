

using Puzzle.Agent;

var initialState = new AgentPuzzleState(3)
{
    CurrentPosition = (1, 0),
    State = new int[,] { { 1, 8, 2 }, { 0, 4, 3 }, { 7, 6, 5 } }
};

var goalState = new AgentPuzzleState(3)
{
    CurrentPosition = (1, 1),
    State = new int[,] { { 1, 2, 3 }, { 4, default, 5 }, { 6, 7, 8 } }
};


//var breadthFirstSearchAgent = new SolutionAgent(initialState, goalState);
//var result1 = breadthFirstSearchAgent.ExecuteBreadthFirstSearch();
//
//if (result1.success)
//    result1.sequenceHisotry.ToList().ForEach(history => history.Print());
//
//var bestFirstSearchAgent = new BestFirstSearchAgent(initialState, goalState);
//var result2 = bestFirstSearchAgent.ExecuteBestFirstSearch();
//
//if(result2.success)
//    result2.sequenceHistory.ToList().ForEach(history => history.Print());

var aStarAgent = new AStarAgent(initialState, goalState);
var result3 = aStarAgent.ExecuteAStarSearch();

if ((result3.success))
    result3.sequenceHistory.ToList().ForEach(history => history.Print());