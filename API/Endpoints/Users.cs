using Application.Users.Create;
using Application.Users.Delete;
using Application.Users.Get;
using Application.Users.Update;
using Carter;
using Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Endpoints
{
    public class Users : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder routes)
        {
            routes.MapPost("Users/", async (CreateUserCommand command
                 , ISender sender) =>
            {

                await sender.Send(command);

                return Results.Ok();
            });

            routes.MapDelete("Users/{id:guid}", async (Guid Id, ISender sender) =>
            {
                try
                {
                    await sender.Send(new DeleteUserCommand(Id));

                    return Results.NoContent();
                }
                catch (UserNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }

            });

            routes.MapPut("Users/{id:guid}", async (Guid id, [FromBody] UpdateUserRequest request, ISender sender) =>
            {
                var command = new UpdateUserCommand(id, request.name, request.Email);
                await sender.Send(command);

                return Results.NoContent();
            });

            routes.MapGet("Users/{id:guid}", async (Guid id, ISender sender) =>
            {
                try
                {
                    return Results.Ok(await sender.Send(new GetUserQuery(id)));
                }
                catch (UserNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            });
            routes.MapGet("Users/", async (ISender sender) =>
            {
                try
                {
                    return Results.Ok(await sender.Send(new GetAllUsersQuery()));
                }
                catch (NoUserFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            });
        }
    }
}
