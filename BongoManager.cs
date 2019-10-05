using System;
using System.Collections.Generic;
using System.Text;

namespace Bongos
{
    public class BongoManager
    {
        public delegate void StateChanged(BongoData data);
        public delegate void ButtonUpdate();
        public delegate void MicChanged(int level);

        public event StateChanged StateUpdate;
        public event MicChanged MicUpdate;

        public event ButtonUpdate TopLeftPressed;
        public event ButtonUpdate TopRightPressed;
        public event ButtonUpdate BotLeftPressed;
        public event ButtonUpdate BotRightPressed;
        public event ButtonUpdate StartPressed;
        public event ButtonUpdate MicStarted;

        public event ButtonUpdate TopLeftHeld;
        public event ButtonUpdate TopRightHeld;
        public event ButtonUpdate BotLeftHeld;
        public event ButtonUpdate BotRightHeld;
        public event ButtonUpdate StartHeld;
        public event ButtonUpdate MicHeld;

        public event ButtonUpdate TopLeftReleased;
        public event ButtonUpdate TopRightReleased;
        public event ButtonUpdate BotLeftReleased;
        public event ButtonUpdate BotRightReleased;
        public event ButtonUpdate StartReleased;
        public event ButtonUpdate MicReleased;

        private int micThreshold;

        private BongoData previousBongo;

        public BongoManager (int micThreshold)
        {
            this.micThreshold = micThreshold;
        }

        public void UpdateState(byte[] data)
        {
            BongoData update = new BongoData(data);

            if (!update.Equals(previousBongo))
            {
                StateUpdate?.Invoke(update);
            }

            if (update.micLevel != previousBongo.micLevel)
            {
                MicUpdate?.Invoke(update.micLevel);
            }

            if (update.botLeft && !previousBongo.botLeft)      BotLeftPressed?.Invoke();
            if (update.botRight && !previousBongo.botRight)    BotRightPressed?.Invoke();
            if (update.topLeft && !previousBongo.topLeft)      TopLeftPressed?.Invoke();
            if (update.topRight&& !previousBongo.topRight)     TopRightPressed?.Invoke();
            if (update.start && !previousBongo.start)          StartPressed?.Invoke();
            if (update.micLevel >= micThreshold && previousBongo.micLevel < micThreshold) MicStarted?.Invoke();

            if (update.botLeft && previousBongo.botLeft)       BotLeftHeld?.Invoke();
            if (update.botRight && previousBongo.botRight)     BotRightHeld?.Invoke();
            if (update.topLeft && previousBongo.topLeft)       TopLeftHeld?.Invoke();
            if (update.topRight && previousBongo.topRight)     TopRightHeld?.Invoke();
            if (update.start && previousBongo.start)           StartHeld?.Invoke();
            if (update.micLevel >= micThreshold && previousBongo.micLevel >= micThreshold) MicHeld?.Invoke();

            if (!update.botLeft && previousBongo.botLeft)      BotLeftReleased?.Invoke();
            if (!update.botRight && previousBongo.botRight)    BotRightReleased?.Invoke();
            if (!update.topLeft && previousBongo.topLeft)      TopLeftReleased?.Invoke();
            if (!update.topRight && previousBongo.topRight)    TopRightReleased?.Invoke();
            if (!update.start && previousBongo.start)          StartReleased?.Invoke();
            if (update.micLevel < micThreshold && previousBongo.micLevel >= micThreshold) MicReleased?.Invoke();

            //Console.WriteLine(update.micLevel);

            previousBongo = update;
        }
    }

    public struct BongoData
    {
        public bool topLeft;
        public bool topRight;
        public bool botLeft;
        public bool botRight;
        public bool start;
        public int micLevel;

        public BongoData(byte[] data)
        {
            topRight = (data[0] & 0x1) != 0;
            botRight = (data[0] & 0x2) != 0;
            
            botLeft = (data[0] & 0x4) != 0;
            topLeft = (data[0] & 0x8) != 0;

            start = (data[1] & 0x2) != 0;

            micLevel = data[7];
        }

        public override bool Equals(object obj)
        {
            return obj is BongoData data &&
                   topLeft == data.topLeft &&
                   topRight == data.topRight &&
                   botLeft == data.botLeft &&
                   botRight == data.botRight &&
                   start == data.start &&
                   micLevel == data.micLevel;
        }

        public override int GetHashCode()
        {
            var hashCode = -330359105;
            hashCode = hashCode * -1521134295 + topLeft.GetHashCode();
            hashCode = hashCode * -1521134295 + topRight.GetHashCode();
            hashCode = hashCode * -1521134295 + botLeft.GetHashCode();
            hashCode = hashCode * -1521134295 + botRight.GetHashCode();
            hashCode = hashCode * -1521134295 + start.GetHashCode();
            hashCode = hashCode * -1521134295 + micLevel.GetHashCode();
            return hashCode;
        }
    }
}
