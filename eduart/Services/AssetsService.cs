using System;
using System.IO;

namespace eduart.Services;

public class AssetsService(string projectName, Func<Uri, Uri?, Stream> open)
{
    private string _projectName = projectName;
    private Func<Uri, Uri?, Stream> _openCallback = open;

    public Stream Open(string path)
    {
        var uri = new Uri("avares://" + UrlCombine(_projectName, path));
        return _openCallback(uri, null);
    }

    private static string UrlCombine(string a, string b) => a.TrimEnd('/') + "/" + b.TrimStart('/');
}