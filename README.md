# BallingGame
It is a C#, Visual Studio toy project. Show score board according to your pin count.
## How to use
* Clone this project
* Open 'BallingGame.sln'
* Run
## Code Description
### Program.cs
There is a main function. You can change pin count by changing parameter.
### Game Class
* rounds : Array of round. It stores every information of each round
* nowRound : int. It saves the number of rounds now
#### Constructor
* Initialize rounds' size to 10
* From first round(rounds[0]) to ninth round(rounds[8]) are initialized with Round
* Last round(rounds[9]) is initilized with LastRound
#### Public Method
* Get input of pin count
* If input is invalid, print error message and ignore the input
* If input is valid, store the input and print scoreboard
#### Private Method
All private methods in this class are used to output scoreboard.
The round sum is only printed, if the round score is **fully verified**
### Round Class
* state : Store state of this round
	+ State.None : This round is not end yet
	+ State.Open : The round was over and player missed some pins
	+ State.Spare : The round was over and the player knocked down all pins with two balls
	+ State.Strike : The round was over and the player knocked down all pins with one ball
* record : Array of nullable int. It store knocked down pin counts
* bonus : Array of nullable int. It store bonus pin counts when it needs
### LastRound Class
LastRound class inherits Round class
**LastRound is special**, because there is no round after that. So we have to record our bonus points to this class