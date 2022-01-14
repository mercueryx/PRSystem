<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="Create_User.aspx.cs" Inherits="ERP_System.ADM_Module.Forms.Create_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Create User</h1>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnsave" Text="SAVE" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
                                            <%--<asp:Button ID="btnnew" Text="NEW" runat="server" CssClass="btn btn-default" OnClick="btnnew_Click" />--%>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">

                                            <%--result message--%>
                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="resulttimer" runat="server" Interval="3000" Enabled="False" OnTick="resulttimer_Tick"></asp:Timer>
                                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                        <ContentTemplate>
                                                            <strong>
                                                                <asp:Label ID="lblsaveresult" runat="server" Text=""></asp:Label>
                                                            </strong>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="resulttimer" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>
                                    </div>
                                    <!-- /.row (nested) -->
                                </div>


                                <!-- Tab panes -->
                                <%--   <div class="tab-content">--%>

                                <%--  <div class="tab-pane fade in active" id="Entry">--%>

                                <div class="row">

                                    <div class="col-lg-6">

                                        <!-- /company -->
                                        <div class="form-group">
                                            <label>Company</label>
                                            <asp:DropDownList ID="ddlcom" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>

                                        <!-- /login id -->
                                        <div class="form-group">
                                            <label>Login ID (Emp No.)</label>
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>

                                                    <asp:TextBox CssClass="form-control" ID="txtempid" runat="server" ReadOnly="false"></asp:TextBox>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>


                                        <!-- /Password -->
                                        <div class="form-group">

                                            <label>Password</label>
                                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox CssClass="form-control" ID="txtpwd" runat="server" ReadOnly="false" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>



                                        </div>

                                        <!-- /Username -->
                                        <div class="form-group">
                                            <label>Username</label>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>

                                                    <asp:TextBox CssClass="form-control" ID="txtusn" runat="server" ReadOnly="false"></asp:TextBox>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>





                                    </div>

                                    <div class="col-lg-6">


                                        <!-- /Email -->
                                        <div class="form-group">
                                            <label>Email</label>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>

                                                    <asp:TextBox CssClass="form-control" ID="txtemail" runat="server" ReadOnly="false"></asp:TextBox>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>




                                        <!-- /deparment -->
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>

                                                    <asp:DropDownList ID="ddldpt" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddldpt_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>


                                        <!-- /section -->
                                        <div class="form-group">
                                            <label>Section</label>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddldpt" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>

                                    </div>


                                </div>


                                <%--                                    </div>--%>
                                <!-- /.col-lg-12 -->
                            </div>


                        </div>

                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->

            <!-- /.col-lg-12 -->
            </div>
            <!-- /.col-lg-12 -->
            </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnsave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
