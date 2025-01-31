using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc; // For [FromBody]
using BusinessLayer;
using Entities;
namespace MyProject.APIEndpoints
{
    public static class UserApiEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("/api/users", async (IUsers repository) =>
            {
                List<UserDomainModel> users = await repository.GetUsers();
                return Results.Ok(users);
            });

            builder.MapGet("/api/users/{id}", async (int id, IUsers repository) =>
            {
                var user = await repository.GetUsersByid(id);
                return user != null ? Results.Ok(user) : Results.NotFound();
            });

            builder.MapPost("/api/users", async (UserDomainModel user, IUsers repository) =>
            {
                var userId = await repository.insert(user);
                return Results.Created($"/api/users/{userId}", userId);
            });

            builder.MapPut("/api/users", async (UserDomainModel user, IUsers repository) =>
            {
                var updated = await repository.update(user);
                return updated ? Results.Ok() : Results.NotFound();
            });

            builder.MapDelete("/api/users/{id}", async (int id, IUsers repository) =>
            {
                var deleted = await repository.delete(id);
                return deleted != null ? Results.Ok(deleted) : Results.NotFound();
            });

            return builder;
        }
    }
}
