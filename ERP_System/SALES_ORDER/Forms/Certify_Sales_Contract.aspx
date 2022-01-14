<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="Certify_Sales_Contract.aspx.cs" Inherits="ERP_System.SALES_ORDER.Forms.Certify_Sales_Contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">

                <div class="col-lg-12">
                    <h1 class="page-header">Certify Sales Contract</h1>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">

                                <div class="row">

                                    <div class="col-lg-6">

                                        <!-- /type -->
                                        <div class="form-group">
                                            <label>Type.</label>
                                            <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>LOCAL</asp:ListItem>
                                                <asp:ListItem>EXPORT</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                        <!-- /type -->
                                        <div class="form-group">
                                            <label>Sales Contract No.</label>
                                            <asp:DropDownList ID="ddlsc_no" runat="server" CssClass="form-control" AutoPostBack="True">
                                            </asp:DropDownList>

                                        </div>

                                        <%-- /sc date--%>
                                        <div class="form-group">
                                            <label>SC Date</label>         
                                            
                                                    <asp:TextBox CssClass="form-control" ID="txtscdate" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="Calendarextender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtscdate"
                                                        Format="yyyy-MM-dd"></cc1:CalendarExtender>                                             
                                        </div>

                                    </div>

                                    <div class="col-lg-6">

                                        <!-- /Bill to -->
                                        <div class="form-group">
                                            <label>Bill To.</label>


                                            <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlbillto" runat="server" CssClass="form-control" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddltype" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>

                                        <!-- /sold to -->
                                        <div class="form-group">
                                            <label>Sold To.</label>

                                            <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlsold" runat="server" CssClass="form-control" AutoPostBack="True">
                                                    </asp:DropDownList>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddltype" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnsearch" runat="server" Text="Filter" CssClass="btn btn-primary" OnClick="btnsearch_Click" />

                                        </div>
                                    </div>
                                </div>


                                <%--tab title--%>
                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Header" data-toggle="tab">Sales Contract</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">Sales Contract Details</a>
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

                                                                    <asp:Button ID="btnall" Text="Certify All" runat="server" CssClass="btn btn-primary" OnClick="btnall_Click"/>
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
                                                                                            <asp:ListItem Value="OPEN">OPEN</asp:ListItem>
                                                                                            <asp:ListItem Value="CERTIFY">CERTIFY</asp:ListItem>
                                                                                             <asp:ListItem Value="CLOSE">CLOSE</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="sc_no" HeaderText="SC No"></asp:BoundField>
                                                                                <asp:BoundField DataField="sts" HeaderText="Status"></asp:BoundField>
                                                                                <asp:BoundField DataField="sc_date" HeaderText="SC Date"></asp:BoundField>
                                                                                <asp:BoundField DataField="order_type" HeaderText="SC Type"></asp:BoundField>
                                                                                <asp:BoundField DataField="group_code" HeaderText="Order Type"></asp:BoundField>
                                                                                <asp:BoundField DataField="total" HeaderText="Order Type"></asp:BoundField>
                                                                                <asp:BoundField DataField="add_usn" HeaderText="Issued By"></asp:BoundField>
                                                                                <asp:BoundField DataField="sold_to" HeaderText="Sold To"></asp:BoundField>
                                                                                <asp:BoundField DataField="bill_to" HeaderText="Bill To"></asp:BoundField>
                                                                                <asp:BoundField DataField="pay_term" HeaderText="Payment Term"></asp:BoundField>
                                                                                <asp:BoundField DataField="signatory" HeaderText="Signatory"></asp:BoundField>
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
                                                    <div class="panel-heading">
                                                        Sales Contract Details
                                                    </div>
                                                    <div class="panel-body" style="overflow: auto;">

                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>

                                                                <asp:GridView ID="dgvscd" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvscd_SelectedIndexChanged">
                                                                    <Columns>
                                                                         <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="btnscd_select" runat="server" Text="Select" CommandName="Select" CssClass="btn btn-info" />
                                                                                        <%-- <asp:Label ID="lblpo_no" Text='<%# Eval("po_no") %>' runat="server" Visible="false" />--%>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Sales Contract No" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsc_no" runat="server"
                                                                                    Text='<%# Eval("sc_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item No" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblid" runat="server"
                                                                                    Text='<%# Eval("dtl_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="ctlno" HeaderText="Finished Goods" />
                                                                         <asp:BoundField DataField="dsc" HeaderText="Description" />
                                                                        <asp:BoundField DataField="qty" HeaderText="Qty" />
                                                                        <asp:BoundField DataField="foc_qty" HeaderText="Foc Qty" />


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

                                          <div class="row">

                                            <div class="col-lg-12">

                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        Sales Contract Delivery Details
                                                    </div>
                                                    <div class="panel-body" style="overflow: auto;">

                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>

                                                                <asp:GridView ID="dgvscdd" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Item No" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblid" runat="server"
                                                                                    Text='<%# Eval("dtl_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="etd" HeaderText="ETD" />
                                                                        <asp:BoundField DataField="eta" HeaderText="ETA" />
                                                                        <asp:BoundField DataField="qty" HeaderText="QTY" />
                                                                        <asp:BoundField DataField="foc_qty" HeaderText="FOC Qty" />
                                                                         <asp:BoundField DataField="ship_qty" HeaderText="Shipped Qty" />
                                                                        <asp:BoundField DataField="plan_qty" HeaderText="Planned Qty" />


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


                </div>

            </div>
            </div>
        </ContentTemplate>
     
    </asp:UpdatePanel>
</asp:Content>
