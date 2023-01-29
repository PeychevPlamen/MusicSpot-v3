namespace MusicSpot_v3.Core.Models.Artists
{
    public class DetailsArtistFormModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Genre { get; set; }

        public string? Description { get; set; }

        public bool IsPublic { get; set; }
    }
}
