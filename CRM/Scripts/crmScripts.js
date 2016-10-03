$(function () {

    LoadCustomerList();

    $("#saveCustomerForm").submit(function (event) {
        event.preventDefault();
        SaveCustomer($(this));
    });

    $(document).on("click", ".addNote", function () {
        $("#CustomerID").val($(this).val());
        $("h4.modal-title").text("Add a note for " + $("#CustomerName" + $(this).val()).val());
    });

    $("#saveNoteForm").submit(function (event) {
        event.preventDefault();
        SaveNote($(this));
    });

    $("#noteModal").on("hidden.bs.modal", function () {
        $("#Body-error").hide();
    });

    $("#updateCustomerForm").submit(function (event) {
        event.preventDefault();
        UpdateCustomer($(this));
    });

    $("#saveNoteForm").submit(function (event) {
        event.preventDefault();
        SaveNote($(this));
    });

    $("#noteModal").on("hidden.bs.modal", function () {
        $("#Body-error").hide();
    });
});

function LoadCustomerList() {
    $.ajax({
        type: "GET",
        url: "/Customer/CustomerList",
        datatype: "html",
        success: function (response) {
            $("#customerList").html(response);
        },
        error: function (ts) {
            console.log(ts.responseText);
        }
    });
}

function ToggleCustomerForm() {
    $("#addCustomerDiv").slideToggle();
}

function SaveCustomer(form) {
    if (form.valid()) {
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            datatype: "json",
            success: function (response) {
                if (response.Result > 0) {
                    var customer = {
                        CustomerID: response.Result,
                        FirstName: $("#FirstName").val(),
                        LastName: $("#LastName").val(),
                        Company: $("#Company").val()
                    };
                    form[0].reset();
                    AddTableRow(customer);
                }
            },
            error: function (ts) {
                console.log(ts.responseText);
            }
        });
    }
}

function AddTableRow(customer) {
    var row = "<tr><td>" + customer.LastName + "</td><td>" + customer.FirstName + "</td><td>" + customer.Company + "</td><td>" +
        "<button class='btn btn-default addNote' value='" + customer.CustomerID + "'data-toggle='modal' data-target='#noteModal'>Add Note</button> " +
        "<a href='/Customer/Notes/?id=" + customer.CustomerID + "' class='btn btn-default'>Details</a>" + "</td></tr>";

    $('#tblCustomerList tr:last').after(row);
}

function SaveNote(form) {
    if (form.valid()) {
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            datatype: "json",
            success: function (response) {
                if (response.Result == true) {
                    $("#Body").val("");
                    $("#noteModal").modal("hide");
                }
            },
            error: function (ts) {
                console.log(ts.responseText);
            }
        });
    }
}

function UpdateCustomer(form) {
    if (form.valid()) {
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            datatype: "json",
            success: function (response) {
                if (response.Result == true) {
                    $(".lblUpdated").fadeIn(500).delay(3000).fadeOut(500);
                }
                else {
                    $(".lblFailed").fadeIn(500).delay(3000).fadeOut(500);
                }
            },
            error: function (ts) {
                console.log(ts.responseText);
            }
        });
    }
}

function SaveNote(form) {
    if (form.valid()) {
        var $Body = $("#Body");

        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            datatype: "json",
            success: function (response) {
                if (response.Result == true) {
                    $("#noteModal").modal("hide");

                    var note = {
                        Author: response.Author,
                        Body: $Body.val(),
                        Created: response.Created
                    };

                    $Body.val("");

                    AddTableRow(note);
                }
            },
            error: function (ts) {
                console.log(ts.responseText);
            }
        });
    }
}

function AddTableRow(note) {
    var row = "<tr><td>" + note.Author + "</td><td>" + note.Body + "</td><td>" + note.Created + "</td><td></td></tr>";
    $('#tblNoteList tr:last').after(row);
}