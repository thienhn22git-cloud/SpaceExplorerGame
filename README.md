# 🚀 Unity Asteroids Clone

This is a classic 2D space shooter, inspired by the arcade game **"Asteroids"**, built in the Unity game engine.  
The player controls a spaceship in an asteroid field, shooting and destroying asteroids while avoiding collisions.

---

## 🎮 Features

- **Classic Gameplay:** Fly, shoot, and survive!  
- **Player Controls:** Smooth spaceship movement with thrust, rotation, and braking.  
- **Screen Wrap:** The player, asteroids, and bullets seamlessly wrap from one side of the screen to the other.  
- **Splitting Asteroids:** Large asteroids break into smaller, faster pieces when shot.  
- **Progressive Difficulty:** Each level increases the number of asteroids spawned.  
- **Score System:** Earn points for destroying asteroids *(1 point)* and collecting stars *(10 points)*.  
- **Star Collectibles:** 1–3 stars randomly spawn each level for bonus points.  
- **Full UI:**
  - In-game UI displays the current **Score** and **Level**.
  - A **Game Over** screen shows the **Final Score** and a **Restart** button.  
- **Scene Management:**
  - **Main Menu** with “Start Game,” “Tutorial,” and “Quit” options.  
  - **Tutorial Scene** with instructions and a “Return to Menu” button.  

---

## 🕹️ How to Play (Controls)

| Action | Key |
|--------|-----|
| Thrust Forward | ↑ Up Arrow |
| Brake / Slow Down | ↓ Down Arrow |
| Rotate Ship | ← / → Arrow Keys |
| Shoot | Spacebar |

---

## 🧩 Project Structure & Key Scripts

### `GameManager.cs`
The "brain" of the game.  
Manages:
- Game state (running, game over)
- Asteroid and star spawning
- Score and level tracking
- UI updates

### `Player.cs`
Handles:
- Player input for movement (thrust, rotation, brake)
- Shooting
- Collision detection with asteroids (triggers GameOver)

### `Asteroid.cs`
Defines asteroid behavior:
- Initial movement and size
- Splits into smaller asteroids or is destroyed when hit

### `Star.cs`
Logic for the star collectible:
- Awards 10 points via `GameManager`
- Destroys itself upon collection

### `Bullet.cs`
Controls:
- Bullet lifetime (auto-destroys after a short time to prevent clutter)

### `Wrap.cs`
Utility script for **screen wrapping**.  
Attach to Player, Asteroids, or Bullets to make them loop around the screen edges.

### `MainMenuManager.cs`
Used in the **Main Menu** scene.  
Contains:
- `StartGame()`
- `GoToTutorial()`
- `QuitGame()`

### `TutorialManager.cs`
Used in the **Tutorial Scene**.  
Contains:
- `ReturnToMainMenu()`

---

## ⚙️ Setup & How to Run

### 1. Clone / Download
Get the project files onto your local machine.

### 2. Open in Unity
- Open **Unity Hub**.  
- Click **Open** or **Add Project**.  
- Navigate to the project root folder and select it.

### 3. Check Build Settings
- Go to **File → Build Settings...**  
- Ensure the following scenes are added **in this order:**

  1. `MainMenu`  
  2. `MainGame`  
  3. `TutorialScene`

### 4. Run the Game
- Open the **MainMenu** scene from the Project window.  
- Press the **Play ▶️** button at the top of the Unity Editor.

---

## 🧠 Credits

Developed in **Unity**  
Inspired by the timeless classic *Asteroids (1979)*.

---
