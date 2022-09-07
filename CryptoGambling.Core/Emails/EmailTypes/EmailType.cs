using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSharp.Core.Emails.EmailTypes
{
    public static class EmailType
    {
        public static string WelcomeEmail(string url)
        {
            return @$"
<table cellpadding='0' cellspacing='0' class='sc-gPEVay eQYmiW' style='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial; min-width: 100%;'>
    <tbody>
        <tr>
            <td style='text-align: center;'><img src='https://i.ibb.co/g37BFfq/favicon.png' role='presentation' width='130' class='sc-cHGsZl bHiaRe' style='max-width: 130px; display: inline-block;border-radius:50%'></td>
        </tr>
        <tr>
            <td height='10'></td>
        </tr>
        <tr style='text-align: center;'>
            <td>
                <h1 color='#050000' class='sc-fBuWsC eeihxG' style='margin: 0px; font-size: 18px; color: rgb(5, 0, 0);'><span>SpaceSharp</span><span>&nbsp;</span><span></span></h1>
                <p color='#050000' font-size='medium' class='sc-fMiknA bxZCMx' style='margin: 0px; color: rgb(5, 0, 0); font-size: 14px; line-height: 22px;'><span>The Best Cryptocurrency Gambling Site Ever</span></p>
            </td>
        </tr>
        <tr style='text-align: center;'>
            <td>
                <h4>Please Verify Registration By Clicking On The Link Below:</h4>
                <a href='{url}' color='#050000' class='sc-gipzik iyhjGb' style='text-decoration: none; color:blue; font-size: 15px;'><span>{url}</span></a>
            </td>
        </tr>
        <tr>
            <td style='text-align: center;'>
                <br />
                © SpaceSharp.com All Rights Reserved
            </td>
        </tr>
    </tbody>
</table>
";
        }
    }
}
