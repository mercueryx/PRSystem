<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="PR_Edit.aspx.cs" Inherits="ERP_System.PR_Module.Forms.PR_Edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <style>
        .item_name_class {
                display: block;
               
                overflow: auto;
                width: 500px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="row">

                <div class="col-lg-12">
                    <h1 class="page-header">PR Edit</h1>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                Edit PR (Only for open PR)
                            </div>

                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label for="disabledSelect">Search by request date</label>
                                            <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txtsearch_date" runat="server" ReadOnly="false" AutoCompleteType="Disabled" OnTextChanged="txtsearch_date_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Calendarextender2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtsearch_date"
                                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label for="disabledSelect">Search by open PR</label>
                                                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddls_prno" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddls_prno_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtsearch_date" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">

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
                                                    <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>
                                    </div>
                                    <!-- /.row (nested) -->
                                </div>
                                <!-- Nav tabs -->

                                <ul class="nav nav-pills">
                                    <li class="active"><a href="#Entry" data-toggle="tab">PR Header</a>
                                    </li>
                                    <li><a href="#Details" data-toggle="tab">PR Details</a>
                                    </li>

                                </ul>

                                <!-- Tab panes -->
                                <div class="tab-content">

                                    <div class="tab-pane fade in active" id="Entry">

                                        <div class="row">

                                            <div class="col-lg-6">

                                                <!-- /po no -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">PR No</label>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <input class="form-control" runat="server" id="txtpr" type="text" placeholder="PR No" disabled>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>




                                                <!-- /department -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">Section Group Leader Department (for approval)</label>
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlr_dpt" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlr_dpt_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>


                                                <!-- /Name -->
                                                <div class="form-group">

                                                    <label for="disabledSelect">Requestor</label>
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>

                                                            <input class="form-control" runat="server" id="txtName" type="text" disabled>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>


                                                </div>



                                              

                                            </div>

                                            <div class="col-lg-6">


                                                <!-- /company code -->
                                                <div class="form-group">
                                                    <label>Company Code</label>
                                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlcom" runat="server" CssClass="form-control" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <%--   <input class="form-control" runat="server" id="txtcom" type="text" disabled>--%>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>


                                                <!-- /department -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">Section</label>
                                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel66" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlsec" runat="server" CssClass="form-control" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlr_dpt" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>



                                            </div>


                                        </div>

                                         <div class="row">
                                            <div class="col-lg-12">
                                                  <div class="form-group">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnupdate" Text="UPDATE" runat="server" CssClass="btn btn-primary" OnClick="btnupdate_Click" Visible="false" />
                                                            <asp:Button ID="btncancel" Text="CANCEL" runat="server" CssClass="btn btn-default" OnClick="btncancel_Click" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>
                                                </div>
                                             </div>

                                    </div>

                                    <div class="tab-pane fade" id="Details">
                                        <div class="row">
                                            <div class="col-lg-6">


                                                <!-- /Item Name -->
                                                <div class="form-group">

                                                    <label>Item Name</label>
                                                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtitem" runat="server" CssClass="form-control" Text="" AutoPostBack="true" OnTextChanged="txtitem_TextChanged"></asp:TextBox>
                                                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtitem"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetItems">
                                                            </ajax:AutoCompleteExtender>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                               <%-- item ref--%>
                                                    <div class="form-group">
                                                    <label>Item More Details </label>
                                                      <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                          <%--  <asp:DropDownList ID="ddlref" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>--%>
                                                               <asp:TextBox ID="txtref" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>


                                      

                                                <!-- /Level -->
                                                <div class="form-group">
                                                    <label>Level</label>
                                                    <asp:DropDownList ID="ddllevel" runat="server" CssClass="form-control">
                                                       <%-- <asp:ListItem>No Priority</asp:ListItem>--%>
                                                        <asp:ListItem>Low Priority</asp:ListItem>
                                                     <%--   <asp:ListItem>Medium Priority</asp:ListItem>--%>
                                                        <asp:ListItem>High Priority</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>


                                            


                                            </div>
                                            <!-- /.col-lg-6 (nested) -->
                                            <div class="col-lg-6">

                                                          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>

                                                        <!-- /purpose -->
                                                        <div class="form-group">
                                                            <label>Purpose / Remark</label>
                                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtpurpose" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>

                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                               

                                                <!-- /quantity -->
                                                <div class="form-group">

                                                    <label>Quantity</label>
                                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" AutoPostBack="true" Text="0" OnTextChanged="txtqty_TextChanged"></asp:TextBox>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="txtqty" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>

                                                 <!-- /UOM -->

                                                  <div class="form-group">
                                                    <label>UOM</label>
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddluom" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtitem" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>

                                        </div>
                                               <div class="row">
                                            <div class="col-lg-12">
                                                    <!-- /details button -->
                                                <div class="form-group">
                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnadd_Click" />

                                                    <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnclear_Click" />
                                                </div>
                                          </div>
                                              </div>

                                        <div class="row">

                                            <div class="col-lg-12">

                                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>

                                                                <%--   <div class="form-group">
                                                                    <!-- /gridview -->
                                                                    <strong>
                                                                        <asp:Label ID="lblresult" runat="server" Text=""></asp:Label></strong>

                                                                </div>--%>

                                                                <div class="panel panel-default">

                                                                    <div class="panel-heading">
                                                                        PR Items
                                                                    </div>

                                                                    <div class="panel-body" style="overflow: auto;">

                                                                        <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="dgvitems_RowDeleting" DataKeyNames="id">
                                                                            <Columns>
                                                                                <%-- <asp:TemplateField>--%>
                                                                                <%-- <ItemTemplate>--%>

                                                                                <%--  <asp:LinkButton Text="Delete" runat="server" OnClientClick="Confirm()" CommandName="Delete" />--%>

                                                                                <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                                                                <%--  <asp:BoundField DataField="id" HeaderText="id." Visible="false" />--%>
                                                                                <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-CssClass="item_name_class" />
                                                                                <asp:BoundField DataField="qty" HeaderText="Quantity" />
                                                                                <asp:BoundField DataField="purpose" HeaderText="Purpose/Remark" />
                                                                                <asp:BoundField DataField="lvl" HeaderText="Level" />
                                                                                  <asp:BoundField DataField="uom" HeaderText="UOM" />
                                                                                <%--    </ItemTemplate> --%>



                                                                                <%--   </asp:TemplateField>--%>
                                                                            </Columns>
                                                                            <HeaderStyle Wrap="False" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="ddls_prno" />
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
            <asp:AsyncPostBackTrigger ControlID="txtsearch_date" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
