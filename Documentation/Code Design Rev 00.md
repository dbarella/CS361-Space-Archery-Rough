# Space Archery Base Engine - Rev. 01  
| Priority | Object | Model | Script |
| -- | ------------ | ---------| ------------------------------------------------------------|
| 00 | Setup Git + Github | None | None |
| 01 | Rocket | RocketModel | RocketAttributes + RocketMovement.cs |
| 02 | Player | PlayerModel | Bow.cs, PlayerMovement.cs |
| 03 | Map | Skybox | None |
| 04 | Target | Unity Primitive | None; assign collider and "Target" tag |
| *Bonus* | None | None | GravityField.cs | 

#### Script Details
All classes need getters and possibly setters for the variables specified here. They should all handle variable mutation in methods that other classes can call. There should be no mutation being done by an outside class - everything needs to pass through the script in which the variables reside.

1. RocketAttributes.cs
	- Contains rocket attributes
		1. public float fuel
		2. public float health, unused in this build 
2. RocketMovement.cs
	- Takes the player's input:
		1. Up/Down controls the rocket's angle of attack. Uses fuel.
		2. Space controls boost. Boost is active when depressed, inactive otherwise. Uses fuel. 
			- Sets private bool boostActive: true if active, false if inactive.
3. Bow.cs
	- This is a weapon script. It will be attached to the player prefab, not specifically instantiated. It needs to get mouse input to create a vector from the player to the mousepointer:
		1. Calculate the angle between the vector and the horizon
			- Sets private float theta.
		2. Calculate the magnitude of the vector from the player to the mousepoint. Cap this value at some modifiable bound (public float bound)
			- Sets private float velocityMagnitude.
4. PlayerMovement.cs
	- Takes the player's input:
		1. Up/Down moves the player up and down between certain bounds