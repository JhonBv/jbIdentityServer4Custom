using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crt_creditgw_auth_api.DTOs
{
    public class Client:BaseDto
    {
        public bool Enabled { get; set; }//= table.Column<bool>(nullable: false),
        public string ClientId { get; set; } //JB. Make it a Random ID similar to the Clysma one .. //= table.Column<string>(maxLength: 200, nullable: false),
        public string ProtocolType { get; set; } //= table.Column<string>(maxLength: 200, nullable: false),
        public bool RequireClientSecret { get; set; } //= table.Column<bool>(nullable: false),
        public string ClientName { get; set; } //= table.Column<string>(maxLength: 200, nullable: true),
        public string Description { get; set; }// = table.Column<string>(maxLength: 1000, nullable: true),
        public string ClientUri { get; set; } //= table.Column<string>(maxLength: 2000, nullable: true),
        public string LogoUri { get; set; } //= table.Column<string>(maxLength: 2000, nullable: true),
        public bool RequireConsent { get; set; } //= table.Column<bool>(nullable: false),
        public bool AllowRememberConsent { get; set; } //= table.Column<bool>(nullable: false),
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } //= table.Column<bool>(nullable: false),
        public bool RequirePkce { get; set; } //= table.Column<bool>(nullable: false),
        public bool AllowPlainTextPkce { get; set; } //= table.Column<bool>(nullable: false),
        public bool AllowAccessTokensViaBrowser { get; set; } //= table.Column<bool>(nullable: false),
        public string FrontChannelLogoutUri { get; set; } //= table.Column<string>(maxLength: 2000, nullable: true),
        public bool FrontChannelLogoutSessionRequired { get; set; } //= table.Column<bool>(nullable: false),
        public string BackChannelLogoutUri { get; set; }// = table.Column<string>(maxLength: 2000, nullable: true),
        public bool BackChannelLogoutSessionRequired { get; set; } //= table.Column<bool>(nullable: false),
        public bool AllowOfflineAccess { get; set; } //= table.Column<bool>(nullable: false),
        public int IdentityTokenLifetime { get; set; } //= table.Column<int>(nullable: false),
        public int AccessTokenLifetime { get; set; } //= table.Column<int>(nullable: false),
        public int AuthorizationCodeLifetime { get; set; } //= table.Column<int>(nullable: false),
        public int ConsentLifetime { get; set; } //= table.Column<int>(nullable: true),
        public int AbsoluteRefreshTokenLifetime { get; set; } //= table.Column<int>(nullable: false),
        public int SlidingRefreshTokenLifetime { get; set; } //= table.Column<int>(nullable: false),
        public int RefreshTokenUsage { get; set; } //= table.Column<int>(nullable: false),
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; } //= table.Column<bool>(nullable: false),
        public int RefreshTokenExpiration { get; set; } //= table.Column<int>(nullable: false),
        public int AccessTokenType { get; set; } //= table.Column<int>(nullable: false),
        public bool EnableLocalLogin { get; set; } //= table.Column<bool>(nullable: false),
        public bool IncludeJwtId { get; set; } //= table.Column<bool>(nullable: false),
        public bool AlwaysSendClientClaims { get; set; } //= table.Column<bool>(nullable: false),
        public string ClientClaimsPrefix { get; set; } //= table.Column<string>(maxLength: 200, nullable: true),
        public string PairWiseSubjectSalt { get; set; } //= table.Column<string>(maxLength: 200, nullable: true),
        public DateTime Created { get; set; } //= table.Column<DateTime>(nullable: false),
        public DateTime Updated { get; set; } //= table.Column<DateTime>(nullable: true),
        public DateTime LastAccessed { get; set; } //= table.Column<DateTime>(nullable: true),
        public int UserSsoLifetime { get; set; } //= table.Column<int>(nullable: true),
        public string UserCodeType { get; set; } //= table.Column<string>(maxLength: 100, nullable: true),
        public int DeviceCodeLifetime { get; set; } //= table.Column<int>(nullable: false),
        public bool NonEditable { get; set; } //= table.Column<bool>(nullable: false)
    }
}
