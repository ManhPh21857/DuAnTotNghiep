using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Services;

public class SendMailRequest : ICommand<SendMailResponse>
{
    public IEnumerable<string> SendTo { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public SendMailRequest(IEnumerable<string> sendTo, string title, string content)
    {
        this.SendTo = sendTo;
        this.Title = title;
        this.Content = content;
    }
}