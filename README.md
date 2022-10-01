# UI-system

<!-- ABOUT THE PROJECT -->
## About The Project

The system was built for managing views on UI. Each view has its own implementation of a state depending on where it exists. The views have been categorized, those occurring on the menu scene and the game scene. Each scene has its own controller implementing state machine. It makes it easy to turn off one view and turn on another, on condition a connection between states has been created. 
In addition to the state machine that manages views during the game, there is a pop-up management system. Each pop-up window has its own implementation of the base script as well as its view. When a pop-up window appears on the screen, system that captures keyboard buttons for the state machine switches to capturing for the window system. This prevents you from going back to the previous state and exiting the pop-up window at the same time.
