
using System;
using System.Collections.Generic;
using FrontEditor.Client.BusinessLogic;
using FrontEditor.Client.Models.EditorModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontEditor.Client.Models.Entities
{
    public class FrontEditorContext : IdentityDbContext<User, Role, int>
    {
        private readonly DbContextOptions _options;
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectJSON> ProjectJSONDatas { get; set; }

        private static JsonParser<EditorModelData> parser = new JsonParser<EditorModelData>();

        public FrontEditorContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProjectJSON>(entity => entity.Property(m => m.ProjectData).HasColumnType("LONGTEXT"));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.Property(m => m.Name).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.ProviderDisplayName).HasMaxLength(128));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(128));

            modelBuilder.Entity<User>()
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.PhoneNumber)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.ConcurrencyStamp)
                .Ignore(c => c.TwoFactorEnabled);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserToken");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            seedData(modelBuilder);
        }
        private void seedData(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();
            User adminUser = new User()
            {
                Id = 1,
                DisplayName = "Admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.hu",
                NormalizedEmail = "ADMIN@ADMIN.HU",
                LastActive = DateTime.Now,
                Registration = DateTime.Now,
                PasswordHash = hasher.HashPassword(null, "Admin-2021"),
                SecurityStamp = string.Empty
            };
            Project project = new Project()
            {
                Id = 1,
                Title = "Példa projekt",
                Category = "Példa",
                CreateTime = DateTime.Now,
                LastEdit = DateTime.Now,
                Description = "Ez egy példa projekt.",
                ExportCount = 0,
                Status = Enums.ProjectStatus.Editable
            };
            ProjectJSON projectJSON = new ProjectJSON()
            {
                Id = 1,
                ProjectId = 1,
                ProjectData = parser.SerializeData(
                    new EditorModelData()
                    {
                        Title = "Példa projekt",
                        Description = "Ez egy példa projekt.",
                        Blocks = new List<EditorBaseViewModel> {
                           new EditorHeaderViewModel(1) {
                               BackgroundColor = "#2e4967",
                               ColorSheme = ColorShemeEnum.Dark,
                               ComponentId = "project-header",
                               ComponentName = "Menü",
                               ContainerMode = true,
                               FixedMenu = true,
                               Icon = "eye",
                               LogoImage = null,
                               MenuItems = new List<MenuItem>() {
                                   new MenuItem() { Title = "Főoldal", Href="#", Icon = "user", MenuItems = new List<MenuItem>(), TargetBlank = false},
                                   new MenuItem() { Title = "Wikipédia", Href="https://hu.wikipedia.org/", Icon = "book", MenuItems = new List<MenuItem>(), TargetBlank = true},
                                   new MenuItem() { Title = "Microsoft", Href="https://www.microsoft.com/hu-hu/", Icon ="globe", MenuItems = new List<MenuItem>() {
                                        new MenuItem() { Title = "Microsoft 365", Href="https://www.microsoft.com/hu-hu/microsoft-365", Icon = "at", MenuItems = new List<MenuItem>(), TargetBlank = true},
                                        new MenuItem() { Title = "Windows", Href="https://www.microsoft.com/hu-hu/windows/", Icon = "columns", MenuItems = new List<MenuItem>(), TargetBlank = true}
                                   }, TargetBlank = true}
                               },
                               MobileMenu = false,
                               ProjectId = 1,
                               ShowSearchBox = false,
                               Title = "Példa projekt"

                            },
                           new EditorCarouselViewModel(1) {
                             AnimationMode = SliderAnimationModeEnum.Fade,
                             AutoRun = true,
                             Captions = true,
                             CaptionsTextBackgroundColor = "#000000",
                             CaptionsTextColor = "#ffffff",
                             CarouselItems = new List<CarouselItem>() {
                                 new CarouselItem() { Title = "Kacsa", Image = "duck.jpg", Description = "A képen egy kacsa van." },
                                 new CarouselItem() { Title = "Gyerek kutyával", Image = "kid.jpg", Description = "A képen egy gyerek játszik egy kutyával." },
                                 new CarouselItem() { Title = "Hangulat", Image = "mood.jpg", Description = "A képen egy lankás táj látható." }
                             },
                             ComponentId = "carousel-637734425601321648",
                             ComponentName = "Carousel - 1",
                             ContainerMode = true,
                             Controls = true,
                             HeightSize = 500,
                             Indicators = true,
                             PageInterval = 3000,
                             ProjectId = 1
                           },
                           new EditorBlocksViewModel(1) {
                               BlockItems =  new List<BlockItem> () {
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = "anchor",
                                       ImageHref = null,
                                       Title = "Horgony",
                                       Content = "<center>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</center>",
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   },
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = "basketball-ball",
                                       ImageHref = null,
                                       Title = "Kosárlabda",
                                       Content = "<center>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</center>",
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   },
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = "bicycle",
                                       ImageHref = null,
                                       Title = "Kerékpár",
                                       Content = "<center>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</center>",
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   },
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = "birthday-cake",
                                       ImageHref = null,
                                       Title = "Torta",
                                       Content = "<center>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</center>",
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   }
                               },
                               ComponentId = "blocks-637734431908272422",
                               ComponentName = "Blocks - 1",
                               BackgroundColor = "#e4f1ff",
                               ContainerMode = true,
                               ItemsPerRow = 4,
                               ProjectId = 1
                           },
                           new EditorBlocksViewModel(1) {
                               BlockItems =  new List<BlockItem> () {
                                   new BlockItem() {
                                       BlockItemId = "diagram01",
                                       ChartRows =  new List<ChartRow>() {
                                           new ChartRow() {Label = "Red", Data = 12, Color = "#ff6384" },
                                           new ChartRow() {Label = "Blue", Data = 19, Color = "#36a2eb" },
                                           new ChartRow() {Label = "Yellow", Data = 3, Color = "#ffce56"},
                                           new ChartRow() {Label = "Green", Data = 5, Color = "#4abf6d" },
                                           new ChartRow() {Label = "Purple", Data = 2, Color = "#9966ff" },
                                           new ChartRow() {Label = "Orange", Data = 3, Color = "#ff9f40" }
                                       },
                                       ChartType = Enums.ChartType.Bar,
                                       IconName = null,
                                       ImageHref = null,
                                       Title = "Oszlop diagram",
                                       Content = null,
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   },
                                   new BlockItem() {
                                       BlockItemId = "diagram02",
                                       ChartRows =  new List<ChartRow>() {
                                           new ChartRow() {Label = "Red", Data = 12, Color = "#ff6384" },
                                           new ChartRow() {Label = "Blue", Data = 19, Color = "#36a2eb" },
                                           new ChartRow() {Label = "Yellow", Data = 3, Color = "#ffce56"},
                                           new ChartRow() {Label = "Green", Data = 5, Color = "#4abf6d" },
                                           new ChartRow() {Label = "Purple", Data = 2, Color = "#9966ff" },
                                           new ChartRow() {Label = "Orange", Data = 3, Color = "#ff9f40" }
                                       },
                                       ChartType = Enums.ChartType.Pie,
                                       IconName = null,
                                       ImageHref = null,
                                       Title = "Kör diagram",
                                       Content = null,
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   },
                                   new BlockItem() {
                                       BlockItemId = "diagram03",
                                       ChartRows =  new List<ChartRow>() {
                                           new ChartRow() {Label = "Red", Data = 12, Color = "#ff6384" },
                                           new ChartRow() {Label = "Blue", Data = 19, Color = "#36a2eb" },
                                           new ChartRow() {Label = "Yellow", Data = 3, Color = "#ffce56"},
                                           new ChartRow() {Label = "Green", Data = 5, Color = "#4abf6d" },
                                           new ChartRow() {Label = "Purple", Data = 2, Color = "#9966ff" },
                                           new ChartRow() {Label = "Orange", Data = 3, Color = "#ff9f40" }
                                       },
                                       ChartType = Enums.ChartType.Doughnut,
                                       IconName = null,
                                       ImageHref = null,
                                       Title = "Fánk diagram",
                                       Content = null,
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   }
                               },
                               ComponentId = "blocks-637734437347539272",
                               ComponentName = "Blocks - 2",
                               BackgroundColor = "#ffffff",
                               ContainerMode = true,
                               ItemsPerRow = 3,
                               ProjectId = 1
                           },
                           new EditorBlocksViewModel(1) {
                               BlockItems =  new List<BlockItem> () {
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = null,
                                       ImageHref = "duck.jpg",
                                       Title = "Házi Kacsa (Anas platyrhynchos domestica)",
                                       Content = "<center>A házi kacsa (Anas platyrhynchos domestica) a récefélék családjába tartozó baromfi, a tőkés réce (vadkacsa) alfaja, háziasított változata. Többnyire fehér színben tenyésztik, de egyes vidékeken – különösen ott, ahol vadon élő őseivel könnyen kereszteződhet – „vad” színezetű példányokat is találunk.</center>",
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   }
                               },
                               ComponentId = "blocks-637734445998390209",
                               ComponentName = "Blocks - 3",
                               BackgroundColor = "#e4f1ff",
                               ContainerMode = true,
                               ItemsPerRow = 1,
                               ProjectId = 1
                           },
                           new EditorBlocksViewModel(1) {
                               BlockItems =  new List<BlockItem> () {
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = null,
                                       ImageHref = null,
                                       Title = "Lorem Ipsum",
                                       Content = "<p><a href='https://www.lipsum.com/' target='_blank'>Lorem Ipsum</a> is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>\n<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>",
                                       TitleAlign = AlignEnum.Left,
                                       TitleColor = "#2e4967"
                                   }
                               },
                               ComponentId = "blocks-637734453652926379",
                               ComponentName = "Blocks - 4",
                               BackgroundColor = "#ffffff",
                               ContainerMode = true,
                               ItemsPerRow = 1,
                               ProjectId = 1
                           },
                           new EditorBlocksViewModel(1) {
                               BlockItems =  new List<BlockItem> () {
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = null,
                                       ImageHref = "duck.jpg",
                                       Title = "Kacsa",
                                       Content = null,
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   },
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = null,
                                       ImageHref = "kid.jpg",
                                       Title = "Kutya",
                                       Content = null,
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   },
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = null,
                                       ImageHref = "kid.jpg",
                                       Title = "Kutya",
                                       Content = null,
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   },
                                   new BlockItem() {
                                       BlockItemId = null,
                                       ChartRows = new List<ChartRow>(),
                                       ChartType = Enums.ChartType.None,
                                       IconName = null,
                                       ImageHref = "duck.jpg",
                                       Title = "Kacsa",
                                       Content = null,
                                       TitleAlign = AlignEnum.Center,
                                       TitleColor = "#2e4967"
                                   }
                               },
                               ComponentId = "blocks-637734453573369273",
                               ComponentName = "Blocks - 5",
                               BackgroundColor = "#e4f1ff",
                               ContainerMode = true,
                               ItemsPerRow = 2,
                               ProjectId = 1
                           },
                           new EditorFooterViewModel(1) {
                               ComponentId="project-footer",
                               ComponentName = "Lábrész",
                               TextColor = "#ffffff",
                               BackgroundColor = "#2e4967",
                               ColorSheme = ColorShemeEnum.Dark,
                               MenuItemsAlign = AlignEnum.Right,
                               ContainerMode = true,
                               CopyTextAlign = AlignEnum.Left,
                               CopyTextExtended = "Kacsás példa honlap, minden jog fenntartva!",
                               MenuItems = new List<MenuItem>() {
                                   new MenuItem() { Href = "#", Icon = null, MenuItems = new List<MenuItem>(), TargetBlank = false, Title = "Impresszum" },
                                   new MenuItem() { Href = "#", Icon = null, MenuItems = new List<MenuItem>(), TargetBlank = false, Title = "Adatvédelmi irányelvek" },
                               },
                               ProjectId = 1
                            }
                        },
                        CustomCss = "body {\n  color: #436993;\n}"
                    })
            };

            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 2,
                Name = "User",
                NormalizedName = "USER"
            });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>()
            {
                RoleId = 1,
                UserId = 1
            });
            modelBuilder.Entity<Project>().HasData(project);
            modelBuilder.Entity<ProjectJSON>().HasData(projectJSON);
        }
    }
}