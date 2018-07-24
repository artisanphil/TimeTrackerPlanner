// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetCategory(_projectid) {
    $.ajax({
        url: "/AddTask/GetCategoriesByProjectId/",
        data: { projectid: _projectid },
        cache: false,
        type: "POST",
        success: function (data) {
            var markup = "";
            for (var x = 0; x < data.length; x++) {
                markup += "<option value=" + data[x]['value'] + ">" + data[x]['text'] + "</option>";
            }
            $("#CategoryList").html(markup).show();
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
}

function AddItemType(itemType) {
    myParentID = 0;
    if(itemType == "Category")
    {
       myParentID = $("#ProjectList").val();
    }
    $.post("/AddTask/Add" + itemType, { item: $("#" + itemType + "Name").val(), parentid: myParentID },
        function(data) {
            console.log(data);
            if(data > 0)
            {
                var opt = "<option value='" + data + "' selected=\"selected\">" + $("#" + itemType + "Name").val() + "</option>";
            
                $('#' + itemType + "List").append(opt)
            }
        });    
}