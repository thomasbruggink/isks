using InfoSupport.Tessler.Core;

namespace UITests.Pages
{
    public class PageBase<TPage> : PageObject<TPage> where TPage : PageBase<TPage>
    {
    }
}
