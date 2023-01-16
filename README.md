# Sudoku Solver using DLX algorithm

A sudoku solver implemented in C# using the DLX (Dancing Links X) algorithm. This solver is capable of solving any sudoku puzzle, including the most difficult ones.

## Getting Started

These instructions will help you set up and run the sudoku solver on your local machine.

### Prerequisites

- Visual Studio (or any other IDE that supports C#)
- .NET Framework 3.1 or higher

### Installing

- Clone or download the repository
- Open the solution file in Visual Studio
- Build and run the project

### Input

The input to the solver comes in a form of "sudokuData", for example 100000027000304015500170683430962001900007256006810000040600030012043500058001000 will look like this:
<img alt="sudoku visual exmaple" src="https://cdn.discordapp.com/attachments/756025558781132841/1058152945222426696/image.png">

you can input the sudokuData with the console of with a text file.

### Output

The output of the solver is the solved sudoku puzzle displayed in the console, and also added to the text file if it was chosen as the input method.

## How it works

The DLX algorithm is a powerful algorithm for solving exact cover problems, such as sudoku. It uses a technique called "dancing links" to efficiently search for solutions.

## Built With

- C#
- .NET Framework

## Authors

- Jonathan Goren

<br>
<br>
<img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/jonyplayz/OmegaSudoku?style=for-the-badge">