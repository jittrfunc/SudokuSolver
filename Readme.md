# Sudoku Solver

This is a sudoku solver application made with C#/.NET and ASP.NET MVC.


###Architectural Overview

The project is split up into multiple logical layers, so that they can be physically separated later on into tiers/microservices if need arises.

The UI (MVC) makes use of the service layer (SudokuSolver.Service). The service layer in turn calls the core layer (SudokuSolver.Core) which contains the actual implementation of the sudoku solver algorithm.

Both the service and core layers define public contracts (interfaces) to be used by the clients. This promotes loose coupling so that an alternate implementation can easily be swapped in later on, if required.

The service follows the request-response model, where by the client sends requests and receives responses as strongly typed objects. Any errors encountered from the core layer, are wrapped up in the service response object instead of being thrown by the service. This ensures that in future if this service layer needs to be exposed as a web/REST service, 
then we just need to serialize/deserialize the response/request models respectively.

The main application (SudokuSolver.Web), uses ASP.NET MVC and Razor for the UI. The interfaces and implementations (of the serivice and core layers) are tied up here together by using Microsoft Unity container for dependency injection to keep the mapping clean and in a single place. If a different implementation is chosen later on, we only need to update UnityConfig.cs to change the mapping.


###Algorithm Implementation

The main sudoku solver algorithm is implemented as follows - 

1. Backtracking is used to check each valid digit between 1-9 is placable in the cell. A value can be placed in a cell if the row, column and 3x3 block none contain that particular value.
2. If such a valid value is found for the cell, then we recurse for the remaining cells to fill them up.
3. If the remaining cells cannot be filled with 1-9, we try a different value for the cell and repeat from step 1.
4. When we have tried placing all possible values between 1-9 and still failed to fill up the whole board, we return false to indicate the board cannot be filled with the given numeric combinations.
