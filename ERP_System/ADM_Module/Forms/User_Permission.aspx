<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="User_Permission.aspx.cs" Inherits="ERP_System.ADM_Module.Forms.User_Permission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">

                <div class="col-lg-12">
                    <h1 class="page-header">User Privilege</h1>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">

                                <div class="row">

                                    <div class="col-lg-6">
                                        <%--  Department --%>
                                        <div class="form-group">
                                            <label>Company Code</label>
                                            <%--   <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>--%>
                                            <asp:DropDownList ID="ddlcom" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcom_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%--    </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                        </div>

                                        <%--search button--%>
                                        <div class="form-group">

                                            <asp:Button ID="btnsearch" Text="Filter" runat="server" CssClass="btn btn-default" OnClick="btnsearch_Click" />

                                        </div>


                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="resulttimer" runat="server" Interval="3000" Enabled="False" OnTick="resulttimer_Tick"></asp:Timer>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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

                                    <div class="col-lg-6">

                                        <%-- --%>
                                        <div class="form-group">
                                            <label>User ID</label>
                                            <asp:TextBox CssClass="form-control" ID="txtuser" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtuser"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetUser">
                                            </ajax:AutoCompleteExtender>
                                        </div>






                                    </div>
                                </div>

                                <%--tab title--%>
                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Header" data-toggle="tab">User Privilege Settings</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">User Privilege Refer </a>
                                    </li>
                                </ul>

                                <div class="tab-content">
                                    <div class="tab-pane fade in active" id="Header">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="panel panel-default">

                                                    <div class="panel-body">

                                                        <div class="row">

                                                            <div class="col-lg-6">

                                                                <div class="form-group">
                                                                    <label>1st Level Module</label>
                                                                    <asp:DropDownList ID="ddlfirst" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlfirst_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label>3rd Level Module</label>
                                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:DropDownList ID="ddlthird" CssClass="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="ddlsecond" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlfirst" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Button ID="btnsave" Text="Save" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
                                                                </div>


                                                            </div>

                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                    <label>2nd Level Module</label>
                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlsecond" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsecond_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlfirst" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="panel-body" style="overflow: auto;">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>

                                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="dgvheader" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="dgvheader_RowDeleting" DataKeyNames="id">
                                                                            <Columns>


                                                                                <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                                                                <asp:BoundField DataField="module_code" HeaderText="First Module" />
                                                                                <asp:BoundField DataField="second_module_code" HeaderText="Second Module" />
                                                                                <asp:BoundField DataField="third_module" HeaderText="Third Module" />


                                                                            </Columns>

                                                                        </asp:GridView>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnsearch" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </div>

                                                </div>

                                                <!-- /.table-responsive -->

                                            </div>

                                            <!-- /.panel-body -->
                                        </div>
                                    </div>

                                    <div class="tab-pane fade" id="Details">

                                        <div class="row">

                                            <div class="col-lg-12">
                                                <div class="panel panel-default">

                                                    <div class="panel-body">

                                                        <div class="row">

                                                            <div class="col-lg-6">

                                                                <div class="form-group">
                                                                    <label>Refer User Company</label>
                                                                    <%--   <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>--%>
                                                                    <asp:DropDownList ID="ddl_r_com" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_r_com_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>



                                                                <div class="form-group">
                                                                    <asp:Button ID="btn_r_search" Text="Search" runat="server" CssClass="btn btn-primary" OnClick="btn_r_search_Click" />
                                                                    <asp:Button ID="btnrefer" Text="Refer this user" runat="server" CssClass="btn btn-primary" OnClick="btnrefer_Click" />

                                                                </div>



                                                            </div>

                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                    <label>Refer User ID</label>
                                                                    <asp:TextBox CssClass="form-control" ID="txt_r_user" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txt_r_user"
                                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetRUser">
                                                                    </ajax:AutoCompleteExtender>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="panel-body" style="overflow: auto;">
                                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                            <ContentTemplate>

                                                                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="dgvrefer" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%">
                                                                            <Columns>



                                                                                <asp:BoundField DataField="module_code" HeaderText="First Module" />
                                                                                <asp:BoundField DataField="second_module_code" HeaderText="Second Module" />
                                                                                <asp:BoundField DataField="third_module" HeaderText="Third Module" />


                                                                            </Columns>

                                                                        </asp:GridView>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btn_r_search" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnrefer" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </div>

                                                </div>

                                                <!-- /.table-responsive -->

                                            </div>


                                            <!-- /.panel-body -->
                                        </div>


                                        <!-- /.panel -->
                                    </div>
                                    <!-- /.col-lg-12 -->
                                </div>

                            </div>
                        </div>
                    </div>

                    <%--tab with gridview for header and details--%>



                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
            </div>
        </ContentTemplate>
        <%--         <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                </Triggers>          --%>
    </asp:UpdatePanel>
</asp:Content>
