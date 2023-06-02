namespace RESTArchitecture.Common.Utils
{
    public static class Calculations
    {
        public static int CalculateId(string path)
        {
            var files = Directory.GetFiles(path);
            if (files.Length == 0)
            {
                return 0;
            }

            var lastFile = Directory.GetFiles(path).Last();
            var fileName = Path.GetFileNameWithoutExtension(lastFile);
            return int.Parse(fileName) + 1;
        }
    }
}
