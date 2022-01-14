<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="GRN_Approval.aspx.cs" Inherits="ERP_System.GRN_Module.Forms.GRN_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">

                <div class="col-lg-12">
                    <h1 class="page-header">GRN Approval</h1>
                </div>

                <div class="row">

                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">
                                <div class="row">

                                    <div class="col-lg-6">
                                        <%--  search by po_no--%>
                                        <div class="form-group">
                                            <label>GRN No</label>
                                            <input class="form-control" runat="server" id="txtfilter" type="text" placeholder="PO Number">
                                        </div>

                                        <%--search by vendor--%>
                                        <div class="form-group">
                                            <label>Vendor</label>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlven_name" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>

                                    <div class="col-lg-6">

                                        <%-- search by date--%>
                                        <div class="form-group">
                                            <label>Order Date (From)</label>

                                            <asp:TextBox CssClass="form-control" ID="txtfrom" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Calendarextender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtfrom"
                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>

                                        </div>

                                        <div class="form-group">
                                            <label>Order Date (To)</label>
                                            <asp:TextBox CssClass="form-control" ID="txtto" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Calendarextender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtto"
                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                        </div>

                                        <%--search button--%>
                                        <div class="form-group">

                                            <asp:Button ID="btnsearch" Text="Filter" runat="server" CssClass="btn btn-default" OnClick="btnsearch_Click" />

                                        </div>

                                    </div>
                                </div>

                                <%--tab title--%>
                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Header" data-toggle="tab">GRN Details</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">GRN Details</a>
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

                                                                    <asp:Button ID="btnall" Text="Approve All" runat="server" CssClass="btn btn-primary" OnClick="btnall_Click" />
                                                                    <asp:Button ID="btnsave" Text="Save" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
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
                                                                        <asp:GridView ID="dgvheader" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvheader_SelectedIndexChanged">
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
                                                                                            <asp:ListItem Value="OPEN">OPEN</asp:ListItem>
                                                                                            <asp:ListItem Value="APPROVE">APPROVE</asp:ListItem>
                                                                                            <asp:ListItem Value="CANCEL">CANCEL</asp:ListItem>
                                                                                            <asp:ListItem Value="CLOSE">CLOSE</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="sts" HeaderText="Status" />
                                                                                <asp:BoundField DataField="grn_no" HeaderText="GRN Number" />

                                                                                <asp:BoundField DataField="grn_date" HeaderText="Order Date" />
                                                                                <asp:BoundField DataField="vendor" HeaderText="Vendor Code" />
                                                                                <asp:BoundField DataField="ven_name" HeaderText="Vendor Name" />
                                                                                <asp:BoundField DataField="do_no" HeaderText="Vendor DO No" />
                                                                                <asp:BoundField DataField="rmk" HeaderText="Remark" />

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
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>

                                                                <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false">
                                                                    <Columns>

                                                                        <asp:BoundField DataField="po_no" HeaderText="PO No" />
                                                                        <asp:BoundField DataField="catalog_no" HeaderText="Catalog No" />
                                                                        <asp:BoundField DataField="dsc" HeaderText="Description" />
                                                                        <asp:BoundField DataField="ext_dsc" HeaderText="Extra Description" />
                                                                        <asp:BoundField DataField="uom" HeaderText="Purchase UOM" />
                                                                        <asp:BoundField DataField="rec_qty" HeaderText="Received Qty" />
                                                                        <asp:BoundField DataField="uprice" HeaderText="Unit Cost" />

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
