namespace RouteAPi.Models.Domain
{
    public class Route
    {
        public int Id { get; set; }
        public string? Origin { get; set; }  
        public string? Destination { get; set; }
        public double Distance { get; set; }    
        public int Leadtime { get; set; }
    }
}
