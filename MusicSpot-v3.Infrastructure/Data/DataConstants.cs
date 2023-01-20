
namespace MusicSpot_v3.Infrastructure.Data
{
    public class DataConstants
    {
        public const int AlbumMaxNameLength = 160;
        public const int FormatMaxLength = 20;
        public const int NotesMaxLength = 250;

        public const int ArtistMaxNameLength = 60;
        public const int GenreMaxLength = 40;

        public const int TrackMaxNameLength = 100;
        public const string TrackDurationRegEx = @"^([0-9]+:[0-5][0-9])";

        public const int MinYearValue = 1900;
        public const int MaxYearValue = 2023;

        public const int UsernameMinLength = 5;
        public const int UsernameMaxLength = 20;

        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 20;

        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int TitleMinLenght = 1;
        public const int TitleMaxLength = 100;

        public const int GenreMinLength = 2;

        public const int DescriptionMaxLength = 250;
    }
}
