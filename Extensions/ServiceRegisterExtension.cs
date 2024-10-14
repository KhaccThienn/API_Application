namespace API_Application.Extensions
{
    public static class ServiceRegisterExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("AllowOrigin", p =>
                {
                    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<UserMemory>();
            services.AddSingleton<GenreMemory>();
            services.AddSingleton<ActorMemory>();
            services.AddSingleton<DirectorMemory>();
            services.AddSingleton<ComicMemory>();
            services.AddSingleton<EpisodeMemory>();
            services.AddSingleton<ImagesMemory>();
            services.AddSingleton<ReviewMemory>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();

            services.AddHttpContextAccessor();
        }
    }
}
