# Bandit-Raid--Unity
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
    - [PlayerController.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/blob/main/Scripts/Controllers/PlayerController.cs) <br/>
- **AI enemies that has:** <br/>
  - Following the player after entering the trigger.<br/>
  - Attack at the right distance. <br/>
  - Scripts:
    - [EnemyAI.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/blob/main/Scripts/EnemiesAI/Bandit/EnemyAI.cs) <br/>
- **AI Boss that has:** <br/>
  - Draw an attack to perform.<br/>
  - Attack at the right distance. <br/>
  - Phases that change after losing health. Increasing the frequency of attacks and movement speed.
  - Scripts: 
    - [BossAI.cs](https://github.com/KacperSkyKS/Bandit-Raid--Unity/tree/main/Scripts/EnemiesAI/Boss) <br/>
- **Respawn**
  - After death, the player can respawn at the checkpoint
  - Scripts:
    - [CheckPointController.cs] (https://github.com/KacperSkyKS/Bandit-Raid--Unity/blob/main/Scripts/Controllers/CheckPointController.cs)
- **System of upgrading statistics** <br/>
  - Possibility to upgrade stats such as: <br/>
    - Health
    - Damage
    - Attack speed
    - Movement speed
  - Scripts: UpgradePanelUI.cs<br/>
- **Equipment**<br/>
  - Coins.<br/>
  - Scripts: InventoryManager.cs<br/>
- **An interactive door that opens when the player approaches if the waves are not turned on and the wave is not currently in progress.** <br/>
  - Scripts: DeviceTrigger.cs, DoorOpenDevice.cs<br/> 
- **Interactive buttons in the Main Menu and the Game Over screen**<br/>
  - Scripts: MainMenu.cs, EventButttons.cs<br/> 
<br/>
You can test the game here:<br/>
https://kacpersky.itch.io/plush-toy-attack
