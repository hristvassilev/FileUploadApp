namespace FileUploadApiTests.TestData
{
    internal class TestData
	{
		public const string XmlContent = @"<RestToken xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <Configuration>Box</Configuration>
  <Token>y9cGBvA+CQ2NDC+vR0XfFw5aZDShqJfx7LTwr/vs2y3uOrGwNq6cfaSiDYpp9uAqDC6RLxoAqrsh4PZgM+ldKlUKN626Nmhf</Token>
  <RefreshToken>t+Zta76keWbLrT3Jg2MNvKxzGLpBvUlFgEfCmvxNN3x4KNQNn4ip/7Kp7qC3tlA/x1b+Z2d+sN2uyfcw2CJLOEcsBDDF6eIFXLysM4ZtDoUzkERDNeQuTKQQRCsjP66ydf1xbkCOzy6CCwaD+obToolZYeseU6enMykQDtecKJakUVJeDf8Mtg==</RefreshToken>
  <Expiration>2023-05-30T07:59:51.621Z</Expiration>
  <Parameters>
    <item>
      <key>
        <string>client_id</string>
      </key>
      <value>
        <string />
      </value>
    </item>
    <item>
      <key>
        <string>client_secret</string>
      </key>
      <value>
        <string>Qhuqja7JXQc=</string>
      </value>
    </item>
    <item>
      <key>
        <string>redirect_uri</string>
      </key>
      <value>
        <string>https://www.cozyroc.com/oauth_callback</string>
      </value>
    </item>
    <item>
      <key>
        <string>scope</string>
      </key>
      <value>
        <string>root_readwrite</string>
      </value>
    </item>
  </Parameters>
</RestToken>";

		public const string TextContent = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
									Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type
									and scrambled it to make a type specimen book.";
	}
}
