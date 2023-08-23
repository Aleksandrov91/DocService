namespace DocService.Application.UnitTests.Common
{
    public static class Constants
    {
        public const string ValidXmlContent = 
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
              <note>
                <to>Tove</to>
                <from>Jani</from>
                <heading>Reminder</heading>
                <body>Don't forget me this weekend!</body>
              </note> ";

        public const string InvalidXmlContent =
            @"encoding=""UTF-8""?>
              <note>
                <to>Tove</to>
                <from>Jani</from>
                <heing>Reminder</heading>
                <body>Don't forget me this weekend!</body>
              </note> ";
    }
}
