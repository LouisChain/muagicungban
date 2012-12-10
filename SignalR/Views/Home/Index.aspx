<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

>
<script type="text/javascript">
    $(function () {
        // Proxy created on the fly          
        var chat = $.connection.chat;

        // Declare a function on the chat hub so the server can invoke it          
        chat.client.addMessage = function (message) {

            //$('#messages').append('<li>' + message + '</li>');
            alert("hello from server");
        };

        $("#broadcast").click(function () {
            // Call the chat method on the server
            chat.server.send($('#msg').val());
        });

        // Start the connection
        $.connection.hub.start();
    });
</script>
  
  <div>
    <input type="text" id="msg" />
<input type="button" id="broadcast" value="broadcast" />

<ul id="messages">
</ul>
  </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
