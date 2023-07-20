namespace SnipeLabelEditor.Data
{
    public static class PxMmConverter
    {
        public static double MmToPx(double mm)
        {
            return mm * 3.7795275591;
        }

        public static double PxToMm(double px)
        {
            return px / 3.7795275591;
        }
    }
}
