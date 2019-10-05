using System;
using WindowsInput;
using WindowsInput.Native;

namespace Bongos
{
    public class KeyboardHandler : OutputHandler
    {
        private VirtualKeyCode topLeft;
        private VirtualKeyCode topRight;
        private VirtualKeyCode botLeft;
        private VirtualKeyCode botRight;
        private VirtualKeyCode mic;
        private VirtualKeyCode start;

        private InputSimulator sim;

        public KeyboardHandler(BongoManager manager, BongoInputMapping mapping) : base(manager, mapping)
        {
            TranslateMapping(mapping);

            sim = new InputSimulator();

            manager.TopRightPressed += () => sim.Keyboard.KeyDown(topRight);
            manager.BotRightPressed += () => sim.Keyboard.KeyDown(botRight);
            manager.TopLeftPressed += () => sim.Keyboard.KeyDown(topLeft);
            manager.BotLeftPressed += () => sim.Keyboard.KeyDown(botLeft);
            manager.MicStarted += () => sim.Keyboard.KeyDown(mic);
            manager.StartPressed += () => sim.Keyboard.KeyDown(start);

            manager.TopRightHeld += () => sim.Keyboard.KeyDown(topRight);
            manager.BotRightHeld += () => sim.Keyboard.KeyDown(botRight);
            manager.TopLeftHeld += () => sim.Keyboard.KeyDown(topLeft);
            manager.BotLeftHeld += () => sim.Keyboard.KeyDown(botLeft);
            manager.MicHeld += () => sim.Keyboard.KeyDown(mic);
            manager.StartHeld += () => sim.Keyboard.KeyDown(start);

            manager.TopRightReleased += () => sim.Keyboard.KeyUp(topRight);
            manager.BotRightReleased += () => sim.Keyboard.KeyUp(botLeft);
            manager.TopLeftReleased += () => sim.Keyboard.KeyUp(topLeft);
            manager.BotLeftReleased += () => sim.Keyboard.KeyUp(botLeft);
            manager.MicReleased += () => sim.Keyboard.KeyUp(mic);
            manager.StartReleased += () => sim.Keyboard.KeyUp(start);
        }

        private void TranslateMapping(BongoInputMapping mapping)
        {
            topLeft  = ParseKeyCode(mapping.topLeft);
            topRight = ParseKeyCode(mapping.topRight);
            botLeft  = ParseKeyCode(mapping.botLeft);
            botRight = ParseKeyCode(mapping.botRight);
            mic      = ParseKeyCode(mapping.mic);
            start    = ParseKeyCode(mapping.start);
        }

        private VirtualKeyCode ParseKeyCode(string key)
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            key = key.ToUpper();
            VirtualKeyCode retVal;

            if (letters.Contains(key))
            {
                if (Enum.TryParse("VK_" + key, out retVal)) {
                    return retVal;
                }
                else
                {
                    throw new Exception("Invalid key in config");
                }
            }
            else
            {
                if (Enum.TryParse(key, out retVal))
                {
                    return retVal;
                }
                else
                {
                    throw new Exception("Invalid key in config");
                }
            }

        }
    }
}