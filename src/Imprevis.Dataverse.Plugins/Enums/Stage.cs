namespace Imprevis.Dataverse.Plugins
{
    public static class Stage
    {
        public const int PreValidation = 10;
        public const int PreOperation = 20;
        public const int PostOperation = 40;

        public static string GetName(int stage)
        {
            switch (stage)
            {
                case 10:
                    return "PreValidation";
                case 20:
                    return "PreOperation";
                case 40:
                    return "PostOperation";
                default:
                    return "Unkown";
            }
        }
    }
}
