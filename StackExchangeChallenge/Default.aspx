<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                <%--To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.--%>
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Title of the question with the highest score</h5>
            <br />
            Question ID:
            <br /> 
            <asp:Label ID="lblQuestionId" runat="server" ></asp:Label>
            <br />
            Score: <asp:Label ID="lblScore" runat="server"></asp:Label>
            <br />
            Title:
            <br />
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
            <br />
        </li>
        <li class="two">
            <h5>Sum of the reputation for all users in that request</h5>
            <br /> 
            <asp:Label ID="txtRepScore" runat="server" ></asp:Label>
            <br />            
        </li>
        <li class="three">
            <h5>User with the most reputation</h5>
            <br /> 
            Name:
            <br /> 
            <asp:Label ID="lblRepName" runat="server" ></asp:Label>
            <br />
            Reputation: 
            <br />
            <asp:Label ID="lblRepScore" runat="server"></asp:Label>            
            <br />
            
        </li>
        <li class="four">
            <h5>List of all of the questions</h5>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
                <Columns>
                   <asp:BoundField DataField="Gravitar" HeaderText="Gravitar" ReadOnly="True" SortExpression="Gravitar" />
                    <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" SortExpression="Title" />
                    <asp:BoundField DataField="Link" HeaderText="Link" ReadOnly="True" SortExpression="Link" />
                    <asp:BoundField DataField="AnswerCount" HeaderText="AnswerCount" ReadOnly="True" SortExpression="AnswerCount" />
                    <asp:BoundField DataField="Score" HeaderText="Score" ReadOnly="True" SortExpression="Score" />
                    
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllQuestions" TypeName="WebApplication2.StackApi"></asp:ObjectDataSource>
        </li>
        
       
    </ol>
</asp:Content>
