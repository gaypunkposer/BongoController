# Bongo Controller 

Reads in input from the Gamecube DK Bongo controllers, outputs through either the keyboard or vJoy. 
The bongos must be connected via a GC Adapter that supports HID output.

## Usage
Bongos.exe config.json

## Config format
* "pollRate" - How many times per second should the input be polled.
* "micThreshold" - The threshold, above which the microphone mapping will be triggered.
* "micSensitivity" - A multipler dictating the sensitivity of the microphone.
* "vendorID" - The vendor ID of the bongos. The default config has support for the MAYFLASH GC Adapter.
* "productID" - The product ID of the bongos. The default config has support for the MAYFLASH GC Adapter.
* "vJoyID" - The id of the controller, if using vJoy.
* "output" - The output method of the program, either "keyboard" or "vjoy".
* "keyboardMapping" - Maps the controller inputs to an output - keyboard keys.
>   * "topLeft" - The top left button of the bongo controllers.
>   * "topRight" - The top right button of the bongo controllers.
>   * "botLeft" - The bottom left button of the bongo controllers.
>   * "botRight" - The bottom right button of the bongo controllers.
>   * "mic" - The microphone in the bongo controllers. Will trigger the output if threshold reached.
>   * "start" - The start button on the bongo controllers.
* "vJoyMapping" - Maps the controller inputs to an output - vJoy buttons or axis.
>   * topLeft, topRight, botLeft, botRight, and start must be mapped to a button.
>   * mic can be mapped to either a button or an axis. If mapped to a button, will trigger if threshold reached.
