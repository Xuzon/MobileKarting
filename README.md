# MobileKarting

## Instructions

For Install download the .apk and run
### Movement
Tap on the right side to turn right, left to turn left
For drifting move one finger 20% of the height of screen from tapping

## Modifications

### Added
#### Assets
- For coin I created a model in blender called coin.blend
- MainMenu scene
#### Scripts
- Coin
- CoinManager
- MobileInput
- MainMenu
- EndMenu
- Singleton

### Modify
#### Assets
* SampleScene
  * TimeDisplayCanvas
  * Added coins
  * Added CoinManager component to TrackManager
  * Modified Kart to use MobileInput
#### Scripts
- TrackManager -> EndRace events and NewRecord events
- TimeDisplay -> show new record
