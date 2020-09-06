# Nunit tests for Angular busy demo page
## Intro
Tests writted in this repo test, if the angular busy demo page is working correctly. 

## Test Organization
I have used page object model to create tests
- Pages can be found here()
- Tests can be found here()
- Utilities can be found here()

## Test case flow
- TC1 - TC5: Checks for the presence of delay, duration, message and table
- TC6 - TC11: Sets delay, duration, message, backdrop(set and unset) and selects standard loading template
- TC12: Checks if loader appers after the set delay
- TC13, 14: Check if busy text is displayed on the loader and that it matches the message set
- TC 15: Checks if loader goes away after the duration set
- TC 16: Clears duration, sets delay to 1000 ms and changes the message to "Waiting"
- TC 17: Selects custom template
- TC 19-21: Checks if custom loading image appears after set delay and the text on the custom image, along with the duration

## Failed cases
- T15_CheckLoaderDuration
  - duration is set to 10000 ms, with 2000 sec delay, and the loader is displayed for 8000 ms
- T20_ChecksCustomLoaderDuration
  - duration is set to 1000 ms, with 0 ms delay, loader is displayed for 3000 ms

