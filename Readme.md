ARCHITECTURE
For scalability purposes microservice architecture is used here. For simple applications monolith architecture is known to be good enough but scalability is not an option with this approach. Also the services are loosely coupled in monolithic architectural style. 

API gateway microservice pattern is used for performing API composition of Pokemon API and translation API.

For each service I tried to use onion architecture with DDD approach. MediatR pattern is used to make layers less coupled.

I have implemented two APIs:

1. Pokemon restful CRUD API, endpoints:
/HTTP/GET/Pokemon/<pokemon id>
/HTTP/GET/Pokemon/byname/<pokemon name>
/HTTP/Post/Pokemon/<pokemon id>
/HTTP/Put/Pokemon/<pokemon id>
/HTTP/DELETE/Pokemon/<pokemon id>

CACHE
In memory cache with cache strategy write-through is used here. With this approach data that is to be written or updated onto the database is also updated on the cache. Cache can be outdated for a very short period of time.

DATABASE
In memory db is used here.

UNIT TESTS
Pokemon API is not covered by tests(but API gateway is covered by tests).

2. API gateway, endpoints:
/HTTP/GET/Pokemon/byname/<pokemon name>
/HTTP/GET/Pokemon/translated/<pokemon name>

CACHE
Cache was not implemented.

PERFORMANCE AND SCALABILITY
There are two approaches: synchronous and asynchronous I/O model. In the synchronous I/O model, each network connection is handled by a dedicated
thread. Operating system threads are heavyweight, so there is a limit on the number of threads, and hence concurrent connections, that an API gateway can have. Nonblocking I/O is much more scalable. So asynchronous I/O model was implemented for the purpose of performance and scalability.

HANDLING PARTIAL FAILURES
When an API gateway invokes a service, there’s always a chance that the service is slow or unavailable. An API
gateway may wait a very long time, perhaps indefinitely, for a response, which consumes resources and prevents it from sending a response to its client. Timeout was implemented in order to break long requests.


•••••••••••


What would be done different for production:

CACHE
Distributed cache such as Redis etc would be used instead of in-memory cache (for web farm scenarios). 
Translation API seems to have static rules, so we can cache API responses. Etag cache can be implemented for caching on client side.

EDGE FUNCTIONS
Edge functions such as caching, metrics collection, rate limiting can be added to API gateway. Also API key for accessing APIs can be implemented.

CIRCUIT BREAKER
A Circuit breaker pattern would be used for invoking services in API gateway.

SERVICE DISCOVERY
Service discovery pattern would be used to determine the network location of a service instance.

LOAD BALANCER
Although multiple instances of the API gateway and Pokemon API can be run behind a load balancer service for translation can be the slowest place.

API VERSION
API requirements can change over time so API version /HTTP/GET/v1/Pokemon/ would be used.

CORS
A strict CORS policy would be specified for production.

DB
In memory db would be replaced with relational or non-relational db for Pokemon API.


HOW TO RUN DOCKER CONTAINERS
1 Check .netcoreapp5.0 platform is installed
2 Check docker desktop is installed
3 Open PowerShell in the PokemonGame directory and run next commands step by step:
dotnet dev-certs https -ep .\devcert\aspnetcore.pfx -p password
dotnet dev-certs https --trust
docker compose build
docker-compose up
4 Open additional PowerShell instance and run next commands:
Start-Process "https://localhost:8001/index.html"
Start-Process "https://localhost:9001/index.html"

Please note, if you would like to run under API under, you will need to specify URL for accessing Pokémon API in PokemonService\PokemonService.WebApi\appsettings.json, item PokemonApi