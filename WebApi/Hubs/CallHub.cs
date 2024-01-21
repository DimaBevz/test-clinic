using Application.Chat.Commands;
using Application.Chat.DTOs.RequestDTOs;
using Application.Common.Interfaces.Hubs;
using Application.Common.Interfaces.Services;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs;

[Authorize]
public class CallHub : Hub<ICallClient>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    public CallHub(ICurrentUserService currentUserService, IMediator mediator)
    {
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    public async Task JoinCall(Guid sessionId, string callId)
    {
        var groupName = sessionId.ToString();

        Context.Items.TryGetValue("GroupName", out var currentSessionId);

        if (currentSessionId?.ToString() == sessionId.ToString())
        {
            await Clients.OthersInGroup(groupName).SendCallId(callId);
            
            return;
        }
        
        if (currentSessionId is not null)
        {
            Context.Items.Remove("GroupName");
        }
        
        Context.Items.Add("GroupName", groupName);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.OthersInGroup(groupName).SendCallId(callId);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Context.Items.TryGetValue("GroupName", out var currentSessionId);

        if (currentSessionId is not null)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, currentSessionId.ToString());
            
            Context.Items.Remove("GroupName");
        }

        return base.OnDisconnectedAsync(exception);
    }
}
