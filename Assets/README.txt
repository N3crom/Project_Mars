README:

To test the system go to !_Project -> Scenes -> Scenes_Gym 
And play it, you need to collect all coins (yellow  circle) and after it reach the exit tile (green tile)

you can move with arrow key and ZQSD

How the systems works:

	P_CoinAreaDetection: detecte all coins in the collider(trigger) so make sure all the coins are in it

	P_GridManager: Set up the grid with the size of the grid and SSO_LevelGridData where you put the predefinied tiles if they has coins or are walkable and the type of it to initialise
  		it with the grid manager and all the grid tile not definied are by default wall
	
	S_Coins come from S_Collectible if we need to add other collectibles 


