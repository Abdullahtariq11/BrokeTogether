namespace BrokeTogether.Shared.Configuration
{
    public sealed class JwtSettings
    {
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public string SigningKey { get; init; } = default!;
        public int AccessTokenMinutes { get; init; } = 60;
        public int RefreshTokenDays { get; init; } = 30;
    }
}
