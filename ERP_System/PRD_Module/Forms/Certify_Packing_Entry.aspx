<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="Certify_Packing_Entry.aspx.cs" Inherits="ERP_System.PRD_Module.Forms.Certify_Packing_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">

                <div class="col-lg-12">
                    <h1 class="page-header">Certify Packing Entry</h1>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">

                                <div class="row">

                                    <div class="col-lg-6">
                                        <%--  brand code --%>
                                        <div class="form-group">
                                            <label>Brand Code</label>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlbrand" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <%--Master catalog --%>
                                        <div class="form-group">
                                            <label>Master Catalog</label>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlmstr" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>

                                    <div class="col-lg-6">

                                        <%-- search by transaction date--%>
                                        <div class="form-group">
                                            <label>Transaction Date (From)</label>

                                            <asp:TextBox CssClass="form-control" ID="txttrans_date" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Calendarextender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txttrans_date"
                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                        </div>

                                        <%-- search by customer code --%>
                                        <div class="form-group">
                                            <label>Customer Code</label>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlcust" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <%--search button--%>
                                        <div class="form-group">

                                            <asp:Button ID="btnsearch" Text="Filter" runat="server" CssClass="btn btn-default" OnClick="btnsearch_Click" />

                                        </div>

                                    </div>
                                </div>

                                <%--tab title--%>
                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Header" data-toggle="tab">Packing Info</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">Packing Details</a>
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

                                                                    <asp:Button ID="btnall" Text="Certify All" runat="server" CssClass="btn btn-primary" OnClick="btnall_Click" />
                                                                    <asp:Button ID="btnsave" Text="Save" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
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

                                                        </div>

                                                    </div>

                                                    <div class="panel-body" style="overflow: auto;">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>

                                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="dgvheader" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanged="dgvheader_SelectedIndexChanged">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="btnselect" runat="server" Text="Select" CommandName="Select" CssClass="btn btn-info" />
                                                                                        <%-- <asp:Label ID="lblpo_no" Text='<%# Eval("po_no") %>' runat="server" Visible="false" />--%>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlsts" runat="server">
                                                                                            <asp:ListItem Value="O">OPEN</asp:ListItem>
                                                                                            <asp:ListItem Value="C">CERTIFY</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="sts" HeaderText="Status"></asp:BoundField>
                                                                                <asp:BoundField DataField="brd_code" HeaderText="Brand Code"></asp:BoundField>
                                                                                <asp:BoundField DataField="pkg_no" HeaderText="Packing No"></asp:BoundField>
                                                                                <asp:BoundField DataField="mstr_catalog" HeaderText="Master Catalog"></asp:BoundField>
                                                                                <asp:BoundField DataField="trans_date" HeaderText="Transaction Date"></asp:BoundField>
                                                                                <asp:BoundField DataField="rmk" HeaderText="Remark"></asp:BoundField>

                                                                            </Columns>

                                                                            <HeaderStyle Wrap="False" />
                                                                            <RowStyle Wrap="False" />

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

                                                    <div class="panel-body" style="overflow: auto;">

                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false">
                                                                    <Columns>


                                                                        <asp:BoundField DataField="catalog_no" HeaderText="Catalog No" />
                                                                        <asp:BoundField DataField="dsc" HeaderText="Description" />
                                                                        <asp:BoundField DataField="loc" HeaderText="Location" />
                                                                        <asp:BoundField DataField="req_qty" HeaderText="Req Qty" />
                                                                        <asp:BoundField DataField="o_qty" HeaderText="OnHand Qty" />


                                                                    </Columns>
                                                                    <HeaderStyle Wrap="False" />
                                                                    <RowStyle Wrap="False" />
                                                                </asp:GridView>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnsearch" />
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
