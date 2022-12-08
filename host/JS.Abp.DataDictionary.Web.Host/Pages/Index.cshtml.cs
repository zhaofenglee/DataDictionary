using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace JS.Abp.DataDictionary.Pages;

public class IndexModel : DataDictionaryPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
