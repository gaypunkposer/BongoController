using System;
using System.Collections.Generic;
using System.Text;
using vJoyInterfaceWrap;

namespace Bongos
{
    public class VJoyHandler : OutputHandler
    {
        private uint topLeft;
        private uint topRight;
        private uint botLeft;
        private uint botRight;
        private uint micButton;
        private HID_USAGES micAxis;
        private uint start;

        private int micSensitivity;

        private vJoy joystick;
        public VJoyHandler(BongoManager manager, BongoInputMapping mapping, int id, int mic) : base(manager, mapping)
        {
            joystick = new vJoy();
            uint _id = (uint)id;
            micSensitivity = mic;

            if (id <= 0 | id > 16)
            {
                Program.WriteErrorToConsoleAndExit("Invalid vJoy id");
            }

            if (!joystick.vJoyEnabled())
            {
                Program.WriteErrorToConsoleAndExit("vJoy not enabled");
            }

            if (!joystick.AcquireVJD(_id))
            {
                Program.WriteErrorToConsoleAndExit("Failed to acquire vJoy device");
            }

            TranslateMapping(mapping);

            joystick.ResetVJD(_id);

            manager.TopRightPressed  += () => joystick.SetBtn(true, _id, topRight);
            manager.BotRightPressed  += () => joystick.SetBtn(true, _id, botRight);
            manager.TopLeftPressed   += () => joystick.SetBtn(true, _id, topLeft);
            manager.BotLeftPressed   += () => joystick.SetBtn(true, _id, botLeft);
            manager.StartPressed     += () => joystick.SetBtn(true, _id, start);

            manager.TopRightReleased += () => joystick.SetBtn(false, _id, topRight);
            manager.BotRightReleased += () => joystick.SetBtn(false, _id, botRight);
            manager.TopLeftReleased  += () => joystick.SetBtn(false, _id, topLeft);
            manager.BotLeftReleased  += () => joystick.SetBtn(false, _id, botLeft);
            manager.StartReleased    += () => joystick.SetBtn(false, _id, start);

            if (micButton == 255)
            {
                manager.MicUpdate    += (int update) => joystick.SetAxis(update * micSensitivity, _id, micAxis);
            }
            else
            {
                manager.MicStarted   += () => joystick.SetBtn(true, _id, micButton);
                manager.MicReleased  += () => joystick.SetBtn(false, _id, micButton);
            }
        }

        private void TranslateMapping(BongoInputMapping mapping)
        {
            topLeft  = uint.Parse(mapping.topLeft);
            topRight = uint.Parse(mapping.topRight);
            botLeft  = uint.Parse(mapping.botLeft);
            botRight = uint.Parse(mapping.botRight);
            start    = uint.Parse(mapping.start);

            if (!uint.TryParse(mapping.mic, out micButton))
            {
                micButton = 255;
                Enum.TryParse("HID_USAGE_" + mapping.mic, out micAxis);
            }
        }
    }
}
