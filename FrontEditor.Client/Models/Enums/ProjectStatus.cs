namespace FrontEditor.Client.Models.Enums
{
    public enum ProjectStatus 
    {    
       New,
       Editable,
       Exported  
    }
    public static class ProjectStatusExtensions
    {
        public static string ToDisplayString(this ProjectStatus status)
        {
            switch(status)
            {
            case ProjectStatus.New:
                return "Új";
            case ProjectStatus.Editable:
                return "Szerkeszthető";
            case ProjectStatus.Exported:
                return "Exportált";
            default:
                return "Ismeretlen";
            }
        }
    }
}