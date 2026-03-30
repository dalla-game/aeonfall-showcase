using System.Net.Http.Json;
using BlazorApp.Models;

namespace BlazorApp.Services;

public sealed class MemberService : IDisposable
{
    private readonly HttpClient _client;
    private readonly Task<List<Member>?> _getMembersTask;

    public MemberService(HttpClient client)
    {
        _client = client;
        _getMembersTask =
            _client.GetFromJsonAsync<List<Member>>(
                "sample-data/members.json");
    }

    internal async Task<Member?> GetAsync(Func<Member, bool> predicate)
    {
        var members = await _getMembersTask;
        return members?.FirstOrDefault(predicate);
    }
    internal async Task<List<Member>?> GetListAsync(Func<Member, bool> predicate)
    {
        var members = await _getMembersTask;
        return members?.Where(predicate).ToList();
    }
    internal async Task<List<Member>?> GetListAsync()
    {
        var members = await _getMembersTask;
        return members?.ToList();
    }
    public void Dispose() => _client.Dispose();
}
