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


function setCompleted(planid, completed) {
    $.post("/AddTask/setCompleted", { planid: planid, completed: completed },
        function(data) {
            console.log(data);
        });       
}

/*
function GetTask(_categoryid) {
    $.ajax({
        url: "/AddTask/GetTasksByCategoryId/",
        data: { categoryid: _categoryid },
        cache: false,
        type: "POST",
        success: function (data) {
            var markup = "";
            for (var x = 0; x < data.length; x++) {
                markup += "<option value=" + data[x]['value'] + ">" + data[x]['text'] + "</option>";
            }
            $("#TaskDescription").html(markup).show();
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
}
*/

function addZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

function Start() {
    $("#btnStart").hide();
    $("#btnStop").show();
    const now = new Date();
    $("#starttimems").val(now.getTime());
    $("#starttime").val(addZero(now.getHours()) + ":" + addZero(now.getMinutes()));
    $.post("/Home/Start", { planid: $("#plannedTasksList").val() },
        function(data) {
            console.log(data);
            if(data > 0)
            {
                $("#workid").val(data)
            }

        });  
          
}

function AddItemType(itemType) {
    myParentID = 0;
    if(itemType == "Category")
    {
       myParentID = $("#ProjectList").val();

        if(myParentID == 0)
        {
            alert("Please select a project");
            return false;
        }
    }
    if(itemType == "Task")
    {
       myParentID = $("#CategoryList").val();

        if(myParentID == 0)
        {
            alert("Please select a category");
            return false;
        }
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