using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public static class HtmlHelpers
{
    private class ScriptBlock : IDisposable
    {
        private const string scriptsKey = "scripts";
        public static List<string> pageScripts
        {
            get
            {
                if (HttpContext.Current.Items[scriptsKey] == null)
                    HttpContext.Current.Items[scriptsKey] = new List<string>();
                return (List<string>)HttpContext.Current.Items[scriptsKey];
            }
        }

        WebViewPage webPageBase;

        public ScriptBlock(WebViewPage webPageBase)
        {
            this.webPageBase = webPageBase;
            this.webPageBase.OutputStack.Push(new StringWriter());
        }

        public void Dispose()
        {
            pageScripts.Add(((StringWriter)this.webPageBase.OutputStack.Pop()).ToString());
        }
    }

    private class ExternalScriptBlock : IDisposable
    {
        private const string externalScriptsKey = "externalscripts";

        public static List<string> pageExternalScripts
        {
            get
            {
                if (HttpContext.Current.Items[externalScriptsKey] == null)
                    HttpContext.Current.Items[externalScriptsKey] = new List<string>();
                return (List<string>)HttpContext.Current.Items[externalScriptsKey];
            }
        }

        WebViewPage externalWebPageBase;

        public ExternalScriptBlock(WebViewPage externalWebPageBase)
        {
            this.externalWebPageBase = externalWebPageBase;
            this.externalWebPageBase.OutputStack.Push(new StringWriter());
        }

        public void Dispose()
        {
            pageExternalScripts.Add(((StringWriter)this.externalWebPageBase.OutputStack.Pop()).ToString());
        }
    }

    public static IDisposable BeginScripts(this HtmlHelper helper)
    {
        return new ScriptBlock((WebViewPage)helper.ViewDataContainer);
    }

    public static MvcHtmlString PageScripts(this HtmlHelper helper)
    {
        return MvcHtmlString.Create(string.Join(Environment.NewLine, ScriptBlock.pageScripts.Select(s => s.ToString())));
    }

    public static IDisposable BeginExternalScripts(this HtmlHelper helper)
    {
        return new ExternalScriptBlock((WebViewPage)helper.ViewDataContainer);
    }

    public static MvcHtmlString PageExternalScripts(this HtmlHelper helper)
    {
        return MvcHtmlString.Create(string.Join(Environment.NewLine, ExternalScriptBlock.pageExternalScripts.Select(s => s.ToString())));
    }

    public static MvcHtmlString WriteLanguage(this HtmlHelper helper, String phrase, params string[] words)
    {
        return MvcHtmlString.Create(String.Format(phrase, words));
    }
}
