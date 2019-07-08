using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BcmmOja.Models;

namespace BcmmOja.Utility
{
    public class TemplateGenerator
    {
        private readonly bcmm_ojaContext _context = new bcmm_ojaContext();

        public static string GetHTMLString()
        {
            var applicants = new bcmm_ojaContext().Applicant;
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>
                                        <th>LastName</th>
                                        <th>ID</th>
                                        <th>Title</th>
                                    </tr>");

            foreach (var a in applicants)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", a.FirstName, a.LastName, a.IdNumber, a.Title);
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
