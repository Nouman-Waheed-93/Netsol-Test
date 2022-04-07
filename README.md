Overview:

- The scene where all work is done is at Assets/HelicopterShootemUp/Scenes/HelicopterScene
- Scripts are placed under Assets/HelicopterShootemUp/Scripts
- HelicopterController.cs is the script that is responsible for helicopter movement(Helicopter does not move, SetOriginPoint() has been used to move the world)
- POIDownloader.cs is responsible for downloading the points of interests.
- EnemyManager.cs is an abstract class that is responsible for spawning enemies and respawning enemies when they die. Gets enemies positions from POIDownloader.cs
- RoadEnemyManager.cs extends EnemyManager.cs. This script is responsible for placing enemies on the road.
- RoofTopEnemyManager.cs extends EnemyManager.cs. This script is responsible for placing enemies on the roof tops.
- ObjectPool.cs is responsible for pooling objects. It pools objects that have PooledObject.cs on them.
- Turret.cs is responsible for automatically shooting at any object that has IDamageable component when it detects them.
- Bullet.cs is attached to the bullet and damages any IDamageable it collides with.

Tasks Done:
1. Helicopter movement
2. Enemies placed on top of buildings.
3. Enemies placed on roads.
4. Helicopter automatically fires at enemies that are in front of it.
5. Bonus Requirement : Helicopter stays still. but the world moves to give the illusion of helicopter movement.
6. Bonus Requirement : Enemies lat lon not hardcoded. POIs have been used.
7. Bonus Requirement : Object Pool used for bullets
