[Demo Link](https://youtu.be/2tV6yi5wTtI)

Feature List
- A player that can
  - [x] Look around using a third person free-look camera
  - [x] Move around in an environment where the forward direction is determined by the camera rotation
  - [x] Jump to elevated platforms
  - [x] Tune the movement and jump parameters to make a well controlled platformer
- An environment with a large flat plane that ocntains
  - [x] boxes to jump onto and invisible walls to prevent falling off
  - [x] coins that rotate of boxes to collect
  - [x]  upon collection of the coins, they disappear and update a score value
  - [x] Proper Git History maintained
  - [x]  README with youtube links

Bonus:
- Double-jump (Decided to implement my own Ground detection with RayCast instead of OnCollisionEnter)
- Dash is iffy, there is no opposite force being applied horizontally while dashing so it feels instant rather than an initial burst of speed.
- SRP -> Tried to limit responsibilities to specific scripts. Opted for parts of the Player to subscribe to Player, instead of making Player the control variable for everything, in order to avoid the god object anti-pattern.
