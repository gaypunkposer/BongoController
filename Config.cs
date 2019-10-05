namespace Bongos
{
    class BongoConfig
    {
        public int pollRate = 250;
        public int micThreshold = 15;
        public int micSensitivity = 1000;
        public int vendorID = 0x0079;
        public int productID = 0x1843;
        public string output = "keyboard";
        public int vJoyId = 1;
        public BongoInputMapping keyboardMapping;
        public BongoInputMapping vJoyMapping;
    }

    public class BongoInputMapping
    {
        public string topLeft;
        public string topRight;
        public string botLeft;
        public string botRight;
        public string mic;
        public string start;
    }
}
