using Application.Chat.Commands;
using Application.Chat.DTOs.RequestDTOs;
using Application.Common.Interfaces.Hubs;
using Application.Common.Interfaces.Services;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    public ChatHub(ICurrentUserService currentUserService, IMediator mediator)
    {
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    public async Task JoinChat(Guid sessionId)
    {
        var groupName = sessionId.ToString();

        if (Context.Items.ContainsKey("GroupName"))
        {
            Context.Items.Remove("GroupName");
        }
        
        Context.Items.Add("GroupName", groupName);

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        await Clients.Group(groupName).ReceiveConnectionStatus($"{Context.ConnectionId} connected");
    }

    public async Task SendMessage(string message)
    {
        string? groupName = null;

        Context.Items.TryGetValue("GroupName", out object? value);
        groupName = (string?)value;
        
        if (groupName is not null)
        {
            var createdAt = DateTime.UtcNow;
            var sessionId = Guid.Parse(groupName);
            var userId = Guid.Parse(_currentUserService.UserId);
            
            var addMessageDto = new AddMessageDto(sessionId, userId, message, createdAt);

            var result = await _mediator.Send(new AddMessageCommand(addMessageDto));
            await Clients.Group(groupName).ReceiveMessage(result);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (Context.Items.ContainsKey("GroupName"))
        {
            Context.Items.Remove("GroupName");
        }

        return base.OnDisconnectedAsync(exception);
    }
}