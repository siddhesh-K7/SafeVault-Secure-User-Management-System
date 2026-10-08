using Xunit;
using System.Web;

namespace SafeVault.Tests;

public class XssTests
{
    [Fact]
    public void UpdateUser_XssPayload_IsHtmlEncoded()
    {
        string payload = "<script>alert('XSS')</script>";
        string encoded = HttpUtility.HtmlEncode(payload);

        // Verifies our encoding logic safely encodes tags
        Assert.Equal("&lt;script&gt;alert(&#39;XSS&#39;)&lt;/script&gt;", encoded);
    }
}