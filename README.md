# Unity-3D-RPG
Basic code for a 3D RPG in C# for Unity. Must be heavily rewritten to apply to each unique case. 
Currently working on:
1. Maximizing performance(mostly tiny changes)
  a. reducing Update methods
  b. looking closely at foreach loops in 'Inventory' and GetComponentsInChildren in 'InventoryUI'
  c. storing reference to main camera in 'PlayerController' in Start
  d. testing for bugs
2. Exploring gravity and raycasts to detect slopes in 'PlayerController'
