##B ongo Controller 

Reads in input from the Gamecube DK Bongo controllers, outputs through either the keyboard or vJoy. 
The bongos must be connected via a GC Adapter that supports HID output.

# Usage
Bongos.exe config.json

# Config format
"pollRate" - how many times per second should the input be polled
"micThreshold" - the threshold, above which the microphone mapping will be triggered
"micSensitivity" - a multipler dictating the sensitivity of the microphone
"vendorID" - the vendor ID of the bongos. The default config has support for the MAYFLASH GC Adapter
"productID" - the product ID of the bongos. The default config has support for the MAYFLASH GC Adapter
"vJoyID" - the id of the controller, if using vJoy.
"output" - the output method of the program, either "keyboard" or "vjoy"
"keyboardMapping" - maps the controller inputs to an output - keyboard keys
  "topLeft" - the top left button of the bongo controllers
  "topRight" - the top right button of the bongo controllers 
  "botLeft" - the bottom left button of the bongo controllers
  "botRight" - the bottom right button of the bongo controllers
  "mic" - the microphone in the bongo controllers. will trigger the output if threshold reached
  "start" - the start button on the bongo controllers
"vJoyMapping" - maps the controller inputs to an output - vJoy buttons or axis
  topLeft, topRight, botLeft, botRight, and start must be mapped to a button
  mic can be mapped to either a button or an axis. If mapped to a button, will trigger if threshold reached
