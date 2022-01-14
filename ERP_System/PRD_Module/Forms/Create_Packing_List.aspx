<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="Create_Packing_List.aspx.cs" Inherits="ERP_System.PRD_Module.Forms.Create_Packing_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Creating Packing List</h1>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">


                                            <asp:Button ID="btnsave" Text="SAVE" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
                                            <asp:Button ID="btnnew" Text="NEW" runat="server" CssClass="btn btn-default" OnClick="btnnew_Click" />
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
                                    <!-- /.row (nested) -->
                                </div>
                                <!-- Nav tabs -->

                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Entry" data-toggle="tab">Packing List Header</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">Packing List Details</a>
                                    </li>

                                </ul>

                                <!-- Tab panes -->
                                <div class="tab-content">

                                    <div class="tab-pane fade in active" id="Entry">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <!-- /brand code -->
                                                <div class="form-group">
                                                    <label>Brand Code</label>
                                                    <asp:DropDownList ID="ddlbrand" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </div>

                                                <!-- /master catalog -->
                                                <div class="form-group">
                                                    <label>Master Catalog</label>
                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlmstr" runat="server" CssClass="form-control" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlbrand" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>


                                                <!-- /effective date -->
                                                <div class="form-group">

                                                    <label>Effective Date</label>
                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox CssClass="form-control" ID="txtef_date" runat="server" ReadOnly="false" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="Calendarextender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtef_date"
                                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>



                                                </div>

                                            </div>

                                            <div class="col-lg-6">

                                                <!-- /version -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">Version</label>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <input class="form-control" runat="server" id="txtver" type="text" placeholder="Version" disabled>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>


                                                <!-- /Ref No contact -->
                                                <div class="form-group">

                                                    <label>Ref No</label>
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                <ContentTemplate>
                                                                    <input class="form-control" runat="server" id="txtrefno" type="text" placeholder="Ref No">
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlbrand" />
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

                                    <div class="tab-pane fade" id="Details">
                                        <div class="row">

                                            <div class="col-lg-6">

                                                <!-- /default location -->
                                                <div class="form-group">

                                                    <label>Default Loc</label>

                                                    <asp:DropDownList ID="ddlloc" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlloc_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </div>

                                                <!-- /qty -->
                                                <div class="form-group">

                                                    <label>Quantity</label>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtqty_TextChanged" Text="0.00"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtqty" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <!-- /details button -->
                                                <div class="form-group">

                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnadd_Click" />

                                                    <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-primary" />
                                                </div>

                                            </div>

                                            <div class="col-lg-6">

                                                <!-- /catalog -->
                                                <div class="form-group">

                                                    <label>Catalog No</label>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlcatalog" runat="server" CssClass="form-control" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlloc" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="row">

                                            <div class="col-lg-12">

                                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>


                                                                <div class="panel panel-default">



                                                                    <div class="panel-body" style="overflow: auto;">

                                                                        <asp:GridView ID="dgvheader" runat="server" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" OnRowDeleting="dgvheader_RowDeleting">
                                                                            <Columns>
                                                                                <asp:CommandField ShowDeleteButton="true" ButtonType="Button" ItemStyle-HorizontalAlign="Center" />
                                                                                <asp:BoundField DataField="loc" HeaderText="Default Loc" />
                                                                                <asp:BoundField DataField="catalog_no" HeaderText="Catalog No" />
                                                                                <asp:BoundField DataField="dsc" HeaderText="Description" />
                                                                                <asp:BoundField DataField="qty" HeaderText="Quantity" />

                                                                            </Columns>

                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />

                                                                            <RowStyle Wrap="False" />
                                                                        </asp:GridView>
                                                                    </div>

                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

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
