-------------------
BotOps Alpha Build
-------------------

This is a very rough alpha build of BotOps. The purpose of this build is to show a rough idea of how the game is going to work, and as such most of the mechanics are not implemented yet.

- Use the mouse to select a Worker Bot on the screen and make it the Current Worker. After left-clicking on a Worker Bot, you can right-clicking on other objects on the screen to order them to move there.
- Right-clicking on a box while a Worker Bot is selected will command them to pick the box up. A Worker Bot can hold one item at a time. Right-clicking on another place like a table or a conveyor belt will order the Bot to put the box down there, if the spot is free.
- Conveyor belts are currently not functional, but they will spawn boxes with random numbers assigned every so often later.
- Clicking on a box while the selected Worker Bot is holding another box will command the Bot to perform its corresponding operation with the boxes, taking the Bot's held box as first operand and the other box as second operand. A box with the number resulting from the operation will replace the box the Bot is not holding, while the box the Bot is holding will disappear. ***This is not fully implemented, but code for it has been started***

This build is again, very rough and the core functionality of number operations is not in yet, but we're getting there.

** RECOMMENDATION **
Launch the executable in windowed mode if possible - there is no exit button in the scene yet. If the game is launched fullscreen you'll have to Alt+F4 to close the window.
