using System.Net.Http.Json;
using System.Text.Json;
using Application.Call.DTOs;
using Application.Common.Interfaces.Services;
using Infrastructure.Dtos.Call;
using Infrastructure.Dtos.Call.Common;
using Infrastructure.Extensions;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Infrastructure.Services;

public class CallService : ICallService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public CallService(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = true
        };
    }

    public async Task<string?> GetCallToken(CreateCallDto callData)
    {
        var requestBody = new AddParticipantToMeetingRequest
        {
            Name = callData.Name,
            Picture = callData.Picture ?? "https://i.imgur.com/test.jpg",
            PresetName = callData.IsHost ? "group_call_host" : "group_call_participant",
            CustomParticipantId = callData.UserId,
        };

        var data = JsonSerializer.Serialize(requestBody, _options);
        
        var request = new HttpRequestMessage(HttpMethod.Post,$"/v2/meetings/{callData.MeetingId}/participants")
        {
            Content = new StringContent(data)
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        var body = await response.Content.ReadFromJsonAsync<AddParticipantToMeetingResponse>(_options);
        
        return body?.Data?.Token;
    }

    public async Task<string?> CreateMeeting(DateTime sessionStartTime)
    {
        var requestBody = new AddMeetingRequest
        {
            Title = "string",
            PreferredRegion = "eu-central-1",
            RecordOnStart = false,
            LiveStreamOnStart = false,
            RecordingConfig = new RecordingConfig().Default()
        };

        var data = JsonSerializer.Serialize(requestBody, _options);

        var request = new HttpRequestMessage(HttpMethod.Post, "/v2/meetings")
        {
            Content = new StringContent(data)
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }

            }
        };

        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        var body = await response.Content.ReadFromJsonAsync<AddMeetingResponse>(_options);

        return body?.Data?.Id;
    }
}
