using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Services;

public class SendMailRequest : ICommand<SendMailResponse>
{
    public List<string> SendTo { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public SendMailRequest(List<string> sendTo, string title, string content)
    {
        SendTo = sendTo;
        Title = title;
        Content = content;
    }
}