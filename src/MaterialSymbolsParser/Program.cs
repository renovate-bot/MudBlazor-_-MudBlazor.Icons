﻿using MaterialSymbolsParser.Extensions;
using MaterialSymbolsParser.Model;
using MaterialSymbolsParser.Service;

namespace MaterialSymbolsParser;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var iconType = IconType.MaterialIcons;
        var codeGenerator = new CodeGenerationService();
        using var client = new IconHttpClientService();
        var metadata = await client.ParseIconsAsync().ConfigureAwait(false);
        var filteredIcons = Utility.IconFilter.FilterByFamily(metadata, iconType);
        var groupedIcons = Utility.IconFilter.GroupIconsByFamilies(filteredIcons, iconType);

        codeGenerator.GenerateCsFilesUsingRoslyn(iconType, groupedIcons, folder: $"../../../../MudBlazor.FontIcons.{iconType.GetDescription()}");
    }
}