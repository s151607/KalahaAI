Contents:
	This folder contains a Visual Studio solution, source code and a .exe for the game/ai.
		- .exe can be found in \KalahaAI\bin\Debug
		- There are 3 source files AI.cs, Board.cs, and Program.cs all found in \KalahaAI. AI.cs contains the MiniMax algorithm,
		  Board.cs contains the game logic, and Program.cs controls the gameplay by asking for moves.

User guide:
	To play the game, run the .exe file and enter the desired pocket index when it is your turn. The game is made so that the human
	player is always player one, meaning that you will have pocket indices 0-5 to chose from (bottom row) and your store will be to
	the right at index 6. Indices are shown in the report.

	NOTE: Since it's implement in C# using Visual Studio, the program is meant to be run on a Windows machine. If you do not have 
	acess to one, you might have to install a VM or Visual Studio on your Mac. 


	  