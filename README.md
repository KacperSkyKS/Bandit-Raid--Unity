# Bandit-Raid--Unity
You can test the game here:<br/>
https://kacpersky.itch.io/bandit-raid<br/>

You can watch the gameplay here:<br/>
https://www.youtube.com/watch?v=Xi4ZBXI8eWU<br/>
<br/>
This is a 2d project made in Unity.<br/>
This is a platform game.<br/>
The player takes on the role of a monk whose temple was attacked by bandits, and his only form of attack is a jump kick.<br/>
The game perspective is third-person.<br/>
<br/>
<br/>
Features list(including scripts responsible for the operation of this features):<br/>
- **Movement/Jump/Picking/Attack**<br/>
  - Movement: Left and Right Arrow key<br/>
  - Jump: Space
  - Picking : Space while Jumping(not falling)
  - Attack: After bouncing off an enemy and pressing Z while he is stunned
  - Scripts: 
    - [PlayerController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/PlayerController.cs) <br/>
- **AI enemies that has:** <br/>
  - Following the player after entering the trigger.<br/>
  - Attack at the right distance. <br/>
  - Scripts:
    - [EnemyAI.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/EnemiesAI/Bandit/EnemyAI.cs) <br/>
    - [FightController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/FightController.cs)<br/>
- **AI Boss that has:** <br/>
  - Draw an attack to perform.<br/>
  - Attack at the right distance. <br/>
  - Phases that change after losing health. Increasing the frequency of attacks and movement speed.
  - Scripts: 
    - [BossAI.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/EnemiesAI/Boss/BossAI.cs) <br/>
    - [BossFightController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/BossFightController.cs)<br/>
- **Respawn**
  - After death, the player can respawn at the checkpoint
  - Scripts:
    - [CheckPointController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/CheckPointController.cs)<br/>
    - [RespawnController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/RespawnController.cs)<br/>
- **UI includes:** <br/>
  - Points Counter
  - Boss Token and Health Points
  - Death Screen
  - Win Screen
  - Game Over Screen
  - Scripts:
    - [PointsController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/PointsController.cs)<br/>
    - [HealthControllerUI.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/HealthControllerUI.cs)<br/>
    - [DeathScreenController.cs)](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/DeathScreenController.cs)<br/>
    - [WinController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/WinController.cs)<br/>
    - [ButtonInGame.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/ButtonInGameController.cs)<br/>
- **Traps:**<br/>
  - Traps that activate at specific time.
  - Scripts:
    - [TrapController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Assets/Scripts/Controllers/TrapController.cs)<br/> 
<br/>
