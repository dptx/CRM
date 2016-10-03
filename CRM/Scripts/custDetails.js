$(function () {

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