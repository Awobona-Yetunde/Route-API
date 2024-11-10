namespace RouteAPi.Models.DTO
{
    public class AddRouteRequestDTO
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double Distance { get; set; }
        public int Leadtime { get; set; }
    }
}
