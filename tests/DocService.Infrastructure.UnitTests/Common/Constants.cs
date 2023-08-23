namespace DocService.Infrastructure.UnitTests.Common
{
    public static class Constants
    {
        public const string XmlContent = 
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
              <note>
                <to>Tove</to>
                <from>Jani</from>
                <heading>Reminder</heading>
                <body>Don't forget me this weekend!</body>
              </note> ";

        public const string JsonContent =
            @"{
                ""to"": ""Tove"",
                ""from"": ""Jani"",
                ""heading"": ""Reminder"",
                ""body"": ""Don't forget me this weekend!""
              }";
    }
}
