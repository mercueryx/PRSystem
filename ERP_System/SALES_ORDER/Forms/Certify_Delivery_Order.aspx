<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="Certify_Delivery_Order.aspx.cs" Inherits="ERP_System.SALES_ORDER.Forms.Certify_Delivery_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">

                <div class="col-lg-12">
                    <h1 class="page-header">Certify Delivery Order</h1>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">

                                <div class="row">

                                    <div class="col-lg-6">

                                        <!-- /delivery download -->
                                        <div class="form-group">
                                            <label>Delivery No.</label>
                                            <asp:DropDownList ID="ddldo" runat="server" CssClass="form-control" AutoPostBack="True">
                                            </asp:DropDownList>

                                        </div>

                                        <!-- /invoice no -->
                                        <div class="form-group">
                                            <label>Invoice No.</label>
                                            <asp:DropDownList ID="ddlinv" runat="server" CssClass="form-control" AutoPostBack="True">
                                            </asp:DropDownList>

                                        </div>

                                        <%-- /Delivery order date--%>
                                        <div class="form-group">
                                            <label>DO Date from</label>

                                            <asp:TextBox CssClass="form-control" ID="txtfrom" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Calendarextender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtfrom"
                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                        </div>

                                        <div class="form-group">
                                            <label>DO Date to</label>

                                            <asp:TextBox CssClass="form-control" ID="txtto" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Calendarextender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtto"
                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                        </div>

                                    </div>

                                    <div class="col-lg-6">


                                        <!-- /order type -->
                                        <div class="form-group">
                                            <label>Order Type.</label>
                                            <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                        <!-- /sold to -->
                                        <div class="form-group">
                                            <label>Sold To.</label>

                                            <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlsold" runat="server" CssClass="form-control" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddltype" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <!-- /ship to -->
                                        <div class="form-group">
                                            <label>Ship To.</label>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlship" runat="server" CssClass="form-control" AutoPostBack="True">
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
                                    <li class="active"><a href="#Header" data-toggle="tab">Delivery Order Header</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">Delivery Order Details</a>
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
                                                                                            <asp:ListItem Value="OPEN">OPEN</asp:ListItem>
                                                                                            <asp:ListItem Value="CERTIFY">CERTIFY</asp:ListItem>
                                                                                            <asp:ListItem Value="CLOSE">CLOSE</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>

                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="sts" HeaderText="Status"></asp:BoundField>
                                                                                <asp:BoundField DataField="do_no" HeaderText="Delivery Order No"></asp:BoundField>
                                                                                <asp:BoundField DataField="d_date" HeaderText="Delivery Order Date"></asp:BoundField>
                                                                                <asp:BoundField DataField="inv_no" HeaderText="Invoice No"></asp:BoundField>
                                                                                <asp:BoundField DataField="t_amount" HeaderText="Total Amount"></asp:BoundField>
                                                                                <asp:BoundField DataField="sold" HeaderText="Sold To"></asp:BoundField>
                                                                                <asp:BoundField DataField="bill" HeaderText="Bill To"></asp:BoundField>
                                                                                <asp:BoundField DataField="ship" HeaderText="Ship To"></asp:BoundField>
                                                                                <asp:BoundField DataField="d_term" HeaderText="Delivery Term"></asp:BoundField>
                                                                                <asp:BoundField DataField="pay_term" HeaderText="Payment Term"></asp:BoundField>
                                                                                <asp:BoundField DataField="usn" HeaderText="Issued By"></asp:BoundField>
                                                                                <asp:BoundField DataField="add_dt" HeaderText="Date Add"></asp:BoundField>
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
                                                        Delivery Order Details
                                                    </div>
                                                    <div class="panel-body" style="overflow: auto;">

                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>

                                                                <asp:GridView ID="dgvdtl" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false">
                                                                    <Columns>

                                                                        <asp:BoundField DataField="sc_no" HeaderText="Sales Contract" />
                                                                        <asp:BoundField DataField="ctlno" HeaderText="Finished Goods" />
                                                                        <asp:BoundField DataField="dsc" HeaderText="Description" />
                                                                        <asp:BoundField DataField="uom" HeaderText="Selling UOM" />
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
                                                        Free Of Charge(FOC) Items
                                                    </div>
                                                    <div class="panel-body" style="overflow: auto;">

                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>

                                                                <asp:GridView ID="dgvfoc" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false">
                                                                    <Columns>

                                                                        <asp:BoundField DataField="foc_ctlno" HeaderText="FOC Item" />
                                                                        <asp:BoundField DataField="dsc" HeaderText="Description" />
                                                                        <asp:BoundField DataField="uom" HeaderText="UOM" />
                                                                        <asp:BoundField DataField="claim_no" HeaderText="Claim Doc No" />
                                                                        <asp:BoundField DataField="gift_code" HeaderText="Gift Code" />
                                                                        <asp:BoundField DataField="qty" HeaderText="Qty" />
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
