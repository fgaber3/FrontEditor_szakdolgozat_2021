namespace FrontEditor.Client.Models.Enums
{
    public enum ChartType 
    {    
        None,
        Bar,
        Pie,
        Doughnut
    }
    public static class ChartTypeExtensions
    {
        public static string ToDisplayString(this ChartType type)
        {
            switch(type)
            {
                case ChartType.Bar:
                    return "Oszlop";
                case ChartType.Pie:
                    return "Kör";
                case ChartType.Doughnut:
                    return "Fánk";
                default:
                    return "Ismeretlen";
            }
        }
    }
}