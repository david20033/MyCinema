using MyCinema.Data.MovieApi.Responses;
using MyCinema.Data.MovieApi;
using Newtonsoft.Json;
using RestSharp;
using MyCinema.Services.IServices;

namespace MyCinema.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<List<MovieNowPlayingDTO>> GetNowPlayingMoviesAsync(int page)
        {
            var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/now_playing?language=en-US&page={page}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            string bearerToken = _configuration["ApiSettings:BearerToken"];
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", bearerToken);
            var response = await client.GetAsync(request);

            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Failed to fetch now playing movies.");
            }


            var movieResponse = JsonConvert.DeserializeObject<MovieNowPlayingResponseDTO>(response.Content);

            return movieResponse?.results;

        }
        public async Task<List<LanguageDTO>> GetLanguagesAsync()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/configuration/languages");
            var client = new RestClient(options);
            var request = new RestRequest("");
            string bearerToken = _configuration["ApiSettings:BearerToken"];
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", bearerToken);
            var response = await client.GetAsync(request);

            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Failed to fetch languages.");
            }


            return JsonConvert.DeserializeObject<List<LanguageDTO>>(response.Content);
        }
        public async Task<List<GenreDTO>> GetGenresAsync()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/genre/movie/list?language=en");
            var client = new RestClient(options);
            var request = new RestRequest("");
            string bearerToken = _configuration["ApiSettings:BearerToken"];
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", bearerToken);
            var response = await client.GetAsync(request);

            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception("Failed to fetch languages.");
            }
            var genreResponse = JsonConvert.DeserializeObject<GenreResponseDTO>(response.Content);
            return genreResponse?.genres;
        }
    }
}
