using System.ComponentModel.DataAnnotations;

using static SongCatalog.Common.AppConstants;

namespace SongCatalog.Models
{
    public class Song
    {
        public Song(string title, string artistName, float rating)
        {
            Title = title;
            ArtistName = artistName;
            Rating = rating;
        }

        public string Title { get; set; } = null!;

        public string ArtistName { get; set; } = null!;

        [Range(RatingMinValue, RatingMaxValue, ErrorMessage = InvalidRatingMessage)]
        public float Rating { get; set; }
    }
}
