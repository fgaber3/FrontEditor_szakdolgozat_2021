using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace FrontEditor.Client.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectJSONDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectData = table.Column<string>(type: "LONGTEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectJSONDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    ProfileImage = table.Column<byte[]>(nullable: true),
                    Registration = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    LastEdit = table.Column<DateTime>(nullable: false),
                    ExportCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(maxLength: 128, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProjectJSONDatas",
                columns: new[] { "Id", "ProjectData", "ProjectId" },
                values: new object[] { 1, "{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorModelData, FrontEditor.Client\",\"Title\":\"Példa projekt\",\"Description\":\"Ez egy példa projekt.\",\"CustomCss\":\"body {\\n  color: #436993;\\n}\",\"Blocks\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.EditorBaseViewModel, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorHeaderViewModel, FrontEditor.Client\",\"Title\":\"Példa projekt\",\"Icon\":\"eye\",\"LogoImage\":null,\"FixedMenu\":true,\"MobileMenu\":false,\"ContainerMode\":true,\"ShowSearchBox\":false,\"ColorSheme\":1,\"BackgroundColor\":\"#2e4967\",\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client\",\"Title\":\"Főoldal\",\"Icon\":\"user\",\"Href\":\"#\",\"TargetBlank\":false,\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client\",\"Title\":\"Wikipédia\",\"Icon\":\"book\",\"Href\":\"https://hu.wikipedia.org/\",\"TargetBlank\":true,\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client\",\"Title\":\"Microsoft\",\"Icon\":\"globe\",\"Href\":\"https://www.microsoft.com/hu-hu/\",\"TargetBlank\":true,\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client\",\"Title\":\"Microsoft 365\",\"Icon\":\"at\",\"Href\":\"https://www.microsoft.com/hu-hu/microsoft-365\",\"TargetBlank\":true,\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client\",\"Title\":\"Windows\",\"Icon\":\"columns\",\"Href\":\"https://www.microsoft.com/hu-hu/windows/\",\"TargetBlank\":true,\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}}]}}]},\"ViewName\":\"_headerEditor\",\"ProjectId\":1,\"ComponentName\":\"Menü\",\"ComponentId\":\"project-header\",\"ColorShemes\":{\"$type\":\"System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[FrontEditor.Client.Models.EditorModels.ColorShemeEnum, FrontEditor.Client]], System.Private.CoreLib]], System.Private.CoreLib\",\"$values\":[{\"Key\":\"Világos\",\"Value\":0},{\"Key\":\"Sötét\",\"Value\":1}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorCarouselViewModel, FrontEditor.Client\",\"ContainerMode\":true,\"Controls\":true,\"Indicators\":true,\"AutoRun\":true,\"Captions\":true,\"CaptionsTextColor\":\"#ffffff\",\"CaptionsTextBackgroundColor\":\"#000000\",\"AnimationMode\":1,\"HeightSize\":500,\"PageInterval\":3000,\"CarouselItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.CarouselItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.CarouselItem, FrontEditor.Client\",\"Image\":\"duck.jpg\",\"Title\":\"Kacsa\",\"Description\":\"A képen egy kacsa van.\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.CarouselItem, FrontEditor.Client\",\"Image\":\"kid.jpg\",\"Title\":\"Gyerek kutyával\",\"Description\":\"A képen egy gyerek játszik egy kutyával.\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.CarouselItem, FrontEditor.Client\",\"Image\":\"mood.jpg\",\"Title\":\"Hangulat\",\"Description\":\"A képen egy lankás táj látható.\"}]},\"ViewName\":\"_carouselEditor\",\"ProjectId\":1,\"ComponentName\":\"Carousel - 1\",\"ComponentId\":\"carousel-637734425601321648\",\"ColorShemes\":{\"$type\":\"System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[FrontEditor.Client.Models.EditorModels.ColorShemeEnum, FrontEditor.Client]], System.Private.CoreLib]], System.Private.CoreLib\",\"$values\":[{\"Key\":\"Világos\",\"Value\":0},{\"Key\":\"Sötét\",\"Value\":1}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorBlocksViewModel, FrontEditor.Client\",\"ContainerMode\":true,\"ItemsPerRow\":4,\"BackgroundColor\":\"#e4f1ff\",\"BlockItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Horgony\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":\"<center>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</center>\",\"ImageHref\":null,\"IconName\":\"anchor\",\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Kosárlabda\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":\"<center>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</center>\",\"ImageHref\":null,\"IconName\":\"basketball-ball\",\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Kerékpár\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":\"<center>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</center>\",\"ImageHref\":null,\"IconName\":\"bicycle\",\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Torta\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":\"<center>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</center>\",\"ImageHref\":null,\"IconName\":\"birthday-cake\",\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}}]},\"ViewName\":\"_blocksEditor\",\"ProjectId\":1,\"ComponentName\":\"Blocks - 1\",\"ComponentId\":\"blocks-637734431908272422\",\"ColorShemes\":{\"$type\":\"System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[FrontEditor.Client.Models.EditorModels.ColorShemeEnum, FrontEditor.Client]], System.Private.CoreLib]], System.Private.CoreLib\",\"$values\":[{\"Key\":\"Világos\",\"Value\":0},{\"Key\":\"Sötét\",\"Value\":1}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorBlocksViewModel, FrontEditor.Client\",\"ContainerMode\":true,\"ItemsPerRow\":3,\"BackgroundColor\":\"#ffffff\",\"BlockItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":\"diagram01\",\"Title\":\"Oszlop diagram\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":null,\"ImageHref\":null,\"IconName\":null,\"ChartType\":1,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Red\",\"Data\":12,\"Color\":\"#ff6384\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Blue\",\"Data\":19,\"Color\":\"#36a2eb\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Yellow\",\"Data\":3,\"Color\":\"#ffce56\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Green\",\"Data\":5,\"Color\":\"#4abf6d\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Purple\",\"Data\":2,\"Color\":\"#9966ff\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Orange\",\"Data\":3,\"Color\":\"#ff9f40\"}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":\"diagram02\",\"Title\":\"Kör diagram\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":null,\"ImageHref\":null,\"IconName\":null,\"ChartType\":2,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Red\",\"Data\":12,\"Color\":\"#ff6384\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Blue\",\"Data\":19,\"Color\":\"#36a2eb\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Yellow\",\"Data\":3,\"Color\":\"#ffce56\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Green\",\"Data\":5,\"Color\":\"#4abf6d\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Purple\",\"Data\":2,\"Color\":\"#9966ff\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Orange\",\"Data\":3,\"Color\":\"#ff9f40\"}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":\"diagram03\",\"Title\":\"Fánk diagram\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":null,\"ImageHref\":null,\"IconName\":null,\"ChartType\":3,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Red\",\"Data\":12,\"Color\":\"#ff6384\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Blue\",\"Data\":19,\"Color\":\"#36a2eb\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Yellow\",\"Data\":3,\"Color\":\"#ffce56\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Green\",\"Data\":5,\"Color\":\"#4abf6d\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Purple\",\"Data\":2,\"Color\":\"#9966ff\"},{\"$type\":\"FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client\",\"Label\":\"Orange\",\"Data\":3,\"Color\":\"#ff9f40\"}]}}]},\"ViewName\":\"_blocksEditor\",\"ProjectId\":1,\"ComponentName\":\"Blocks - 2\",\"ComponentId\":\"blocks-637734437347539272\",\"ColorShemes\":{\"$type\":\"System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[FrontEditor.Client.Models.EditorModels.ColorShemeEnum, FrontEditor.Client]], System.Private.CoreLib]], System.Private.CoreLib\",\"$values\":[{\"Key\":\"Világos\",\"Value\":0},{\"Key\":\"Sötét\",\"Value\":1}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorBlocksViewModel, FrontEditor.Client\",\"ContainerMode\":true,\"ItemsPerRow\":1,\"BackgroundColor\":\"#e4f1ff\",\"BlockItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Házi Kacsa (Anas platyrhynchos domestica)\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":\"<center>A házi kacsa (Anas platyrhynchos domestica) a récefélék családjába tartozó baromfi, a tőkés réce (vadkacsa) alfaja, háziasított változata. Többnyire fehér színben tenyésztik, de egyes vidékeken – különösen ott, ahol vadon élő őseivel könnyen kereszteződhet – „vad” színezetű példányokat is találunk.</center>\",\"ImageHref\":\"duck.jpg\",\"IconName\":null,\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}}]},\"ViewName\":\"_blocksEditor\",\"ProjectId\":1,\"ComponentName\":\"Blocks - 3\",\"ComponentId\":\"blocks-637734445998390209\",\"ColorShemes\":{\"$type\":\"System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[FrontEditor.Client.Models.EditorModels.ColorShemeEnum, FrontEditor.Client]], System.Private.CoreLib]], System.Private.CoreLib\",\"$values\":[{\"Key\":\"Világos\",\"Value\":0},{\"Key\":\"Sötét\",\"Value\":1}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorBlocksViewModel, FrontEditor.Client\",\"ContainerMode\":true,\"ItemsPerRow\":1,\"BackgroundColor\":\"#ffffff\",\"BlockItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Lorem Ipsum\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":0,\"Content\":\"<p><a href='https://www.lipsum.com/' target='_blank'>Lorem Ipsum</a> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>\\n<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>\",\"ImageHref\":null,\"IconName\":null,\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}}]},\"ViewName\":\"_blocksEditor\",\"ProjectId\":1,\"ComponentName\":\"Blocks - 4\",\"ComponentId\":\"blocks-637734453652926379\",\"ColorShemes\":{\"$type\":\"System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[FrontEditor.Client.Models.EditorModels.ColorShemeEnum, FrontEditor.Client]], System.Private.CoreLib]], System.Private.CoreLib\",\"$values\":[{\"Key\":\"Világos\",\"Value\":0},{\"Key\":\"Sötét\",\"Value\":1}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorBlocksViewModel, FrontEditor.Client\",\"ContainerMode\":true,\"ItemsPerRow\":2,\"BackgroundColor\":\"#e4f1ff\",\"BlockItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Kacsa\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":null,\"ImageHref\":\"duck.jpg\",\"IconName\":null,\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Kutya\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":null,\"ImageHref\":\"kid.jpg\",\"IconName\":null,\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Kutya\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":null,\"ImageHref\":\"kid.jpg\",\"IconName\":null,\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.BlockItem, FrontEditor.Client\",\"BlockItemId\":null,\"Title\":\"Kacsa\",\"TitleColor\":\"#2e4967\",\"TitleAlign\":2,\"Content\":null,\"ImageHref\":\"duck.jpg\",\"IconName\":null,\"ChartType\":0,\"ChartRows\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.ChartRow, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}}]},\"ViewName\":\"_blocksEditor\",\"ProjectId\":1,\"ComponentName\":\"Blocks - 5\",\"ComponentId\":\"blocks-637734453573369273\",\"ColorShemes\":{\"$type\":\"System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[FrontEditor.Client.Models.EditorModels.ColorShemeEnum, FrontEditor.Client]], System.Private.CoreLib]], System.Private.CoreLib\",\"$values\":[{\"Key\":\"Világos\",\"Value\":0},{\"Key\":\"Sötét\",\"Value\":1}]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.EditorFooterViewModel, FrontEditor.Client\",\"CopyTextExtended\":\"Kacsás példa honlap, minden jog fenntartva!\",\"CopyTextAlign\":0,\"ContainerMode\":true,\"ColorSheme\":1,\"TextColor\":\"#ffffff\",\"BackgroundColor\":\"#2e4967\",\"MenuItemsAlign\":1,\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[{\"$type\":\"FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client\",\"Title\":\"Impresszum\",\"Icon\":null,\"Href\":\"#\",\"TargetBlank\":false,\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}},{\"$type\":\"FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client\",\"Title\":\"Adatvédelmi irányelvek\",\"Icon\":null,\"Href\":\"#\",\"TargetBlank\":false,\"MenuItems\":{\"$type\":\"System.Collections.Generic.List`1[[FrontEditor.Client.Models.EditorModels.MenuItem, FrontEditor.Client]], System.Private.CoreLib\",\"$values\":[]}}]},\"ViewName\":\"_footerEditor\",\"ProjectId\":1,\"ComponentName\":\"Lábrész\",\"ComponentId\":\"project-footer\",\"ColorShemes\":{\"$type\":\"System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, System.Private.CoreLib],[FrontEditor.Client.Models.EditorModels.ColorShemeEnum, FrontEditor.Client]], System.Private.CoreLib]], System.Private.CoreLib\",\"$values\":[{\"Key\":\"Világos\",\"Value\":0},{\"Key\":\"Sötét\",\"Value\":1}]}}]}}", 1 });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Category", "CreateTime", "Description", "ExportCount", "LastEdit", "OwnerId", "Status", "Title" },
                values: new object[] { 1, "Példa", new DateTime(2021, 11, 25, 16, 34, 40, 294, DateTimeKind.Local).AddTicks(2521), "Ez egy példa projekt.", 0, new DateTime(2021, 11, 25, 16, 34, 40, 294, DateTimeKind.Local).AddTicks(2842), null, 1, "Példa projekt" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "e22d43c3-ebd7-4468-82e1-a92a773e50f5", "Admin", "ADMIN" },
                    { 2, "0fc5ed8b-c898-4e69-aa61-d2a8bd49035d", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "LastActive", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "ProfileImage", "Registration", "SecurityStamp", "UserName" },
                values: new object[] { 1, "Admin", "admin@admin.hu", new DateTime(2021, 11, 25, 16, 34, 40, 283, DateTimeKind.Local).AddTicks(2361), "ADMIN@ADMIN.HU", "ADMIN", "AQAAAAEAACcQAAAAEMRX4lfz+F4jN8tq4Fs8lgfzi0e+jO9aMKw+IY0xV8UiuBdfa0v8znoKDtz0N8JpeA==", null, new DateTime(2021, 11, 25, 16, 34, 40, 285, DateTimeKind.Local).AddTicks(4713), "", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectJSONDatas");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
