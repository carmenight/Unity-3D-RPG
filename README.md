# Unity-3D-RPG
Basic code for a 3D RPG in C# for Unity. Must be heavily rewritten to apply to each unique case.<br/>
Currently working on:<br/>
1. Maximizing performance(mostly tiny changes)<br/>
  a. reducing Update methods<br/>
  b. looking closely at foreach loops in 'Inventory' and GetComponentsInChildren in 'InventoryUI'<br/>
  c. storing reference to main camera in 'PlayerController' in Start<br/>
  d. testing for bugs<br/>
2. Exploring gravity and raycasts to detect slopes in 'PlayerController'<br/>
3. Settings script<br/>
