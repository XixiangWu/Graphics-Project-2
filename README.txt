SpaceJumper -- Realistic flying game

===========================================================
Xixiang Wu 731942
	
Qian Huang 774496

===========================================================
Design

Youtube Video Link: https://www.youtube.com/watch?v=VVlJXoq01zE

The idea of astronaut flying through meteors and reach to another spaceship came from a classic movie -- Star Trek 12 Into The Darkness. In the movie scene, Captain Kirk and Khan needs to physically fly through all the remains of exploded spaceship, space rubbish and small meteors. 

Therefore, we did our best to reappear that movie scene, including the automatically generated and updated smooth guide lines which can always points out the direction you need to fly; the indicator of speed and left distance which appeared in the movie.

We also implemented some of our own idea. To make Space Jumper looks more like a game, we use "health" to indicate whether you can keep playing or needs to restart from the beginning(dead). Moreover, we also made a small animation when the player hits by a meteor, the player will move backward a little bit and shake his head. This is a pretty interesting effect in our design.

===========================================================
How to play

The rule of this game is simple, use the touch screen or gyroscope to control the direction you are looking, try not to hit any of the meteors. Hold on for several seconds until you reach the destination -- another giant spaceship.

The panel on the top left is the health indicator while top right indicates the current speed of you. The bottom panel shows the left distance you need to travel from where you are to the destination.

Cautions:
- At the very beginning of the game, you will be boosted from 10m/s to around 700m/s. The first 2.5 seconds is invulnerable, so even if you hit by a meteor, you will push them away and receive the warning sound effect as the only response.

- Two green guide lines are used to update the direction you need to go during the game. However, to make this lines moves smoothly, I generated 5 invisible anchor points. Hence, sometimes the guidelines will suddenly jump to another direction, it's normal, and it's a feature!

- If you don't follow the guide line to fly in this game, you will receive no punishment. I am working on that to make sure player has a good gaming experience when they already moves too far from the objectives.

- Sometimes the jumper will be bitted by several meteors hit simultaneously, the jumper will take all the damages. Hence, a jumper tries to fly through a "hole" which formed by low speed of meteors and accidentally hit by one of the meteors. Suddenly, all meteors get torques and rushed into jumper's face madly. The jumper may instantly died because of his unwise decision even if he his a full health before he approaches here. 

===========================================================
Models and Entities

The general idea is to build everything we need and attach the script(s) on them to make them work.

The most import things are: 

####### GUI ########

- MainGUI		// show the "Start" "Credit" "Quit"
- GameGUI		// show the "HP" "Speed" "Left distance to spaceship" etc
- GameOverGUI		// show "You win" or "You died"


####### GameControllerObject ########

- GameControllerObject	// Game Controller controls most of the gaming process, including:
			// 	- generate and update the GUI data
			//	- generate and update the guide lines
			//	- handle the death of the game
			//	- call the restart method
- MeteorController	// Meteor Controller is used to generate and clean the meteor.
			//	- It is worth mention here that the meteors is generally 			//	generated and cleaned according to the "Player's" 				// 	position. Therefore, you can see a thin layer of meteors
			// 	moving towards the spaceship.
- RestartGameController	// Called by Game Controller Object when the game is finished.
			// It will do all the clean job and reset the script and position.


####### Jumper ########
- Main Camera		// This is the main camera and it's the only camera that provide
			// a first person gaming experience in this game.
- Spot Light		// The spotlight is used to create a illumination that the space 			// is much darker than you thought and it is also the only 
			// lighting material we used in the game.  

===========================================================
Camera Motion and Graphics

The camera motion is the most important part of our game. A good camera moving strategy can lead to a great feeling of "Travelling alone through the rain of meteors". 

We did two approach to create the feeling of space jumping:
- The camera shaking which we implemented a Asset called: "EZ Camera Shake" created by "Road Turtle Games" and it is free. We use this effect when the jumper is boosting and bitten by a meteor.

- The shake head animation which implement by myself to create the jumper wants to clean his head so he shake it every time he crushed into meteors.

===========================================================
Shaders

1. BumpShader

Reference : lab7

Firstly, we created prefabs for two meteor model and bind script to them. There are 3 shader-related public varibles which are shader, texture and normal map.
We assigned them proper values, and then passed them to CG shader together with references of spotlight and its position via script.

Secondly, in .shader file, we converted all the meteors' vertex and position to world coordinate system.

Thirdly, we tested the basic texture by only using "float4 surfaceColor = tex2D(_MainTex, v.uv); return surfaceColor"

Fourthly, we added "bump effect" by using "float3 bump = (tex2D(normalMap, v.uv) - float3(0.5, 0.5, 0.5)) * 2.0;". The reason why add an Vecter3(-0.5) * 2 is 
because we need to convert its origin range (0,1) to (-1,1) which fits for bump effect.

Then, we added phongshader, amb + diff + spec.

Lastly, we kept the alpha channel unchanged.

The final performance are affected by the following coefficient : amb, diff, spec, light color, light position, light intensity, spot angle, texture, normal(shape).
And we adapted it carefully as you can see.

2. ToonShader

Reference : https://en.wikibooks.org/wiki/Cg_Programming/Unity/Toon_Shading
(Only used one pass)

At first I tried to implement this all by myself by following the cg documents. It turned out I cannot do it this way since many native function 
is not support by unity such as tex1D.

This shader is actually quite similar with the phong illumination model except for handling outline edges. We still need to calculate diff and spec based on normals.
In our special case, dot(N,V) = dot(N,L). Since we bind the character on the camera, L = V.

When we are testing it we can easy find aliasing when the camera moving closer to the meteor, some black spot appeared.
This is because the program relies on a gradual fall-off of N dot V to find silhouette edges. When objects have sharp edges, N dot V suddenly changes at the boundary, immediately causing the outline to turn completely black.



===========================================================
Code

The following codes in "SpaceJumper" is not from our original work:

- Curver.cs
	This is a script that we obtain from unity forum that could change list of points 	to a smoother list of points
	
	URL: http://answers.unity3d.com/questions/392606/line-drawing-how-can-i-interpolate-between-points.html



- EZ Camera Shake 
	Camera shaking effect
	From: Asset Store - Free


- DL_Fantasy_RPG_Effects
	For particle effect when jumper hit by meteors.
	From: Asset Store - Free

- YuliFM-MotherShipA32
	The big mothership which is placed at the destination.


