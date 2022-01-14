<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ERP_System.index" %>
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    	 <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Login</title>

    <!-- Bootstrap Core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="vendor/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="dist/css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

</head>
<body>
    <%--<form id="form1" runat="server">--%>
   
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Please Sign In</h3>
                    </div>
                    <div class="panel-body">
                        <form role="form" id="form1" runat="server">
                             <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                            <fieldset>
                                <div class="form-group">
                                    <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" p></asp:TextBox>--%>
                                    <input class="form-control" id="txtloginid" runat="server" placeholder="Login ID" name="Loginid" type="text" autocomplete="off" />
                                </div>
                                <div class="form-group">
                                    <input class="form-control" id="txtpwd" runat="server" placeholder="Password" name="password" type="password" value=""/>
                                </div>
                                  <div class="form-group">
                                    <label>Company.</label>
                                                    <asp:DropDownList ID="ddlcom" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                </div>
                                  <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="resulttimer" runat="server" Interval="3000" Enabled="False" OnTick="resulttimer_Tick"></asp:Timer>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <strong>
                                                                <asp:Label ID="lblsaveresult" runat="server" Text=""></asp:Label></strong>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="resulttimer" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnlogin" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>
                               
                                <asp:Button ID="btnlogin" runat="server" Text="Login" CssClass="btn btn-lg btn-success btn-block" OnClick="btnlogin_Click"/>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="dist/js/sb-admin-2.js"></script>
  <%--  </form>--%>
</body>
</html>
