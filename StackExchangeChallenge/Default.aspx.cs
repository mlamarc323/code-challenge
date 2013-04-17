using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StackApi client = new StackApi();
            Item highestScore = client.GetHighestScore();

            lblQuestionId.Text = highestScore.Id.ToString();
            lblScore.Text = highestScore.Score.ToString();
            lblTitle.Text = highestScore.Title;

            string repSum = client.GetReputationSum();
            txtRepScore.Text = repSum;

            Item userHighestRep = client.GetHighestReputationUser();
            lblRepName.Text = userHighestRep.Name;
            lblRepScore.Text = userHighestRep.Reputation.ToString();

            //IEnumerable<Item> items = client.GetAllItems();
        }
    }
}