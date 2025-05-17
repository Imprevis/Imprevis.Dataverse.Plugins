namespace Imprevis.Dataverse.Plugins
{
    public static class Mode
    {
        public const int Synchronous = 0;
        public const int Asynchronous = 1;

        public static string GetName(int stage)
        {
            switch (stage)
            {
                case 0:
                    return "Synchronous";
                case 1:
                    return "Asynchronous";
                default:
                    return "Unkown";
            }
        }
    }
}
