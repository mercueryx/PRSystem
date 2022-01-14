<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMstr.Master" AutoEventWireup="true" CodeBehind="PR_ENTRY.aspx.cs" Inherits="ERP_System.PR_Module.Forms.PR_ENTRY" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- <ItemTemplate>--%>
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
                    <h1 class="page-header">PR Entry</h1>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                New PR
                            </div>

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnsave" Text="SAVE" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" />
                                            <asp:Button ID="btncancel" Text="New" runat="server" CssClass="btn btn-default" OnClick="btncancel_Click" />
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
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
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

                                                <!-- /pr no -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">PR No</label>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <input class="form-control" runat="server" id="txtpr" type="text" placeholder="PR No" disabled>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                             

                                                <!-- /department -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">Section Group Leader Department (for approval)</label>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlr_dpt" runat="server" CssClass="form-control deptMain" AutoPostBack="True" OnSelectedIndexChanged="ddlr_dpt_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </div>


                                                <!-- /Name -->
                                                <div class="form-group">

                                                    <label for="disabledSelect">Requestor</label>
                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                        <ContentTemplate>

                                                            <input class="form-control" runat="server" id="txtName" type="text">
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>


                                                </div>



                                            </div>

                                            <div class="col-lg-6">

                                                <!-- /company code -->
                                                <div class="form-group">
                                                    <label>Company Code</label>
                                                    <asp:DropDownList ID="ddlcom" runat="server" CssClass="form-control" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <%--<input class="form-control" runat="server" id="txtcom" type="text"  disabled>--%>
                                                </div>

                                                <!-- /department -->
                                                <div class="form-group">
                                                    <label for="disabledSelect">Section</label>
                                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlsec" runat="server" CssClass="form-control ddlSecMdf" AutoPostBack="True">
                                                                    </asp:DropDownList>

                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlr_dpt" />
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




                                                <!-- /item -->
                                                <div class="form-group">

                                                    <label>Item Name</label>
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <%--   <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                <ContentTemplate>--%>
                                                            <%-- <asp:DropDownList ID="ddlitem" runat="server" CssClass="form-control" AutoPostBack="true">
                                                                    </asp:DropDownList>--%>
                                                            <%--<input class="form-control" runat="server" id="txtitem" type="text" placeholder="Item Name.">--%>
                                                            <asp:TextBox ID="txtitem" runat="server" CssClass="form-control txtitem" Text="" AutoPostBack="true" OnTextChanged="txtitem_TextChanged"></asp:TextBox>
                                                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtitem"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000" ServiceMethod="GetItems" 
                                                                onclientpopulating="DisableTxt" onclientpopulated="EnableTxt">
                                                            </ajax:AutoCompleteExtender>
                                                            <%--    </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlcom" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>--%>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                      <div class="form-group">
                                                    <label>Item More Details </label>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                          <%--  <asp:DropDownList ID="ddlref" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>--%>
                                                               <asp:TextBox ID="txtref" runat="server" CssClass="form-control readyHide"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>


                                                </div>


                                                <!-- /Level -->
                                                <div class="form-group">
                                                    <label>Priority</label>
                                                    <asp:DropDownList ID="ddllevel" runat="server" CssClass="form-control">
                                                        <%--   <asp:ListItem>No Priority</asp:ListItem>--%>
                                                        <asp:ListItem>Low Priority</asp:ListItem>
                                                        <%-- <asp:ListItem>Medium Priority</asp:ListItem>--%>
                                                        <asp:ListItem>High Priority</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                             
                                            </div>
                                            <!-- /.col-lg-6 (nested) -->
                                            <div class="col-lg-6">

                                          
                                                <div class="form-group">

                                                    <label>Purpose / Remark</label>
                                                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtpurpose" runat="server" CssClass="form-control readyHide"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>


                                                <div class="form-group">
                                                    <label>Quantity</label>
                                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtqty" runat="server" CssClass="form-control readyHide" AutoPostBack="true" Text="0" OnTextChanged="txtqty_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtqty" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                
                                                   <div class="form-group">
                                                    <label>UOM</label>
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
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
                                          
                                                <div class="form-group">


                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnadd_Click" />
                                                    <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary" Enabled="false" Visible="false" />
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

                                                                <div class="form-group">

                                                                    <strong>
                                                                        <asp:Label ID="lblresult" runat="server" Text=""></asp:Label></strong>

                                                                </div>

                                                                <div class="panel panel-default">

                                                                    <div class="panel-heading">
                                                                        PR Items
                                                                    </div>

                                                                    <div class="panel-body">

                                                                        <asp:GridView ID="dgvitems" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="dgvitems_RowDeleting" OnRowDataBound="dgvitems_RowDataBound">
                                                                            <Columns>


                                                                                <asp:CommandField ShowDeleteButton="true" ButtonType="Button" />
                                                                                <asp:BoundField DataField="item_name" HeaderText="Item Name" ItemStyle-CssClass="item_name_class" />
                                                                                <asp:BoundField DataField="qty" HeaderText="Quantity" />
                                                                                <asp:BoundField DataField="purpose" HeaderText="Purpose/Remark" />
                                                                                <asp:BoundField DataField="level" HeaderText="Level" />
                                                                                  <asp:BoundField DataField="uom" HeaderText="UOM" />

                                                                            </Columns>

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



                                            </div>


                                        </div>


                                    </div>

                                </div>


                            </div>

                        </div>
                    </div>

                </div>

            </div>

            </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnsave" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        // Louis Added on 2020/08/12 
        var $ItemName = $('.txtitem');
        function DisableTxt()
        {
             TxtDisableFunction(true);
        }
        function EnableTxt()
        {
             TxtDisableFunction(false);
        } 

        function TxtDisableFunction(value) {
            $(".readyHide").attr("disabled", value);
        }

        $(document).on('change','.deptMain', function() {
             //console.log(this.value);
             $('.ddlSecMdf').select2();
        });

        $(document).on('mouseenter','.ddlSecMdf', function() {
             //console.log(this.value);
             $('.ddlSecMdf').select2();
        });

        $( document ).ready(function() {
            $('.ddlSecMdf').select2();

            $ItemName.keydown(function (e) {
                TxtDisableFunction(true);
            });
        });

    </script>

    <%--   <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtitem]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/PR_Module/Forms/PR_item.asmx/GetItemNames") %>',
                        data: "{ 'prefix': '" + request.term + "'}",

                        //'" + $('#ddlcategory :selected').text() + "'-
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1
            });
        });
    </script>--%>

    <%-- <link rel="stylesheet" href="http://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtitem").autocomplete({
                source: function (request, responce) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/PR_Module/Forms/PR_item.asmx/GetItemNames") %>',
                        method: "post",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify({ term: request.term },),
                        dataType: 'json',
                        success: function (data) {
                            responce(data.d);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });
        });
    </script>--%>
</asp:Content>
