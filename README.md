Snake Game in Unity 3D c#

> This is a grid based classic snake game.

# Rules

- Snakes moves one space each update
- The snake will continue to move in the direction it is already moving
- If player has changed the direction since the last update, the head of snake will move in the new direction
- Player can change the direction using keyboard.
- Each player will use different key bindings
- The snake will stay the same length, unless the head moves onto an apple, in which case the
length will increase by 1
- Once an apple has collided with the head of the snake, it will respawn somewhere that no snake exists
- There is just one apple at a time
- If the player collides with a boundary it loses
- If the player collides with itself it loses
- If the player collides with another snake, it loses
- If a player has another snake collide with its own body, it survives
- When a player loses, it is removed from the game entirely.
- When all snakes lose, the game ends
- When the snake(s) take up the entire screen, the game ends

## Documentation

### How to run

- Open Scenes/Main
- Run the scene
- Press space key to start the game

### Adding player

- On the Main Camera, locate the Player Manager
- Add new players to the player infos
- New player data is being saved as a sctiptable object
- Create a new player data, Click Assets/Create/Player Info

### Changing player input

We are using the new Unity Input system. All the input actions are saved in the Assets/InputAction folder.

