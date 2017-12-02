<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>
  <xsl:template match="/">
    <html>
      <head>
        <style type="text/css">
          h2
          {
          color: #333333;
          font-family: verdana;
          font-size: 18pt;
          text-align: left;
          text-decoration: none;
          font-weight: bold;
          }

          h4
          {
          color: #333333;
          font-family: verdana;
          font-size: 12pt;
          text-align: left;
          text-decoration: none;
          font-weight: normal;
          }

          td
          {
          vertical-align: top;
          }

          .label
          {
          color: #333333;
          font-family: verdana;
          font-size: 10pt;
          text-align: left;
          text-decoration: none;
          font-weight: bold;
          }

          .value
          {
          color: #333333;
          font-family: verdana;
          font-size: 10pt;
          text-align: left;
          text-decoration: none;
          font-weight: normal;
          }
        </style>
      </head>
      <body>
        <xsl:for-each select="data">
          <h2>
            Space Invasion Game Data
          </h2>
          <h4>
            Created: <xsl:value-of select="@created"/>
          </h4>
        </xsl:for-each>

        <xsl:for-each select="data/entry">
          <hr />
          <div>
            <table cellpadding="5px" cellspacing="0px" border="0px">
              <tr>
                <td>
                  <span class="label">Time:</span>
                </td>
                <td>
                  <span class="value">
                    <xsl:value-of select="@time"/>
                  </span>
                </td>
              </tr>
              <tr>
                <td>
                  <span class="label">Score:</span>
                </td>
                <td>
                  <span class="value">
                    <xsl:value-of select="@score"/>
                  </span>
                </td>
              </tr>
              <tr>
                <td>
                  <span class="label">Lives:</span>
                </td>
                <td>
                  <span class="value">
                    <xsl:value-of select="@lives"/>
                  </span>
                </td>
              </tr>
              <tr>
                <td>
                  <span class="label">Level:</span>
                </td>
                <td>
                  <span class="value">
                    <xsl:value-of select="@level"/>
                  </span>
                </td>
              </tr>
              <tr>
                <td>
                  <span class="label">Hits:</span>
                </td>
                <td>
                  <span class="value">
                    <xsl:value-of select="@hits"/>
                  </span>
                </td>
              </tr>
              <tr>
                <td>
                  <span class="label">Misses:</span>
                </td>
                <td>
                  <span class="value">
                    <xsl:value-of select="@misses"/>
                  </span>
                </td>
              </tr>
              <tr>
                <td>
                  <span class="label">Hit Percent:</span>
                </td>
                <td>
                  <span class="value">
                    <xsl:value-of select="@hitPercent"/>
                  </span>
                </td>
              </tr>
            </table>
          </div>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
